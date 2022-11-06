using System.Collections.Generic;

namespace AMP.DedicatedServer {
    public class CommandHandler {

        public static Dictionary<string, CommandHandler> CommandHandlers = new Dictionary<string, CommandHandler>();

        internal virtual string[] aliases => new string[0];

        public virtual string Process(string[] args) {
            return GetHelp();
        }

        public virtual string GetHelp() {
            return "No help provided.";
        }

    }
}
