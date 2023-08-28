using AMP.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMP.DedicatedServer.Plugins {
    internal class PluginConfigLoader {

        private static Dictionary<AMP_Plugin, PluginConfig> configs = new Dictionary<AMP_Plugin, PluginConfig>();

        internal static PluginConfig GetConfig(AMP_Plugin plugin) {
            if(configs.ContainsKey(plugin)) {
                return configs[plugin];
            }
            return null;
        }

        internal static bool LoadConfig(AMP_Plugin plugin, PluginConfig config) {

            string path = "plugins/" + plugin.NAME + ".json";
            bool save = true;
            if(File.Exists(path)) {
                string json = File.ReadAllText(path);
                try {
                    config = (PluginConfig) JsonConvert.DeserializeObject(json, config.GetType());
                } catch(Exception e) {
                    Log.Err(e);
                    save = false;
                }
            }

            configs.Add(plugin, config);

            if(save) SaveConfig(plugin);

            return save;
        }

        internal static void SaveConfig(AMP_Plugin plugin) {
            PluginConfig pluginConfig = GetConfig(plugin);
            if(pluginConfig == null) return;

            string path = "plugins/" + plugin.NAME + ".json";
            string json = JsonConvert.SerializeObject(pluginConfig, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}
