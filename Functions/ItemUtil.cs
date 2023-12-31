using AMP.Network.Packets.Implementation;
using UnityEngine;

namespace AMP.DedicatedServer.Functions {
    public static class ItemUtil {
        public static void SpawnItem(string itemId, Vector3 position) {
            SpawnItem(itemId, position, Vector3.zero);
        }

        public static void SpawnItem(string itemId, Vector3 position, Vector3 rotation) {
            ModManager.serverInstance.netamiteServer.SendToAll(new ItemSpawnPacket( itemId: 0
                                                                   , type: itemId
                                                                   , category: (byte) 0
                                                                   , 0
                                                                   , position
                                                                   , rotation
                                                                   , false
                                                                   )
                                              );
        }
    }
}
