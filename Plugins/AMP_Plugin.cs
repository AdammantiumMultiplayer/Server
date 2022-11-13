using AMP.Network.Data;
using AMP.Network.Data.Sync;
using System;

namespace AMP.DedicatedServer {
    public class AMP_Plugin {

        public virtual string NAME { get; }
        public virtual string AUTHOR { get; }
        public virtual string VERSION { get; }

        public virtual void OnStart() { }
        public virtual void OnStop() { }
        public virtual void OnClientJoin(ClientData client) { }
        public virtual void OnClientQuit(ClientData client) { }
        public virtual void OnItemSpawned(ItemNetworkData itemData) { }
        public virtual void OnItemDespawned(ItemNetworkData itemData) { }
        public virtual void OnCreatureSpawned(CreatureNetworkData creatureData) { }
        public virtual void OnCreatureDespawned(CreatureNetworkData creatureData) { }
    }
}
