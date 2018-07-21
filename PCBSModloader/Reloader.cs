using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace PCBSModloader
{
    public class Reloader : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F12))
            {
                ModLoader.LoadedMods.Clear();

                ModLoader.LoadMods();

                ModLogs.Log("Reloaded all the mods!");
                //public static List<Mod> LoadedMods { get; private set; }
            }
        }
    }
}
