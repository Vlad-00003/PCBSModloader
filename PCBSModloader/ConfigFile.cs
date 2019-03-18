using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Harmony;
using JetBrains.Annotations;
using Tiny;

namespace PCBSModloader
{
    /// <summary>
    ///     This code is based on open-source project Oxide.Core - https://github.com/OxideMod/Oxide.Core
    ///     Tiny-JSON by gering - https://github.com/gering/Tiny-JSON
    ///     Version modified by Vlad-00003 - https://github.com/Vlad-00003
    /// </summary>
    [UsedImplicitly]
    public class ConfigFile : IEnumerable<KeyValuePair<string, object>>
    {
        
        private readonly string _filepath;
        private readonly Mod _owner;
        private Dictionary<string, object> _elements;
        private const string Extension = "{0}.json";


        /// <summary>
        ///     Generic config file. Extension would be automatically added to the filename.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="filename"></param>
        public ConfigFile(Mod owner, string filename = null)
        {
            _owner = owner;
            _filepath = _owner.Modpath + "/" + string.Format(Extension,filename ?? "Config");
            _elements = new Dictionary<string, object>();
        }

        /// <summary>
        ///     Gets or sets a setting on this config by key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public object this[params string[] keys]
        {
            get => Get(keys);
            set => Set(new List<object>(keys) {value}.ToArray());
        }

        /// <summary>
        ///     Check if specified file exists. If filename is null checks the initial file.
        /// </summary>
        /// <param name="path">Actual path to the file</param>
        /// <param name="filename">Optional name of the file. If not specified - "Config" would be used</param>
        /// <returns></returns>
        public bool Exists(out string path, string filename = null)
        {
            path = GetFilePath(filename);
            return File.Exists(path);
        }

        /// <summary>
        ///     Loads the config from the specified file, or the initialized one.
        /// </summary>
        /// <param name="filename"></param>
        public void Load(string filename = null)
        {
            string filepath;
            if (!Exists(out filepath, filename)) return;
            var source = File.ReadAllText(filepath);
            _elements = source.Decode<Dictionary<string, object>>();
        }

