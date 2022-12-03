using AMP.Logging;
using System;
using System.Linq;

namespace AMP.DedicatedServer.Commands {
    internal class StatusCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "status" };
        public override string   HELP    => "Shows the status of the server.";

        public override string Process(string[] args) {
            Log.Info("Status:");
            Log.Info("        Map: " + ModManager.serverInstance.currentLevel + " / " + ModManager.serverInstance.currentMode);
            Log.Info("      Items: " + ModManager.serverInstance.spawnedItems);
            Log.Info("  Creatures: " + ModManager.serverInstance.spawnedCreatures);
            Log.Info("    Clients: " + ModManager.serverInstance.connectedClients + " / " + ModManager.serverInstance.maxClients);
            if(ModManager.serverInstance.connectedClients > 0)
                Log.Info("     " + String.Join(", ", ModManager.serverInstance.connectedClientList.Values.ToArray()));

            return null;
        }
    }
}
