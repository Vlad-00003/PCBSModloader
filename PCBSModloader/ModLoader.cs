using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using Harmony;

namespace PCBSModloader
{
    public class ModLoader : MonoBehaviour
    {
        public static bool ModLoaderLoaded { get; private set; }
        public static List<Mod> LoadedMods { get; private set; }
        public static ModLoader Instance { get; private set; }
        public static Textures loadTextures;
        public static AssetBundles loadAssetBundles;

        public static readonly string Version = "0.1";
        public static string ModsPath = GetGameRootPath() + "/Mods";
        public static string PatchesPath = GetGameRootPath() + "/Mods/Harmony";
        /*public static string TexturesPath = GetGameRootPath() + "/Mods/Textures";
        public static string AssetBundlesPath = GetGameRootPath() + "/Mods/AssetBundles";*/

        public static void Init()
        {
            PatchMain.Main();
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

            if (!Directory.Exists(ModsPath))
            {
                Directory.CreateDirectory(ModsPath);
            }

            if (!Directory.Exists(PatchesPath))
            {
                Directory.CreateDirectory(PatchesPath);
            }

            /*if (!Directory.Exists(TexturesPath))
            {
                Directory.CreateDirectory(TexturesPath);
            }

            if (!Directory.Exists(AssetBundlesPath))
            {
                Directory.CreateDirectory(AssetBundlesPath);
            }*/
          
            ModLogs.Log("Loading internal mods...");
            LoadMod(new ModUI());
            //LoadMod(new Parts());

            ModLogs.Log("Loading mods...");
            LoadMods();

            ModLoaderLoaded = true;
            ModLogs.Log("Finished loading.");
        }

        private static void LoadMods()
        {
            // Replaced with a new folder sorting system

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

            /*foreach (string file in Directory.GetFiles(ModsPath))
            {
                if (file.EndsWith(".dll"))
                {
                    LoadDLL(file);
                }
            }*/
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
