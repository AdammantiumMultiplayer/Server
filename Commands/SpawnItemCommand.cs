using AMP;
using AMP.Network.Data;
using AMP.Network.Packets.Implementation;
using System;
using UnityEngine;

namespace AMP_Server.Commands {
    internal class SpawnItemCommand : CommandHandler {

        internal override string[] aliases => new string[] { "si", "spawnitem" };

        public override string Process(string[] args) {
            if(args.Length == 4) {
                ModManager.serverInstance.OnPacket( ClientData.SERVER
                                                  , new ItemSpawnPacket( itemId: 0
                                                                       , type: args[0]
                                                                       , category: (byte) 0
                                                                       , 0
                                                                       , new Vector3( float.Parse(args[1])
                                                                                    , float.Parse(args[2])
                                                                                    , float.Parse(args[3])
                                                                                    )
                                                                       , Vector3.zero
                                                                       )
                                                  );

                return $"Spawned Item: {args[0]} at {args[1]} {args[2]} {args[3]}.";
            } else {
                return "Usage: si <item> <x> <y> <z>";
            }
        }

        public override string GetHelp() {
            return "Shows the status of the server.";
        }
    }
}
