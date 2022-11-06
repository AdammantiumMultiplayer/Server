using AMP.Network.Data;

namespace AMP.DedicatedServer {
    public class AMP_Plugin {

        public virtual string NAME { get; }
        public virtual string AUTHOR { get; }
        public virtual string VERSION { get; }

        public virtual void OnStart() { }
        public virtual void OnStop() { }
        public virtual void OnClientJoin(ClientData client) { }
        public virtual void OnClientQuit(ClientData client) { }

    }
}
