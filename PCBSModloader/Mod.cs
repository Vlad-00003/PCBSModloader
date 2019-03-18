namespace PCBSModloader
{
    /// <summary>
    /// Basic class that every mod should extend
    /// </summary>
    public abstract class Mod
    {
        /// <summary>
        /// Indicates if the mod has any texture files that must be loaded from "PCBS/mods/ID/Textures"
        /// </summary>
        public virtual bool HasTextures => false;
        /// <summary>
        /// Indicates if the mod has any AssetBundles that must be loaded from "PCBS/mods/ID/Textures"
        /// </summary>
        public virtual bool HasAssetBundles => false;

        /// <summary>
        /// ID of the mod. Same as folder.
        /// </summary>
        public abstract string ID { get; }
        /// <summary>
        /// User-friendly name of the mod.
        /// </summary>
        public virtual string Name { get { return ID; } }
        /// <summary>
        /// Current version of the mod.
        /// </summary>
        public abstract string Version { get; }
        /// <summary>
        /// Author of the mod.
        /// </summary>
        public abstract string Author { get; }

        /// <summary>
        /// Hook that would be called as soon as the mod has been loaded.
        /// </summary>
        public virtual void OnInit() { }
        /// <summary>
        /// Hook that would be called as soon as the game would be started (Continue\New game).
        /// https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager-sceneLoaded.html
        /// </summary>
        public virtual void OnGame() { }
        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnGUI.html
        /// </summary>
        public virtual void OnGUI() { }
        /// <summary>
        /// MonoBehaviour.Update()
        /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// MonoBehaviour.FixedUpdate()
        /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.FixedUpdate.html
        /// </summary>
        public virtual void FixedUpdate() { }

        /// <summary>
        /// TODO: NOT IMPLEMENTED YET
        /// </summary>
        public virtual void Stop() { }

        /// <summary>
        /// Working directory of the mod.
        /// </summary>
        public virtual string Modpath { get { return ModLoader.ModsPath + "/" + ID; } }
    }
}
