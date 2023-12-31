using AMP.Network.Packets.Implementation;
using UnityEngine;
using static ThunderRoad.AnimationData.Clip;

namespace AMP.DedicatedServer.Functions {
    public static class CreatureUtil {

        public static int SpawnCreature(string type, string container, Vector3 pos, byte factionId = 0) {
            if(type == null || type.Length == 0) type = "HumanMale";
            if(container == null || container.Length == 0) container = "BanditRogue";

            CreatureSpawnPacket csp = new CreatureSpawnPacket() {
                type = type,
                clientsideId = 0,
                position = pos,
                container = container,
                factionId = factionId,
                rotationY = 0,
                health = 1000,
                maxHealth = 1000,
                height = 2,
                colors = new Color[] {
                    Color.white, Color.white, Color.white,
                    Color.white, Color.white, Color.white,
                },
                equipment = new string[] {
                    "ApparelShirt04",
                    "ApparelCivilianLegs"
                }
            };
            csp.ProcessServer(ModManager.serverInstance.netamiteServer, Network.Data.ClientData.SERVER);

            return ModManager.serverInstance.CurrentCreatureId;
        }
    }
}