        /// <summary>
        ///     Saves the config from the specified file, or the initialized one.
        /// </summary>
        /// <param name="filename">File name</param>
        public void Save(string filename = null)
        {
            var filepath = GetFilePath(filename);
            var dir = GetDirectory(filepath);
            if (dir != null && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(filepath, _elements.Encode(true));
        }

        /// <summary>
        ///     Removes all entries from the config.
        /// </summary>
        public void Clear() => _elements.Clear();

        /// <summary>
        ///     Gets a configuration value at the specified path
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public object Get(params string[] keys)
        {
            if (keys.Length < 1)
                throw new ArgumentException("Attempt to get the config value with an empty path");
            object val;
            if (!_elements.TryGetValue(keys[0], out val)) return null;
            for (var i = 1; i < keys.Length; i++)
            {
                var dict = val as Dictionary<string, object>;
                if (dict == null || !dict.TryGetValue(keys[i], out val))
                {
                    FileLog.Log($"Returning null!");
                    return null;
                }
            }

            return val;
        }

        /// <summary>
        ///     Sets a configuration value at the specified path
        /// </summary>
        /// <param name="pathAndTrailingValue"></param>
        public void Set(params object[] pathAndTrailingValue)
        {
            if (pathAndTrailingValue.Length < 2)
                throw new ArgumentException("Attempt to set the config value without path.");
            var path = new string[pathAndTrailingValue.Length - 1];
            for (var i = 0; i < pathAndTrailingValue.Length - 1; i++)
                path[i] = (string) pathAndTrailingValue[i];
            var value = pathAndTrailingValue[pathAndTrailingValue.Length - 1];
            if (path.Length == 1)
            {
                _elements[path[0]] = value;
                return;
            }

            object val;
            if (!_elements.TryGetValue(path[0], out val))
                _elements[path[0]] = val = new Dictionary<string, object>();
            for (var i = 1; i < path.Length - 1; i++)
            {
                if (!(val is Dictionary<string, object>))
                    throw new ArgumentException("Attempt to set the config value with path not being dictionary.");
                var oldVal = (Dictionary<string, object>) val;
                if (!oldVal.TryGetValue(path[i], out val))
                    oldVal[path[i]] = val = new Dictionary<string, object>();
            }

            ((Dictionary<string, object>) val)[path[path.Length - 1]] = value;
        }

        /// <summary>
        ///     Converts a configuration value to another type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public object ConvertValue(object value, Type destinationType)
        {
            if (value == null) return null;
            if (!destinationType.IsGenericType)
            {
                var ret = Convert.ChangeType(value, destinationType);
                return ret;
            }
            if (destinationType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var valueType = destinationType.GetGenericArguments()[0];
                var list = (IList) Activator.CreateInstance(destinationType);
                foreach (var val in (IList) value)
                {
                    //Custom classes. They are being stored as Dictionary<string,object>.
                    var customClass = val as IDictionary;
                    object addingToList;
                    if (customClass != null)
                    {
                        addingToList = ConvertCustomType(customClass, valueType);
                    }
                    else
                    {
                        addingToList = ConvertValue(val, valueType);
                    }
                    list.Add(addingToList);
                }

                return list;
            }

            if (destinationType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var keyType = destinationType.GetGenericArguments()[0];
                var valueType = destinationType.GetGenericArguments()[1];
                var dict = (IDictionary) Activator.CreateInstance(destinationType);
                foreach (var key in ((IDictionary) value).Keys)
                {
                    var val = ((IDictionary) value)[key];
                    var customClass = val as IDictionary;
                    dict.Add(Convert.ChangeType(key, keyType),
                        customClass != null ? ConvertCustomType(customClass, valueType) : ConvertValue(val, valueType));
                }

                return dict;
            }

            throw new InvalidCastException("Generic types other than List<> and Dictionary<,> are not supported");
        }

        private object ConvertCustomType(IDictionary dict, Type destinationType)
        {
            var fields = GetAllFields(destinationType).ToList();
            object inst;
            try
            {
                inst = Activator.CreateInstance(destinationType);
            }
            catch (Exception e)
            {
                //e.Log(LogType.Console);
                ModLogs.Log(e.ToString());
                return null;
            }
            foreach (var key in dict.Keys)
            {
                var field = fields.FirstOrDefault(x => x.Name == key.ToString());
                if (field == null)
                    throw new ArgumentNullException($"Field \"{key}\" not found!");
                field.SetValue(inst, ConvertValue(dict[key], field.FieldType));
            }
            return inst;
        }

        /// <summary>
        ///     Tries to get a value out of config. If it exists it sets the value to the config one.
        ///     If it doesn't - Adds the provided value to the config.
        ///     If the config was changed returns true, otherwise it return false.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="variable">Variable</param>
        /// <param name="path">Config path</param>
        /// <returns></returns>
        public bool TryGet<T>(ref T variable, params string[] path)
        {
            var tmp = Get(path);
            if (tmp != null)
            {
                var res = ConvertValues<T>(tmp);
                variable = res;
                return false;
            }

            this[path] = variable;
            return true;
        }

        /// <summary>
        ///     Tries to convert object to type of T
        /// </summary>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ConvertValues<T>(object value) => (T) ConvertValue(value, typeof(T));

        /// <summary>
        ///     Gets the value from the config stored at path, then tries to convert it to requested type of T
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(params string[] path) => ConvertValues<T>(Get(path));

        /// <summary>
        ///     Tries to read object of type T from the config file. If it doesn't exists creates the new one with the default
        ///     object of type T.
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <param name="filename"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ReadObject<T>(T defaultValue, string filename = null)
        {
            string filePath;
            T customObject;
            if (Exists(out filePath,filename))
            {
                var source = File.ReadAllText(filePath);
                try
                {
                    customObject = source.Decode<T>();
                }
                catch (Exception e)
                {
                    ModLogs.Log(e.ToString());
                    return defaultValue;
                }
            }
            else
            {
                // ReSharper disable once CompareNonConstrainedGenericWithNull
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (defaultValue != null)
                {
                    customObject = defaultValue;
                }
                else
                {
                    customObject = Activator.CreateInstance<T>();
                }
                WriteObject(customObject, filename);
            }

            return customObject;
        }

        /// <summary>
        ///     Saves the config to the specified file, or the initialized one.
        ///     Sync determines if the config should get the data as well.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config"></param>
        /// <param name="filename"></param>
        /// <param name="sync"></param>
        public void WriteObject<T>(T config, string filename = null, bool sync = false)
        {
            var filepath = GetFilePath(filename);
            var dir = GetDirectory(filepath);
            if (dir != null && !Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var json = config.Encode(true);
            File.WriteAllText(filepath, json);
            if (sync) _elements = json.Decode<Dictionary<string, object>>();
        }

        #region IEnumerable

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => _elements.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => _elements.GetEnumerator();

        #endregion

        #region Helpers
        private static IEnumerable<FieldInfo> GetAllFields(Type type) =>
            type?.GetFields(
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly).Union(GetAllFields(type.BaseType)) ?? Enumerable.Empty<FieldInfo>();

        private string GetFilePath(string filename = null) =>
            filename == null ? _filepath : _owner.Modpath + "/" + string.Format(Extension,filename);

        private static string GetDirectory(string path)
        {
            try
            {
                path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                return path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
            }
            catch (Exception e)
            {
                ModLogs.Log("Unable to get config directory!");
                ModLogs.Log(e.ToString());
                return null;
            }
        }

        #endregion
    }
}