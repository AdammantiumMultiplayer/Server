using AMP.Network.Data;

namespace AMP.DedicatedServer {
    public class AMP_Plugin {

        public virtual string Name { get; }
        public virtual string Author { get; }
        public virtual string Version { get; }

        public virtual void OnStart() { }
        public virtual void OnStop() { }
        public virtual void OnClientJoin(ClientData client) { }
        public virtual void OnClientQuit(ClientData client) { }

    }
}
