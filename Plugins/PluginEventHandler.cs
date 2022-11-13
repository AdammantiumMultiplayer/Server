using AMP.Logging;
using AMP.Network.Data;
using AMP.Network.Data.Sync;
using AMP.Network.Server;
using System;
using ThunderRoad;

namespace AMP.DedicatedServer {
    internal class PluginEventHandler {

        internal static void RegisterEvents() {
            Server.OnClientJoin += InvokeOnClientJoin;
            Server.OnClientQuit += InvokeOnClientQuit;
            Server.OnItemSpawned += InvokeOnItemSpawned;
            Server.OnItemDespawned += InvokeOnItemDespawned;
            Server.OnCreatureSpawned += InvokeOnCreatureSpawned;
            Server.OnCreatureDespawned += InvokeOnCreatureDespawned;
        }

        internal static void InvokeOnClientJoin(ClientData client) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnClientJoin(client);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        internal static void InvokeOnClientQuit(ClientData client) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnClientQuit(client);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnItemSpawned(ItemNetworkData itemData) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnItemSpawned(itemData);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnItemDespawned(ItemNetworkData itemData) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnItemDespawned(itemData);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnCreatureSpawned(CreatureNetworkData creatureData) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnCreatureSpawned(creatureData);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnCreatureDespawned(CreatureNetworkData creatureData) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnCreatureDespawned(creatureData);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }
    }
}
