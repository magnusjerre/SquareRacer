using UnityEngine;
using System.Collections;
using Unity.Entities;

namespace Jerre
{
    public class ExplosionEMPSystem : ComponentSystem
    {
        struct ExplosionData {
            public ExplosionEmpComponent explosion;
        }

        struct PlayerData {
            public PlayerSettingsComponent settings;
        }

        protected override void OnUpdate()
        {
            var dt = Time.deltaTime;

            var explosions = GetEntities<ExplosionData>();
            if (explosions.Length == 0) return;

            var players = GetEntities<PlayerData>();
            if (players.Length == 0) return;
            foreach (var player in players)
            {
                var explosionIndex = FindExplosionIndexForPlayer(player.settings.playerNumber, explosions);
                if (explosionIndex == -1) {
                    player.settings.receiveInput = true;
                } else {
                    var explosion = explosions[explosionIndex];
                    explosion.explosion.timeLeftOfExplosionEffect -= dt;
                    if (explosion.explosion.timeLeftOfExplosionEffect <= 0f)
                    {
                        player.settings.receiveInput = true;
                        Object.Destroy(explosion.explosion.gameObject);
                    }
                    else
                    {
                        player.settings.receiveInput = false;
                    }
                }
            }
        }

        private int FindExplosionIndexForPlayer(int playerNumber, ComponentGroupArray<ExplosionData> array) {
            for (int i = 0; i < array.Length; i++) {
                if (array[i].explosion.playerNumber == playerNumber) {
                    return i;
                }
            }
            return -1;
        }
    }
}
