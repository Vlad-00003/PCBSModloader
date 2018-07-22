namespace PCBSModloader
{
    public abstract class Mod
    {
        public virtual bool HasTextures => false;
        public virtual bool HasAssetBundles => false;

        public abstract string ID { get; }
        public virtual string Name { get { return ID; } }
        public abstract string Version { get; }
        public abstract string Author { get; }

        public virtual void OnInit() { }
        public virtual void OnGame() { }
        public virtual void OnGUI() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        public virtual void Stop() { }

        public virtual string Modpath { get { return ModLoader.ModsPath + "/" + ID; } }
    }
}
