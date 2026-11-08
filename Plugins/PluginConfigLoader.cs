using AMP.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace AMP.DedicatedServer.Plugins {
    internal class PluginConfigLoader {

        private static Dictionary<String, PluginConfig> configs = new Dictionary<String, PluginConfig>();

        internal static PluginConfig GetConfig(AMP_Plugin plugin) {
            if(configs.ContainsKey(plugin.FILE)) {
                return configs[plugin.FILE];
            }
            return null;
        }

        // BUG: Is there a memeory leak in LoadConfig? (Bigger Config = Bigger Leak?). Could it be JsonConvert.DeserializeObject?
        internal static bool LoadConfig(AMP_Plugin plugin, PluginConfig config) {

            string path = "plugins/" + plugin.NAME + ".json";
            bool save = true;
            if(File.Exists(path)) {
                string json = File.ReadAllText(path);
                try {
                    config = (PluginConfig)JsonConvert.DeserializeObject(json, config.GetType());
                } catch(Exception e) {
                    Log.Err(e);
                    save = false;
                }
            }

            configs[plugin.FILE] = config;

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

        internal static void UnloadConfig(AMP_Plugin plugin) {
            if (configs.ContainsKey(plugin.FILE)) {
                configs.Remove(plugin.FILE);
            }
        }
    }
}
