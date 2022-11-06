namespace AMPS.Commands {
    internal class KickCommand : CommandHandler {

        internal override string[] aliases => new string[] { "kick" };

        public override string Process(string[] args) {
            
            return null;
        }

        public override string GetHelp() {
            return "Kicks a player.";
        }
    }
}
