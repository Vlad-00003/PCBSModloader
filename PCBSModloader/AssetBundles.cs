using System;
using System.IO;
using UnityEngine;

namespace PCBSModloader
{
    public class AssetBundles : MonoBehaviour
    {
        public static AssetBundle LoadBundle(Mod mod, string BundleName)
        {
            string bundle = ModLoader.ModsPath + "/" + mod.ID + "/AssetBundles/" + BundleName;
            if (File.Exists(bundle))
            {
                try
                {
                    ModLogs.Log(string.Format("Loading Asset Bundle {0}...", BundleName));
                }
                catch (Exception)
                {
                    
                }
                return AssetBundle.LoadFromFile(bundle);
            }
            else
            {
                ModLogs.Log(string.Format("ERROR in LoadBundle(): File not found: {0}", BundleName));
                return null;
            }
        }

        public static Mesh LoadOBJMesh(string fileName)
        {
            string fn = ModLoader.ModsPath + "/Meshes/" + fileName;
            if (!File.Exists(fn))
            {
                ModLogs.Log(string.Format("<b>LoadOBJ() Error:</b>{1}File not found: {0}", fn, Environment.NewLine));
                return null;
            }
            string ext = Path.GetExtension(fn).ToLower();
            if (ext == ".obj")
            {
                OBJLoader obj = new OBJLoader();
                Mesh mesh = obj.ImportFile(ModLoader.ModsPath + "/Meshes/" + fileName);
                mesh.name = Path.GetFileNameWithoutExtension(fn);
                ModLogs.Log(string.Format("Loading Mesh {0}...", mesh.name));
                return mesh;
            }
            else
                ModLogs.Log(string.Format("<b>LoadOBJ() Error:</b>{0}Only (*.obj) files are supported", Environment.NewLine));
            return null;
        }
    }
}
