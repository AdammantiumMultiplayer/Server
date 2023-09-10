using AMP.Network.Data;
using System;
using UnityEngine;

namespace AMP.DedicatedServer.Commands {
    internal class SayCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "say" };
        public override string   HELP    => "Shows the specified message to all players.";

        public override string Process(string[] args) {
            foreach(ClientData client in ModManager.serverInstance.Clients) {
                client.ShowText("say", String.Join(" ", args), Color.yellow, 20);
            }

            return "Server: " + String.Join(" ", args);
        }
    }
}
