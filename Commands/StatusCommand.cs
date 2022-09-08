using AMP;
using AMP.Logging;
using AMP.Network.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMP_Server.Commands {
    internal class StatusCommand : CommandHandler {

        public override string Process(string[] args) {
            Log.Info("Status:");
            Log.Info("      Items: " + ModManager.serverInstance.items.Count);
            Log.Info("  Creatures: " + ModManager.serverInstance.creatures.Count);
            Log.Info("    Clients: " + ModManager.serverInstance.clients.Count + " / " + ModManager.serverInstance.maxClients);
            if(ModManager.serverInstance.clients.Count > 0)
                Log.Info("     " + String.Join(", ", ModManager.serverInstance.clients.Values.Select(client => client.name).ToArray()));

            return null;
        }

        public override string GetHelp() {
            return "Shows the status of the server.";
        }
    }
}
