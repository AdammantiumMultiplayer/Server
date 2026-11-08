using AMP.Logging;
using AMP.Network.Data;

namespace AMP.DedicatedServer.Commands {
    internal class PosCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "pos" };
        public override string   HELP    => "Shows the position of all players.";

        public override string Process(string[] args) {

            if(ModManager.serverInstance.connectedClients > 0) {
                foreach(ClientData client in ModManager.serverInstance.Clients) {
                    Log.Info(client.ClientName + " XYZ: " + client.player.Position.ToString() + " RotY: " + client.player.RotationY);   
                }
            }
            return null;
        }
    }
}
