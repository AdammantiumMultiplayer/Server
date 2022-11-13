using AMP.DedicatedServer.Data;
using AMP.Logging;

namespace AMP.DedicatedServer.Commands {
    internal class PluginCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "pl" };
        public override string   HELP    => "Lists all plugins.";

        public override string Process(string[] args) {
            Log.Info(Defines.PLUGIN_MANAGER, $"{PluginLoader.loadedPlugins.Count} Plugin(s) loaded.");

            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                Log.Info(Defines.PLUGIN_MANAGER, $"- {plugin.NAME} by {plugin.AUTHOR} (v{plugin.VERSION})");
            }

            return null;
        }
    }
}
