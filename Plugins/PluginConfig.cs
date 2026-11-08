namespace AMP.DedicatedServer.Plugins {
    public class PluginConfig {


        public static void Save(AMP_Plugin plugin) {
            PluginConfigLoader.SaveConfig(plugin);
        }
    }
}
