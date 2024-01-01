using AMP.Network.Data;
using AMP.Network.Server;
using System;
using System.Linq;

namespace AMP.DedicatedServer.Commands {
    internal class BanCommand : CommandHandler {
        public override string[] ALIASES => new string[] { "ban" };
        public override string HELP => "Bans a player";

        public override string Process(string[] args) {
            int id = -1;
            string reason = "No reason specified";
            if(args.Length >= 1) {
                try {
                    id = int.Parse(args[0]);
                }catch(Exception) {
                    foreach(var client in ModManager.serverInstance.Clients) {
                        if(client.ClientName.ToLower().Contains(args[0].ToLower())) {
                            id = client.ClientId;
                            break;
                        }
                    }
                }

                if(args.Length >= 2) {
                    reason = string.Join(" ", args.Skip(1));
                }

                if(id >= 0) {
                    ClientData client = ModManager.serverInstance.GetClientById(id);
                    if(client != null) {
                        client.Ban(reason);
                        return $"Player {client.ClientName} has been banned.";
                    }
                }

                return $"Player {args[0]} could not be found.";
            }

            return "ban <playername> <reason>";
        }
    }
}
