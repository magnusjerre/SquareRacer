﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jerre
{
    public class GameManager : MonoBehaviour
    {
        public PlayerSettingsComponent playerPrefab;
        public PlayerScoreUIElement playerScoreUIPrefab;

        public Canvas scoreCanvas;
        public Dictionary<int, PlayerSettingsComponent> instantiatedPlayers;
        private Dictionary<PlayerSettingsComponent, PlayerScoreComponent> playerScores;
        private SpawnPoint[] spawnPoints;

        private int nextSpawnIndex = 0;
        public int maxScore = 10;

        public bool playing = false;

        private void Awake()
        {
            instantiatedPlayers = new Dictionary<int, PlayerSettingsComponent>();
            playerScores = new Dictionary<PlayerSettingsComponent, PlayerScoreComponent>();
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

            //Add score UI element and score component
            var playerScore = Instantiate(playerScoreUIPrefab, scoreCanvas.transform);
            playerScore.numberInLine = playerNumber - 1;
            var playerScoreComponent = playerScore.GetComponent<PlayerScoreComponent>();
            playerScoreComponent.maxScore = maxScore;
            playerScores.Add(player, playerScoreComponent);
        }

        void RemovePlayer(int playerNumber) {
            if (!instantiatedPlayers.ContainsKey(playerNumber)) {
                return;
            }
            var player = instantiatedPlayers[playerNumber];
            instantiatedPlayers.Remove(playerNumber);

            var playerScore = playerScores[player];
            playerScores.Remove(player);

            Destroy(player.gameObject);
            Destroy(playerScore.gameObject);

            var counter = 0;
            foreach (var score in playerScores.Values) {
                var uiElement = score.GetComponent<PlayerScoreUIElement>();
                uiElement.SetNumberInLine(counter++);
            }
        }

        public void RegisterPointsForPlayer(PlayerSettingsComponent player, int points)
        {
            var score = playerScores[player];
            score.playerScore += points;
            score.GetComponent<PlayerScoreUIElement>().SetScore(score.playerScore, maxScore);
            Debug.LogFormat("Player[{0}] scored {1} point. Total points: {2}", player.playerNumber, points, playerScores[player]);

            if (score.playerScore == maxScore) {
                ResetScores();
            }
        }

        private void ResetScores() {
            foreach (var player in playerScores.Values) {
                player.playerScore = 0;
                player.maxScore = maxScore;
                player.GetComponent<PlayerScoreUIElement>().SetScore(0, maxScore);
            }
        }
    }
}
