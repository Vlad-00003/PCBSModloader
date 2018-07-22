using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace PCBSModloader
{
    public class ModUI : Mod
    {
        public override string ID { get { return "ModUI"; } }
        public override string Name { get { return "ModLoader Interface"; } }
        public override string Version { get { return ModLoader.Version; } }
        public override string Author { get { return "FusioN."; } }

        public bool ShowModList = false;
        public bool ShowMessage = true;

        GUIStyle LabelStyle = new GUIStyle();

        public override void OnInit()
        {
            LabelStyle.normal.textColor = Color.green;
        }

        public override void OnGame()
        {
            ShowMessage = false;
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                if (ShowModList == true)
                    ShowModList = false;
                else
                    ShowModList = true;
            }
        }

        public override void OnGUI()
        {
            if (ShowMessage == true)
                GUI.Label(new Rect(10f, 10f, 200f, 25f), "Modloader Initialized.", LabelStyle);

            if (ShowModList == true)
            {
                int i = 0;

                foreach (Mod mod in ModLoader.LoadedMods)
                {
                    if (mod.ID == "ModUI" || mod.ID == "ModConsole")
                        continue;

                    i += 1;
                    GUI.Label(new Rect(10f, 35f + (15f * i), 200f, 25f), mod.ID, LabelStyle);
                }
            }
        }
    }
}
