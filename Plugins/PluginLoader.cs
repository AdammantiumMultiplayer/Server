using AMP.DedicatedServer.Data;
using AMP.DedicatedServer.Plugins;
using AMP.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AMP.DedicatedServer {
    internal class PluginLoader {

        internal static List<AMP_Plugin> loadedPlugins = new List<AMP_Plugin>();

        public static void LoadPlugins(string path) {
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
            
            string[] files = Directory.GetFiles(path);

            foreach(string file in files) {
                if(file.EndsWith(".dll")) LoadPlugin(file);
            }
            Log.Info("");
            Log.Info(Defines.PLUGIN_MANAGER, ">> Loaded " + loadedPlugins.Count + " plugins.");
            Log.Info("");
        }

        private static void LoadPlugin(string file) {
            string strDllPath = Path.GetFullPath(file);
            if(File.Exists(strDllPath)) {
                // Execute the method from the requested .dll using reflection (System.Reflection).
                Assembly pluginAssembly = Assembly.LoadFrom(strDllPath);
                Type[] types = pluginAssembly.GetTypes();
                AMP_Plugin plugin = null;
                foreach(Type type in types) {
                    if(type.BaseType == typeof(AMP_Plugin)) {
                        plugin = (AMP_Plugin) Activator.CreateInstance(type);
                        break;
                    }
                }
                if(plugin != null) {
                    Log.Info("");
                    Log.Info(Defines.PLUGIN_MANAGER, $"{plugin.NAME} ({plugin.VERSION}) by {plugin.AUTHOR}");
                    Log.Info(Defines.PLUGIN_MANAGER, $"└─ Loading...");

                    Type pluginConfigType = null;
                    foreach(Type type in types) {
                        if(type.BaseType == typeof(CommandHandler)) {
                            CommandHandler handler = (CommandHandler) Activator.CreateInstance(type);
                            CommandHandler.RegisterCommandHandler(handler);
                        }
                        if(type.BaseType == typeof(PluginConfig)) {
                            pluginConfigType = type;
                        }
                    }

                    if(pluginConfigType != null) {
                        PluginConfigLoader.LoadConfig(plugin, (PluginConfig) Activator.CreateInstance(pluginConfigType));
                        Log.Info(Defines.PLUGIN_MANAGER, $"└─ Loaded config");
                    }

                    try {
                        plugin.OnStart();
                    } catch(Exception e) {
                        Log.Err(e);
                    }

                    loadedPlugins.Add(plugin);

                    Log.Info(Defines.PLUGIN_MANAGER, $"└─ Plugin fully loaded.");
                }
            }
        }

        public static void UnloadPlugins() {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnStop();
                } catch(Exception e) {
                    Log.Err(e);
                }
                Log.Info(Defines.PLUGIN_MANAGER, $"Unloaded plugin {plugin.NAME} ({plugin.VERSION}) by {plugin.AUTHOR}");
            }
        }
    }
}
