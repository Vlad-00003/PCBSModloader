using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PCBSModloader
{
    public class StorageBehaviour : MonoBehaviour
    {
        
    }

    internal class ModdedStorage : PartDesc
    {
        public int m_sizeGb;
        public float m_speedGbs;

        public override bool ImportProp(string name, string value, GetString errorStack)
        {
            if (base.ImportProp(name, value, errorStack))
            {
                return true;
            }
            if (name != null)
            {
                if (name == "Size (GB)")
                {
                    this.m_sizeGb = HTMLTableReader.GetIntFromString(errorStack, value);
                    return true;
                }
                if (name == "Speed")
                {
                    this.m_speedGbs = HTMLTableReader.GetFloatFromString(errorStack, value);
                    return true;
                }
            }
            return false;
        }
        
        public override float GetQuality()
        {
            return (float)this.m_sizeGb;
        }

        // Token: 0x06001D58 RID: 7512 RVA: 0x000567B8 File Offset: 0x000549B8
        public override void GetShopProps(List<string> props)
        {
            base.GetShopProps(props);
            props.Add(ScriptLocalization.ShopProp.Type);
            props.Add(base.m_uiTypeName);
            props.Add(ScriptLocalization.ShopProp.HDD_Size);
            props.Add(this.m_sizeGb.ToGB());
            props.Add(ScriptLocalization.ShopProp.SSD_Speed);
            props.Add(this.m_speedGbs.ToGBs());
        }
    }
}
