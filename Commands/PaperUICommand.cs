using AMP.Events;
using AMP.Logging;
using AMP.Network.Data;
using AMP.Network.Packets.Implementation;
using System;

namespace AMP.DedicatedServer.Commands {
    internal class PaperUICommand : CommandHandler {

        public override string[] ALIASES => new string[] { "paperui" };
        public override string   HELP    => "Shows a test paper with two buttons to a player. Usage: paperui <player>";

        private const string PAPER_ID = "paperui_test";
        private static bool registered = false;

        public override string Process(string[] args) {
            if(args.Length < 1) return "Usage: paperui <player>";

            RegisterEvents();

            ClientData target = null;
            int id;
            if(int.TryParse(args[0], out id)) {
                target = ModManager.serverInstance.GetClientById(id);
            }
            if(target == null) {
                foreach(ClientData client in ModManager.serverInstance.Clients) {
                    if(client.ClientName.ToLower().Contains(args[0].ToLower())) {
                        target = client;
                        break;
                    }
                }
            }
            if(target == null) return $"Player \"{args[0]}\" not found.";

            target.ShowPaperUI(new PaperUIPacket(PAPER_ID, "Paper UI Test\n\nPress one of the buttons below.")
                .AddButton("accept", "Accept")
                .AddButton("decline", "Decline"));

            return $"Paper UI shown to {target.ClientName}.";
        }

        private static void RegisterEvents() {
            if(registered) return;
            registered = true;

            ServerEvents.onActionButtonPressed += (player, identifier, action) => {
                if(identifier != PAPER_ID) return;

                Log.Info($"[PaperUI] {player.ClientName} ({player.ClientId}) pressed \"{action}\" on \"{identifier}\".");
                player.ClosePaperUI(identifier);
            };

            ServerEvents.onPaperUIClosed += (player, identifier) => {
                if(identifier != PAPER_ID) return;

                Log.Info($"[PaperUI] {player.ClientName} ({player.ClientId}) closed \"{identifier}\" without pressing a button.");
            };
        }
    }
}
