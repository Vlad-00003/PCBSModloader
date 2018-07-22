using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Harmony;

namespace PCBSModloader
{
    [HarmonyPatch(typeof(PartsDatabase))]
    [HarmonyPatch("InstantiatePart")]
    class PatchPartMeshes
    {
        static GameObject Postfix(GameObject __result)
        {
            //ModLogs.Log(__result.name);
            string[] meshConfigurations = File.ReadAllLines(ModLoader.PatchesPath + "/Parts Meshes Replacer.conf");
            foreach (string meshConfigLine in meshConfigurations)
            {
                //ModLogs.Log(__result.name);
                string[] meshConfig = meshConfigLine.Split('|');
                //ModLogs.Log("MeshConfig: " + meshConfig[0]);
                if (__result.name.Equals(meshConfig[0], StringComparison.OrdinalIgnoreCase))
                {
                    ModLogs.Log("Loading Mesh Replace " + __result.name);
                    Mesh mesh = AssetBundles.LoadOBJMesh(meshConfig[1]);
                    MeshFilter[] meshFilters =  __result.GetComponentsInChildren<MeshFilter>(true);
                    foreach(MeshFilter meshFilter in meshFilters)
                    {
                        meshFilter.mesh = mesh;
                    }
                    //MeshFilter meshFilter = __result.GetComponent<MeshFilter>();
                    //ModLogs.Log(meshFilter.name);
                   // meshFilter.mesh = mesh;
                }
            }
            /*MeshFilter[] meshFilters = __result.GetComponentsInChildren<MeshFilter>(true);
            foreach (MeshFilter meshFilter in meshFilters)
            {
                if (meshFilter.mesh != null && typeof(Mesh).IsInstanceOfType(meshFilter.mesh))
                {
                    //ModLogs.Log(meshFilter.mesh.name); - SHOWS ALL THE MESHES NAMES
                    foreach (string meshConfigLine in meshConfigurations)
                    {
                        string[] meshConfig = meshConfigLine.Split('|');
                        //ModLogs.Log(meshConfig[0] + "a.");
                        if (meshFilter.mesh.name.Equals(meshConfig[0], StringComparison.OrdinalIgnoreCase))
                        {
                            //ModLogs.Log(meshConfig[0]);
                            Mesh mesh = AssetBundles.LoadOBJMesh("titanx.obj");
                            meshFilter.mesh = mesh;
                            //ModLogs.Log("Replacing Mesh " + meshFilter.mesh);
                        }
                    }
                }
            }*/
            return __result;
        }
    }

    /*public class PartReplacer : Mod
    {
        public override string ID { get { return "PartReplacer"; } }
        public override string Name { get { return "PartReplacer Testing"; } }
        public override string Version { get { return ModLoader.Version; } }
        public override string Author { get { return "FusioN."; } }

        public Mesh mesh;

        public static bool ReplacePart(MeshFilter mesh, PrefabRef prefab)
        {
            
        }

        public override void OnGame()
        {
            mesh = AssetBundles.LoadOBJMesh(this, "titanx.obj");
            UnityStuff.Setup(mesh);
        }
    }

    public class UnityStuff : MonoBehaviour
    {
        public static void Setup(Mesh meshA)
        {  
            var meshes = assetBundle.LoadAllAssets<Mesh>();
            CombineInstance[] combine = new CombineInstance[meshes.Length];

            int i2 = 0;
            while (i2 < meshes.Length)
            {
                combine[i2].mesh = meshes[i2];
                i2++;
            }

            foreach (Mesh test in meshes)
            {
                if (test.name == "GEFORCE_GTX_1")
                {
                    meshA = test;
                }
            }

            MeshFilter[] filters = FindObjectsOfType<MeshFilter>();
            for (int i = 0; i < filters.Length; i++)
            { 
                MeshFilter filter = filters[i];
                filter.mesh = meshA;
            }
        }
    }*/
}
