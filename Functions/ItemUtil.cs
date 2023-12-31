using AMP.Network.Packets.Implementation;
using UnityEngine;

namespace AMP.DedicatedServer.Functions {
    public static class ItemUtil {
        public static int SpawnItem(string itemId, Vector3 position) {
            return SpawnItem(itemId, position, Vector3.zero);
        }

        public static int SpawnItem(string itemId, Vector3 position, Vector3 rotation) {
            ItemSpawnPacket csp = new ItemSpawnPacket() {
                type = itemId,
                clientsideId = 0,
                itemId = 0,
                position = position,
                rotation = rotation,
                isMagicProjectile = false
            };
            csp.ProcessServer(ModManager.serverInstance.netamiteServer, Network.Data.ClientData.SERVER);

            return ModManager.serverInstance.CurrentItemId;
        }
    }
}
