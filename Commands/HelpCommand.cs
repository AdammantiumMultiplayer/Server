namespace AMP_Server.Commands {
    internal class HelpCommand : CommandHandler {

        internal override string[] aliases => new string[] { "?" };

        public override string Process(string[] args) {
            if(args.Length == 1) {
                if(CommandHandlers.ContainsKey(args[0].ToLower())) {
                    return $"[{args[0]}] " + CommandHandlers[args[0].ToLower()].GetHelp();
                } else {
                    return $"Command \"{ args[0] }\" could not be found.";
                }
            }
            return GetHelp();
        }

        public override string GetHelp() {
            return "Provides help for a other command.";
        }
    }
}
