using AMP.Network.Data.Sync;
using Netamite.Server.Data;

namespace AMP.DedicatedServer {
    public class AMP_Plugin {
        public virtual string NAME { get; }
        public virtual string AUTHOR { get; }
        public virtual string VERSION { get; }

        public virtual void OnStart() { }
        public virtual void OnStop() { }

        public virtual void OnPlayerJoin(ClientInformation client) { }
        public virtual void OnPlayerQuit(ClientInformation client) { }
        public virtual void OnPlayerKilled(PlayerNetworkData playerKilled, ClientInformation killer) { }

        public virtual void OnItemSpawned(ItemNetworkData itemData, ClientInformation clientSpawned) { }
        public virtual void OnItemDespawned(ItemNetworkData itemData, ClientInformation clientDespawned) { }
        public virtual void OnItemOwnerChanged(ItemNetworkData itemData, ClientInformation oldOwner, ClientInformation newOwner) { }

        public virtual void OnCreatureSpawned(CreatureNetworkData creatureData, ClientInformation clientSpawned) { }
        public virtual void OnCreatureDespawned(CreatureNetworkData creatureData, ClientInformation clientDespawned) { }
        public virtual void OnCreatureKilled(CreatureNetworkData creatureData, ClientInformation killer) { }
        public virtual void OnCreatureOwnerChanged(CreatureNetworkData creatureData, ClientInformation oldOwner, ClientInformation newOwner) { }
    }
}
