using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace PCBSModloader
{
    // Not working for now.
    public class ModConsole : Mod
    {
        public override string ID { get { return "ModConsole"; } }
        public override string Name { get { return "ModLoader's Console"; } }
        public override string Version { get { return ModLoader.Version; } }
        public override string Author { get { return "FusioN."; } }

        public bool showConsole = false;
        public string logs = "";

        GameObject CanvasGO;
        GameObject LogsGO;
        Canvas MainCanvas;
        Text Logs;

        public override void OnInit()
        {
            CanvasGO = new GameObject();
            CanvasGO.name = "Console_Canvas";
            CanvasGO.AddComponent<Canvas>();

            MainCanvas = CanvasGO.GetComponent<Canvas>();
            MainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasGO.AddComponent<CanvasScaler>();
            CanvasGO.AddComponent<GraphicRaycaster>();

            LogsGO = new GameObject();
            LogsGO.transform.parent = CanvasGO.transform;
            LogsGO.name = "Console_LogsText";

            Logs = LogsGO.AddComponent<Text>();
            Logs.text = logs;
            Logs.fontSize = 20;

            Logs.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Logs.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 200);
        }

        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                if (showConsole == true)
                    showConsole = false;
                else
                    showConsole = true;
            }

            Logs.text = logs;
        }
    }
}
