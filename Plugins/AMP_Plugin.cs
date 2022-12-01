using AMP.Network.Data;
using AMP.Network.Data.Sync;

namespace AMP.DedicatedServer {
    public class AMP_Plugin {
        public virtual string NAME { get; }
        public virtual string AUTHOR { get; }
        public virtual string VERSION { get; }

        public virtual void OnStart() { }
        public virtual void OnStop() { }

        public virtual void OnPlayerJoin(ClientData client) { }
        public virtual void OnPlayerQuit(ClientData client) { }
        public virtual void OnPlayerKilled(PlayerNetworkData playerKilled, ClientData killer) { }

        public virtual void OnItemSpawned(ItemNetworkData itemData, ClientData clientSpawned) { }
        public virtual void OnItemDespawned(ItemNetworkData itemData, ClientData clientDespawned) { }
        public virtual void OnItemOwnerChanged(ItemNetworkData itemData, ClientData oldOwner, ClientData newOwner) { }

        public virtual void OnCreatureSpawned(CreatureNetworkData creatureData, ClientData clientSpawned) { }
        public virtual void OnCreatureDespawned(CreatureNetworkData creatureData, ClientData clientDespawned) { }
        public virtual void OnCreatureKilled(CreatureNetworkData creatureData, ClientData killer) { }
        public virtual void OnCreatureOwnerChanged(CreatureNetworkData creatureData, ClientData oldOwner, ClientData newOwner) { }
    }
}
