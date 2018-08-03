using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;
using Harmony;

namespace PCBSModloader
{
    [HarmonyPatch(typeof(PartsDatabase))]
    [HarmonyPatch("InstantiatePart")]
    [HarmonyPatch(new Type[] { typeof(PartInstance), typeof(Transform) })]
    class PartsPatch
    {
        static GameObject Postfix(GameObject __result, PartInstance part, Transform parent)
        {
            string[] config = File.ReadAllLines(ModLoader.ModsPath + "/prefabs.conf");
            foreach (string line in config)
            {
                string[] prefab = line.Split('|');
                if (__result.name.Equals(prefab[0], StringComparison.OrdinalIgnoreCase))
                {
                    UnityEngine.Object.Destroy(__result);

                    AssetBundle assetBundle = AssetBundle.LoadFromFile(ModLoader.ModsPath + "/Prefabs/" + prefab[1]);
                    PartDesc part2 = part.GetPart();

                    GameObject loaded = assetBundle.LoadAsset<GameObject>(prefab[2]);
                    GameObject modded = UnityEngine.Object.Instantiate<GameObject>(loaded, parent);
                    modded.SetActive(true);

                    if (part2.m_subPart == null)
                    {
                        if (parent)
                        {
                            modded.transform.parent = null;
                            modded.transform.localScale = loaded.transform.localScale;
                            modded.transform.rotation = parent.rotation;
                            modded.transform.parent = parent;
                        }
                        NonModularConnector componentInChildren = modded.GetComponentInChildren<NonModularConnector>();
                        if (componentInChildren)
                        {
                            componentInChildren.CreateCables();
                        }
                    }
                    modded.AddComponent<PartInstanceContainer>().Init(part);
                    modded.AddComponent<BoxCollider>();
                    modded.AddComponent<ComponentPC>();
                    modded.name = part.GetPart().m_id;
                    assetBundle.Unload(false);

                    ModLogs.Log("Replaced prefab of part " + part.GetPart().m_id);
                    return modded;
                }
            }

            return __result;
        }
    }
}
