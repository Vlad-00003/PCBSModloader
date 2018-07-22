using Harmony;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PCBSModloader
{
    public class ModLoader : MonoBehaviour
    {
        public static bool ModLoaderLoaded { get; private set; }
        public static List<Mod> LoadedMods { get; private set; }
        public static ModLoader Instance { get; private set; }
        public static Textures loadTextures;
        public static AssetBundles loadAssetBundles;
        public static HarmonyInstance Harmony { get; private set; }


        public static readonly string Version = "0.1";
        public static string ModsPath = GetGameRootPath() + "/Mods";

        public static void Init()
        {
            ModLogs.EnableLogs();

            SceneManager.sceneLoaded += OnSceneLoaded;

            if (ModLoaderLoaded || Instance)
            {
                ModLogs.Log("----- PCBSModloader is already loaded! -----\n");
                return; 
            }

            GameObject ModHandler = new GameObject();
            ModHandler.AddComponent<ModLoader>();
            ModHandler.AddComponent<Textures>();
            ModHandler.AddComponent<AssetBundles>();
            Instance = ModHandler.GetComponent<ModLoader>();
            loadTextures = ModHandler.GetComponent<Textures>();
            loadAssetBundles = ModHandler.GetComponent<AssetBundles>();
            GameObject.DontDestroyOnLoad(ModHandler);

            ModLogs.Log("----- Initializing PCBSModloader... -----\n");
            ModLoaderLoaded = false;
            LoadedMods = new List<Mod>();

            ModLogs.Log("Initializing harmony...");
            Harmony = HarmonyInstance.Create("com.github.harmony.pcbs.mod");

            if (!Directory.Exists(ModsPath))
            {
                Directory.CreateDirectory(ModsPath);
            }

            ModLogs.Log("Loading internal mods...");
            LoadMod(new ModUI());

            ModLogs.Log("Loading mods...");
            LoadMods();

            ModLoaderLoaded = true;
            ModLogs.Log("Finished loading.");
        }

        private static void LoadMods()
        {
            foreach (string dir in Directory.GetDirectories(ModsPath))
            {
                foreach (string file in Directory.GetFiles(dir))
                {
                    if (file.EndsWith(".dll"))
                    {
                        LoadDLL(file);
                    }
                }
            }
        }
        
        private static void LoadDLL(string file)
        {
            Assembly assembly = Assembly.LoadFrom(file);

            foreach(Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(Mod)))
                {
                    LoadMod((Mod)Activator.CreateInstance(type));
                }
            }

            // load any harmony patches within the Mod assembly
            Harmony.PatchAll(assembly);
        }

        private static void LoadMod(Mod mod)
        {
            if (!LoadedMods.Contains(mod))
            {
                if (mod.HasTextures)
                {
                    if (!Directory.Exists(ModsPath + "/" + mod.ID + "/Textures"))
                        Directory.CreateDirectory(ModsPath + "/" + mod.ID + "/Textures");
                }

                if (mod.HasAssetBundles)
                {
                    if (!Directory.Exists(ModsPath + "/" + mod.ID + "/AssetBundles"))
                        Directory.CreateDirectory(ModsPath + "/" + mod.ID + "/AssetBundles");
                }

                mod.OnInit();
                LoadedMods.Add(mod);
                ModLogs.Log("Loaded mod " + mod.ID);
            }
            else
            {
                ModLogs.Log("Mod " + mod.ID + " already loaded.");
            }
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 3)
            {
                foreach (Mod mod in LoadedMods)
                {
                    mod.OnGame();
                }
            }
        }

        private void OnGUI()
        {
            foreach(Mod mod in LoadedMods)
            {
                mod.OnGUI();
            }
        }

        private void Update()
        {
            foreach (Mod mod in LoadedMods)
            {
                mod.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (Mod mod in LoadedMods)
            {
                mod.FixedUpdate();
            }
        }

        private static string GetGameRootPath()
        {
            var directoryInfo = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent;
            string path = directoryInfo?.ToString();

            return path;
        }
    }
}
