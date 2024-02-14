using AMP.DedicatedServer.Data;
using AMP.Logging;
using System.IO;

namespace AMP.DedicatedServer.Commands {
    internal class PluginUnloadCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "pl_unload" };
        public override string   HELP    => "Unloads the plugin with the given name.";

        public override string Process(string[] args) {
            if (args.Length == 0) {
                return $"Usage: pl_unload [plugin name]";;
            }

            bool success = PluginLoader.UnloadPlugin(args[0]);
            PluginWatcher.RemoveFile(Path.GetFullPath("plugins/" + (args[0].EndsWith(".dll") ? args[0] : (args[0] + ".dll"))));

            return success ? $"Plugin \"{ args[0] }\" was unloaded." : $"Plugin \"{ args[0] }\" was not found or loaded?";
        }
    }
}