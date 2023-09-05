using AMP.DedicatedServer.Plugins;

namespace AMP.DedicatedServer {
    public class AMP_Plugin {
        public virtual string NAME { get; }
        public virtual string AUTHOR { get; }
        public virtual string VERSION { get; }

        public virtual void OnStart() { }
        public virtual void OnStop() { }

        public PluginConfig GetConfig() {
            return PluginConfigLoader.GetConfig(this);    
        }
    }
}
