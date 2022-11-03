using AMP;
using AMP.Network.Packets.Implementation;
using System;
using UnityEngine;

namespace AMP_Server.Commands {
    internal class SayCommand : CommandHandler {

        public override string Process(string[] args) {
            ModManager.serverInstance.SendReliableToAll(
              new DisplayTextPacket("say", String.Join(" ", args), Color.yellow, Vector3.forward * 2, true, true, 20)
            );

            return "Server: " + String.Join(" ", args);
        }

        public override string GetHelp() {
            return "Shows the status of the server.";
        }
    }
}
