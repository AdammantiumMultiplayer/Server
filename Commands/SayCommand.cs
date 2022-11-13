using AMP.Network.Packets.Implementation;
using System;
using UnityEngine;

namespace AMP.DedicatedServer.Commands {
    internal class SayCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "say" };
        public override string   HELP    => "Shows the specified message to all players.";

        public override string Process(string[] args) {
            ModManager.serverInstance.SendReliableToAll(
              new DisplayTextPacket("say", String.Join(" ", args), Color.yellow, Vector3.forward * 2, true, true, 20)
            );

            return "Server: " + String.Join(" ", args);
        }
    }
}
