using AMP;
using AMP.Logging;
using System;
using System.Linq;

namespace AMP_Server.Commands {
    internal class StatusCommand : CommandHandler {

        public override string Process(string[] args) {
            Log.Info("Status:");
            Log.Info("      Items: " + ModManager.serverInstance.spawnedItems);
            Log.Info("  Creatures: " + ModManager.serverInstance.spawnedCreatures);
            Log.Info("    Clients: " + ModManager.serverInstance.connectedClients + " / " + ModManager.serverInstance.maxClients);
            if(ModManager.serverInstance.connectedClients > 0)
                Log.Info("     " + String.Join(", ", ModManager.serverInstance.connectedClientList.Values.ToArray()));

            return null;
        }

        public override string GetHelp() {
            return "Shows the status of the server.";
        }
    }
}
