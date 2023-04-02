using AMP.Events;
using AMP.Logging;
using AMP.Network.Data;
using AMP.Network.Data.Sync;
using Netamite.Server.Data;
using System;

namespace AMP.DedicatedServer {
    internal class PluginEventHandler {

        internal static void RegisterEvents() {
            ServerEvents.OnPlayerJoin           += InvokeOnPlayerJoin;
            ServerEvents.OnPlayerQuit           += InvokeOnPlayerQuit;
            ServerEvents.OnPlayerKilled         += InvokeOnPlayerKilled;

            ServerEvents.OnItemSpawned          += InvokeOnItemSpawned;
            ServerEvents.OnItemDespawned        += InvokeOnItemDespawned;
            ServerEvents.OnItemOwnerChanged     += InvokeOnItemOwnerChanged;

            ServerEvents.OnCreatureSpawned      += InvokeOnCreatureSpawned;
            ServerEvents.OnCreatureDespawned    += InvokeOnCreatureDespawned;
            ServerEvents.OnCreatureKilled       += InvokeOnCreatureKilled;
            ServerEvents.OnCreatureOwnerChanged += InvokeOnCreatureOwnerChanged;
        }

        #region Player Events
        internal static void InvokeOnPlayerJoin(ClientInformation client) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnPlayerJoin(client);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        internal static void InvokeOnPlayerQuit(ClientInformation client) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnPlayerQuit(client);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnPlayerKilled(PlayerNetworkData playerKilled, ClientInformation killer) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnPlayerKilled(playerKilled, killer);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }
        #endregion


        #region Item Events
        private static void InvokeOnItemSpawned(ItemNetworkData itemData, ClientInformation clientSpawned) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnItemSpawned(itemData, clientSpawned);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnItemDespawned(ItemNetworkData itemData, ClientInformation clientDespawned) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnItemDespawned(itemData, clientDespawned);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnItemOwnerChanged(ItemNetworkData itemData, ClientInformation oldOwner, ClientInformation newOwner) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnItemOwnerChanged(itemData, oldOwner, newOwner);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }
        #endregion


        #region Creature Events
        private static void InvokeOnCreatureSpawned(CreatureNetworkData creatureData, ClientInformation clientSpawned) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnCreatureSpawned(creatureData, clientSpawned);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnCreatureDespawned(CreatureNetworkData creatureData, ClientInformation clientDespawned) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnCreatureDespawned(creatureData, clientDespawned);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnCreatureKilled(CreatureNetworkData creatureData, ClientInformation killer) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnCreatureKilled(creatureData, killer);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }

        private static void InvokeOnCreatureOwnerChanged(CreatureNetworkData creatureData, ClientInformation oldOwner, ClientInformation newOwner) {
            foreach(AMP_Plugin plugin in PluginLoader.loadedPlugins) {
                try {
                    plugin.OnCreatureOwnerChanged(creatureData, oldOwner, newOwner);
                } catch(Exception e) {
                    Log.Err(e);
                }
            }
        }
        #endregion
    }
}
