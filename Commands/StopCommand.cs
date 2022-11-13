namespace AMP.DedicatedServer.Commands {
    internal class StopCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "stop", "exit" };
        public override string   HELP    => "Stops the server.";

        public override string Process(string[] args) {
            ModManager.StopHost();
            return null;
        }
    }
}
