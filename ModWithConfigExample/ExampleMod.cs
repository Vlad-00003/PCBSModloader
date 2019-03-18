using PCBSModloader;
using Harmony;

namespace ExampleModNamespace
{
    public class ExampleMod : Mod
    {
        public static ConfigFile ConfigFile1;
        public static ConfigFile ConfigFile2;
        internal static PluginConfig1 PluginConfig1 = new PluginConfig1();
        internal static PluginConfig2 PluginConfig2 = new PluginConfig2();
        public override string ID => "ExampleMod";

        public override string Version => "1.0";

        public override string Author => "Vlad-00003";

        public override void OnInit()
        {
            #region First way

            ModLogs.Log("Testing first way of working with a ConfigFile...\nInitializing ConfigFile1.");
            ConfigFile1 = new ConfigFile(this);
            ModLogs.Log("ConfigFile1 initialized. Loading stored data (if any)");
            ConfigFile1.Load();
            var configChanged = ConfigFile1.TryGet(ref PluginConfig1.FirstOption, "First option") |
                                ConfigFile1.TryGet(ref PluginConfig1.SecondOption, "Second option") |
                                ConfigFile1.TryGet(ref PluginConfig1.ThirdOption, "Third option") |
                                ConfigFile1.TryGet(ref PluginConfig1.RegularList, "Regular list example") |
                                ConfigFile1.TryGet(ref PluginConfig1.CustomList, "Custom list example") |
                                ConfigFile1.TryGet(ref PluginConfig1.RegularDictionary, "Regular dictionary example") |
                                ConfigFile1.TryGet(ref PluginConfig1.CustomDictionary, "Custom dictionary example");
            if (configChanged)
            {
                ModLogs.Log("Config 1 has some value changed (new fields was added), saving...");
                ConfigFile1.Save();
            }

            ModLogs.Log(PluginConfig1.ToString());

            #endregion

            ModLogs.Log(
                "First way of working with a ConfigFile complete.\nTesting second way of working with a ConfigFile..." +
                "\nInitializing ConfigFile2.");

            #region Second Way

            //As we already have file called "Config.json" - we should give the second one another name.
            ConfigFile2 = new ConfigFile(this, "Config2");
            ModLogs.Log("Loading custom class from the config...");
            PluginConfig2 = ConfigFile2.ReadObject(PluginConfig2.DefaultConfig());
            ModLogs.Log("Custom object loaded:");
            ModLogs.Log(PluginConfig2.ToString());
            ModLogs.Log("Changing first value to \"Weeeee\"");
            PluginConfig2.FirstValue = "Weeeee";
            ModLogs.Log("Writing object back to the file");
            ConfigFile2.WriteObject(PluginConfig2);
            ModLogs.Log("All the tests completed!");

            #endregion
        }
    }
}