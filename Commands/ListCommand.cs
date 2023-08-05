using AMP.Logging;

namespace AMP.DedicatedServer.Commands {
    internal class ListCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "list" };
        public override string   HELP    => "Shows all available commands.";

        public override string Process(string[] args) {
            Log.Line('=', $"Commands ({ CommandHandlers.Count })");
            foreach(CommandHandler command in CommandHandlers) {
                Log.Info($"{ command.ALIASES[0].PadRight(10) } - { command.HELP }");
            }
            Log.Line('=');

            return null;
        }
    }
}
