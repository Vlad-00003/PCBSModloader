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
    }
}
