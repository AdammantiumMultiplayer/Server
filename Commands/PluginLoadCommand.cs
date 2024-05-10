using AMP.DedicatedServer.Data;
using AMP.Logging;
using System.IO;
using static AMP.DedicatedServer.PluginLoader;

namespace AMP.DedicatedServer.Commands {
    internal class PluginLoadCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "pl_load" };
        public override string   HELP    => "Loads the plugin with the given name.";

        public override string Process(string[] args) {
            if (args.Length == 0) {
                return $"Usage: pl_load [file name]";;
            }

            string file = "plugins/" + (args[0].EndsWith(".dll") ? args[0] : (args[0] + ".dll"));
            AMP_Plugin plugin = PluginLoader.GetPlugin(Path.GetFullPath(file));
            if (plugin != null) {
                return $"Plugin \"{ plugin.NAME }\" is already loaded.";
            }

            PLUGIN_STATUS loaded = PluginLoader.LoadPlugin(file);
            switch (loaded) {
                case PLUGIN_STATUS.OK:
                    PluginWatcher.AddFile(Path.GetFullPath(file));
                    return $"Plugin \"{ args[0] }\" was loaded.";
                case PLUGIN_STATUS.ERROR:
                    return $"Plugin \"{ args[0] }\" had an error while loading.";
                case PLUGIN_STATUS.NOFILE:
                    return $"Plugin \"{ args[0] }\" was not found.";
                default:
                    return null;
            }
        }
    }
}