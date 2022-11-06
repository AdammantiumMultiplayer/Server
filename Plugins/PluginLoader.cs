using AMP.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace AMPS.Plugins {
    internal class PluginLoader {

        private static List<AMP_Plugin> loadedPlugins = new List<AMP_Plugin>();

        public static void LoadPlugins(string path) {
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
            
            string[] files = Directory.GetFiles(path);

            foreach(string file in files) {
                LoadPlugin(file);
            }
        }

        private static void LoadPlugin(string file) {
            string strDllPath = Path.GetFullPath(file);
            if(File.Exists(strDllPath)) {
                // Execute the method from the requested .dll using reflection (System.Reflection).
                Assembly pluginAssembly = Assembly.LoadFrom(strDllPath);
                Type[] types = pluginAssembly.GetTypes();
                foreach(Type type in types) {
                    if(type.BaseType == typeof(AMP_Plugin)) {
                        AMP_Plugin pluginInstance = (AMP_Plugin) Activator.CreateInstance(type);

                        pluginInstance.OnStart();

                        loadedPlugins.Add(pluginInstance);

                        Log.Info($"[PluginManager] Loaded plugin {pluginInstance.PluginName} ({pluginInstance.PluginVersion}) by {pluginInstance.PluginAuthor}");
                    }
                }
            }
        }

    }
}
