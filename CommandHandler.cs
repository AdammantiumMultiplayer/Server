using System;
using System.Collections.Generic;
using System.Linq;

namespace AMP.DedicatedServer {
    public class CommandHandler {

        public static List<CommandHandler> CommandHandlers = new List<CommandHandler>();

        public virtual string[] ALIASES => new string[0];
        public virtual string   HELP    => "No help provided.";

        public virtual string Process(string[] args) {
            return HELP;
        }

        public static void RegisterCommandHandler(CommandHandler handler) {
            CommandHandlers.Add(handler);
        }

        public static CommandHandler GetCommandHandler(string command) {
            foreach(CommandHandler handler in CommandHandlers) {
                if(handler.ALIASES.Contains(command, StringComparer.OrdinalIgnoreCase)) {
                    return handler;
                }
            }
            return null;
        }
    }
}
