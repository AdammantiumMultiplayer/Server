using System.Collections.Generic;

namespace AMP_Server {
    public class CommandHandler {

        public static Dictionary<string, CommandHandler> CommandHandlers = new Dictionary<string, CommandHandler>();

        public virtual string Process(string[] args) {
            return GetHelp();
        }

        public virtual string GetHelp() {
            return "No help provided.";
        }

    }
}
