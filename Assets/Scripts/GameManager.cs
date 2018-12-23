using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jerre
{
    public class GameManager : MonoBehaviour
    {
        public PlayerSettingsComponent playerPrefab;

        public Dictionary<int, PlayerSettingsComponent> instantiatedPlayers;
        private SpawnPoint[] spawnPoints;

        private int nextSpawnIndex = 0;

        private void Awake()
        {
            instantiatedPlayers = new Dictionary<int, PlayerSettingsComponent>();
        }

        // Use this for initialization
        void Start()
        {
            spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();
        }

        // Update is called once per frame
        void Update()
        {
            var playerJoin1 = Input.GetButtonDown(InputNames.JOIN + 1);
            var playerJoin2 = Input.GetButtonDown(InputNames.JOIN + 2);

            if (playerJoin1) {
                SpawnPlayer(1);
            }
            if (playerJoin2) {
                SpawnPlayer(2);
            }

            var playerLeave1 = Input.GetButtonDown(InputNames.LEAVE + 1);
            var playerLeave2 = Input.GetButtonDown(InputNames.LEAVE + 2);

            if (playerLeave1) {
                RemovePlayer(1);
            }
            if (playerLeave2) {
                RemovePlayer(2);
            }

        }

        void SpawnPlayer(int playerNumber) {
            if (instantiatedPlayers.ContainsKey(playerNumber)) {
                return;
            }
            var spawnPoint = spawnPoints[nextSpawnIndex];
            nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;

            var player = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
            player.playerNumber = playerNumber;
            instantiatedPlayers.Add(playerNumber, player);
        }

        void RemovePlayer(int playerNumber) {
            if (!instantiatedPlayers.ContainsKey(playerNumber)) {
                return;
            }
            var player = instantiatedPlayers[playerNumber];
            instantiatedPlayers.Remove(playerNumber);
            Destroy(player.gameObject);
        }
    }
}
