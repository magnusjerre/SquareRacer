using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jerre
{
    public class GameManager : MonoBehaviour
    {
        public PlayerSettingsComponent playerPrefab;

        public Dictionary<int, PlayerSettingsComponent> instantiatedPlayers;
        private Dictionary<PlayerSettingsComponent, int> playerScores;
        private SpawnPoint[] spawnPoints;

        private int nextSpawnIndex = 0;

        private void Awake()
        {
            instantiatedPlayers = new Dictionary<int, PlayerSettingsComponent>();
            playerScores = new Dictionary<PlayerSettingsComponent, int>();
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
            playerScores.Add(player, 0);
        }

        void RemovePlayer(int playerNumber) {
            if (!instantiatedPlayers.ContainsKey(playerNumber)) {
                return;
            }
            var player = instantiatedPlayers[playerNumber];
            instantiatedPlayers.Remove(playerNumber);
            playerScores.Remove(player);
            Destroy(player.gameObject);
        }

        public void RegisterPointsForPlayer(PlayerSettingsComponent player, int points)
        {
            playerScores[player] = playerScores[player] + points;
            Debug.LogFormat("Player[{0}] scored {1} point. Total points: {2}", player.playerNumber, points, playerScores[player]);
        }
    }
}
