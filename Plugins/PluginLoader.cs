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

        public enum PLUGIN_STATUS {
            OK = 0,
            ERROR = 1,
            NOFILE = 2,
        }

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

        public static PLUGIN_STATUS LoadPlugin(string file) {
            string strDllPath = Path.GetFullPath(file);
            if(File.Exists(strDllPath)) {
                // Execute the method from the requested .dll using reflection (System.Reflection).
                Assembly assembly = Assembly.Load(File.ReadAllBytes(strDllPath));
                Type [] types = assembly.GetTypes();

                AMP_Plugin plugin = null;
                foreach(Type type in types) {
                    if(type.BaseType == typeof(AMP_Plugin)) {
                        plugin = (AMP_Plugin) Activator.CreateInstance(type);
                        break;
                    }
                }
                if(plugin != null) {
                    plugin.FILE = strDllPath;

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

                    return PLUGIN_STATUS.OK;
                }

                return PLUGIN_STATUS.ERROR;
            } else {
                return PLUGIN_STATUS.NOFILE;
            }
        }

        public static AMP_Plugin GetPlugin(String name) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                if (plugin.NAME == name || plugin.FILE == name) {
                    return plugin;
                }
            }

            return null;
        }

        public static void UnloadPlugins() {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnStop();
                    PluginConfigLoader.UnloadConfig(plugin);
                } catch(Exception e) {
                    Log.Err(e);
                }
                Log.Info(Defines.PLUGIN_MANAGER, $"Unloaded plugin {plugin.NAME} ({plugin.VERSION}) by {plugin.AUTHOR}");
            }
            PluginLoader.loadedPlugins.Clear();
        }

        public static bool UnloadPlugin(String name) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    if (plugin.NAME.Equals(name, StringComparison.OrdinalIgnoreCase) || plugin.FILE.Contains(name)) {
                        plugin.OnStop();
                        PluginConfigLoader.UnloadConfig(plugin);
                        PluginLoader.loadedPlugins.Remove(plugin);

                        Log.Info(Defines.PLUGIN_MANAGER, $"Unloaded plugin {plugin.NAME} ({plugin.VERSION}) by {plugin.AUTHOR}");
                        return true;
                    }
                } catch(Exception e) {
                    Log.Err(e);
                }
            }

            return false;
        }
    }
}
