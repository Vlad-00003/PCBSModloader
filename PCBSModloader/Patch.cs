using System.Reflection;
using System.IO;
using UnityEngine;
using Harmony;

namespace PCBSModloader
{
    public class PatchMain : MonoBehaviour
    {
        public static void Main()
        {
            ModLogs.Log("Initializing harmony...");
            var harmony = HarmonyInstance.Create("com.github.harmony.pcbs.mod");

            ModLogs.Log("Loading all internal harmony patches");
            harmony.PatchAll(Assembly.GetExecutingAssembly()); // if we only use external DLLs this will not be needed

            ModLogs.Log("Loading all external harmony patches from " + ModLoader.PatchesPath);
            string[] harmonyMods = Directory.GetDirectories(ModLoader.PatchesPath);
            foreach (string modDirectory in harmonyMods)
            {
                string[] modDlls = Directory.GetFiles(modDirectory, "*.dll");
                foreach (string modDll in modDlls)
                {
                    harmony.PatchAll(Assembly.LoadFrom(modDll));
                }
            }
            
        }

    }
}
