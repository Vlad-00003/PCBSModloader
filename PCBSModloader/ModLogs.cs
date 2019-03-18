using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace PCBSModloader
{
    /// <summary>
    /// Provides an easy way to log information
    /// </summary>
    public class ModLogs : MonoBehaviour
    {
        /// <summary>
        /// Path to the logfile.
        /// </summary>
        public static readonly string LogsPath = GetGameRootPath() + "/Mods/log.txt";

        /// <summary>
        /// Removes the existing logfile.
        /// </summary>
        public static void EnableLogs()
        {
            if (File.Exists(LogsPath))
            {
                File.Delete(LogsPath);
            }
        }

        /// <summary>
        /// Append text to the logfile.
        /// </summary>
        /// <param name="logString"></param>
        public static void Log(string logString)
        {
            File.AppendAllText(LogsPath, logString + Environment.NewLine);
        }

        private static string GetGameRootPath()
        {
            var directoryInfo = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent;
            string path = directoryInfo?.ToString();

            return path;
        }
    }
}
