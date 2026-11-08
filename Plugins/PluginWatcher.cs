using AMP.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AMP.DedicatedServer {
    internal class PluginWatcher {

        internal static Dictionary<String, DateTime> lastChange = new Dictionary<String, DateTime>();
        internal static List<String> filelist = new List<String>();
        private static Thread watcher;
        private static bool running = false;

        public static void InitWatcher() {
            if (running) {
                new Exception("[PluginWatcher] Already running?");
            }

            running = true;

            watcher = new Thread(WatcherThread);
            watcher.Start();
        }

        public static void AddFile(string file) {
            if (!filelist.Contains(file) && File.Exists(file)) {
                filelist.Add(file);
            }
        }

        public static void RemoveFile(string file) {
            filelist.Remove(file);
        }

        public static void WatcherThread() {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                lastChange[plugin.FILE] = File.GetLastWriteTime(plugin.FILE);
                filelist.Add(plugin.FILE);
            }

            while (running) {
                foreach(String plugin in filelist) {
                    DateTime time = File.GetLastWriteTime(plugin);
                    if (time != lastChange[plugin]) {
                        PluginLoader.UnloadPlugin(plugin);
                        PluginLoader.PLUGIN_STATUS status = PluginLoader.LoadPlugin(plugin);
                        switch (status) {
                            case PluginLoader.PLUGIN_STATUS.OK:
                                AMP_Plugin amp_plugin = PluginLoader.GetPlugin(plugin);
                                Log.Info("[PluginWatcher] Reloaded " + amp_plugin.NAME);
                                break;
                            case PluginLoader.PLUGIN_STATUS.NOFILE:
                                Log.Warn("[PluginWatcher] Failed to reload " + plugin + "! (It was deleted?)");
                                break;
                            case PluginLoader.PLUGIN_STATUS.ERROR:
                                Log.Err("[PluginWatcher] An error ocurred while reloading " + plugin);
                                break;
                            default:
                                Log.Err("[PluginWatcher] How did we get here");
                                break;
                        }

                        lastChange[plugin] = time;
                    }
                }

                Thread.Sleep(100);
            }
        }

        public void StopWatcher() {
            if (!running) {
                new Exception("[PluginWatcher] Not running?");
            }

            running = false;
            watcher.Join();
        }
    }
}