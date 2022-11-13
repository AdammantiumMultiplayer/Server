namespace AMP.DedicatedServer.Commands {
    internal class HelpCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "help", "?" };
        public override string   HELP    => "Provides help for a other command.";

        public override string Process(string[] args) {
            if(args.Length == 1) {
                if(GetCommandHandler(args[0].ToLower()) != null) {
                    return $"[{args[0]}] " + GetCommandHandler(args[0].ToLower()).HELP;
                } else {
                    return $"Command \"{ args[0] }\" could not be found.";
                }
            }
            return HELP;
        }
    }
}
