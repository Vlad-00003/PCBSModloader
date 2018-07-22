using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace PCBSModloader
{
    public class ModLogs : MonoBehaviour
    {
        public static string LogsPath = GetGameRootPath() + "/Mods/log.txt";

        public static void EnableLogs()
        {
            if (File.Exists(LogsPath))
            {
                File.Delete(LogsPath);
            }
        }

        public static void Log(string logString)
        {
            ModConsole console = new ModConsole();

            File.AppendAllText(LogsPath, logString + Environment.NewLine);
            console.logs += logString + Environment.NewLine;
        }

        private static string GetGameRootPath()
        {
            var directoryInfo = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).Parent;
            string path = directoryInfo?.ToString();

            return path;
        }
    }
}
