using AMP;

namespace AMPS.Commands {
    internal class StopCommand : CommandHandler {

        internal override string[] aliases => new string[] { "exit" };

        public override string Process(string[] args) {
            ModManager.StopHost();
            return null;
        }

        public override string GetHelp() {
            return "Stops the server.";
        }
    }
}
