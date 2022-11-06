namespace AMPS.Plugins {
    public class AMP_Plugin {

        public virtual string PluginName { get; }
        public virtual string PluginAuthor { get; }
        public virtual string PluginVersion { get; }

        public virtual void OnStart() {

        }

        public virtual void OnStop() {

        }

    }
}
