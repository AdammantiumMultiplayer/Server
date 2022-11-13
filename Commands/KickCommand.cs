namespace AMP.DedicatedServer.Commands {
    internal class KickCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "kick" };
        public override string   HELP    => "Kicks a player. (Not implmented yet)";

        public override string Process(string[] args) {
            
            return null;
        }
    }
}
