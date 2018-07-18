using System;
using System.IO;
using UnityEngine;

namespace PCBSModloader
{
    public class Textures : MonoBehaviour
    {
        public static Texture2D LoadTexture(Mod mod, string fileName, bool normalMap = false)
        {
            string FilePath = ModLoader.ModsPath + "/" + mod.ID + "/Textures";

            if (!File.Exists(FilePath))
            {
                ModLogs.Log(string.Format("ERROR in LoadTexture(): File not found {0}", FilePath));
                return null;
            }

            string Extension = Path.GetExtension(FilePath).ToLower();

            if (Extension == ".png")
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(File.ReadAllBytes(FilePath));
                if (normalMap)
                    SetNormalMap(ref texture);
                return texture;
            }
            else
            {
                ModLogs.Log(string.Format("ERROR in LoadTexture(): Texture extension not supported: {0}", Environment.NewLine));
            }
            return null;
        }

        static void SetNormalMap(ref Texture2D tex)
        {
            Color[] pixels = tex.GetPixels();
            for (int i = 0; i < pixels.Length; i++)
            {
                Color temp = pixels[i];
                temp.r = pixels[i].g;
                temp.a = pixels[i].r;
                pixels[i] = temp;
            }
            tex.SetPixels(pixels);
        }
    }
}
