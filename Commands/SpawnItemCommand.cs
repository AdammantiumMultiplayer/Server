using AMP.DedicatedServer.Functions;
using UnityEngine;

namespace AMP.DedicatedServer.Commands {
    internal class SpawnItemCommand : CommandHandler {

        public override string[] ALIASES => new string[] { "si", "spawnitem" };
        public override string   HELP    => "Spawns the given Item at the given position";

        public override string Process(string[] args) {
            if(args.Length == 4) {
                ItemUtil.SpawnItem(args[0], new Vector3(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3])));

                return $"Spawned Item: {args[0]} at {args[1]} {args[2]} {args[3]}.";
            } else {
                return "Usage: si <item> <x> <y> <z>";
            }
        }
    }
}
