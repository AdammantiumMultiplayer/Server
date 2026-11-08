using AMP.Network.Packets.Implementation;
using System.Linq;
using UnityEngine;

namespace AMP.DedicatedServer.Functions {
    public static class CreatureUtil {

        public static int SpawnCreature(string type, string container, Vector3 pos, float rotationY = 0, byte factionId = 0, int health = 500) {
            return SpawnCreature( type
                                , container
                                , pos
                                , new Color[] {
                                        Color.white, Color.white, Color.white,
                                        Color.white, Color.white, Color.white,
                                    }
                                , new string[] {}
                                ,  rotationY
                                , factionId
                                , health);
        }

        public static int SpawnCreature(string type, string container, Vector3 pos, Color[] colors, string[] equipment, float rotationY = 0, byte factionId = 0, int health = 500) {
            if(type == null || type.Length == 0) type = "HumanMale";
            if(container == null || container.Length == 0) container = "BanditRogue";
            
            if(colors == null) colors = new Color[0];
            if(equipment == null) equipment = new string[0];

            while(colors.Length < 6) {
                colors.Append(Color.white);
            }

            CreatureSpawnPacket csp = new CreatureSpawnPacket() {
                type = type,
                clientsideId = 0,
                position = pos,
                container = container,
                factionId = factionId,
                rotationY = rotationY,
                health = health,
                maxHealth = health,
                height = 2,
                colors = colors,
                equipment = equipment
            };
            csp.ProcessServer(ModManager.serverInstance.netamiteServer, Network.Data.ClientData.SERVER);

            return ModManager.serverInstance.CurrentCreatureId;
        }
    }
}
