
using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Script.GameMaster.WaveSpawner
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private int initialSpawnCurrency = 10;
        [SerializeField] private int spawnCurrencyProgression = 5;
        [SerializeField] private int hardWaveCurrencyMultiplier = 2;
        [SerializeField] private float timeBetweenSpawn = 0.5f;
        [SerializeField] private GameObject fireEnemyPrefab;
        [SerializeField] private GameObject earthEnemyPrefab;
        [SerializeField] private GameObject windEnemyPrefab;
        [SerializeField] private GameObject waterEnemyPrefab;
        [SerializeField] private GameObject northSpawnpoint;
        [SerializeField] private GameObject eastSpawnpoint;
        [SerializeField] private GameObject southSpawnpoint;
        [SerializeField] private GameObject westSpawnpoint;

        private GameObject[] spawnPoints;
        private GameObject[] enemies;
        private int[,] enemiesToSpawn;
        private int[,] currentWaveContent;
        private int[] waveSpawnPoints;
        private int[] waveElements;
        private int liveEnemyCount;
        private int spawnCurrency;
        private int waveLeft;
        private float timeBeforeSpawn;
        
        private void Start()
        {
            spawnPoints = new [] {northSpawnpoint, eastSpawnpoint,
                southSpawnpoint, westSpawnpoint};
            
            enemies = new [] {fireEnemyPrefab, earthEnemyPrefab,
                windEnemyPrefab, waterEnemyPrefab};
            
            enemiesToSpawn = new int[spawnPoints.Length, enemies.Length];
            currentWaveContent = new int[spawnPoints.Length, enemies.Length];

            ResetEnemiesToSpawn();
            ResetCurentWaveContent();

            waveSpawnPoints = new[] {1, 0, 0, 0};
            waveElements = new[] {1, 0, 0, 0};

            liveEnemyCount = 0;
            spawnCurrency = initialSpawnCurrency;
            waveLeft = 0;
            timeBeforeSpawn = 0f;
            
            CalculateNextNightContent(WaveDifficulty.NORMAL);
        }

        private void Update()
        {
            if (currentWaveContent[0, 0] > 0)
            {
                timeBeforeSpawn -= Time.deltaTime;
                if (timeBeforeSpawn < 0)
                {
                    Instantiate(enemies[0], spawnPoints[0].transform.position, spawnPoints[0].transform.rotation);
                    currentWaveContent[0, 0]--;
                    timeBeforeSpawn = timeBetweenSpawn;
                }
            }
            
        }

        private void ResetEnemiesToSpawn()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                for (int j = 0; j < enemies.Length; j++)
                {
                    enemiesToSpawn[i, j] = 0;
                }
            }
        }

        private void ResetCurentWaveContent()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                for (int j = 0; j < enemies.Length; j++)
                {
                    currentWaveContent[i, j] = 0;
                }
            }
        }

        private void DecrementLiveEnemyCount()
        {
            liveEnemyCount--;
        }

        private void nextDay()
        {
            spawnCurrency += spawnCurrencyProgression;
        }

        private void CalculateNextNightContent(WaveDifficulty difficulty)
        {
            waveLeft = 5;
            if (difficulty == WaveDifficulty.NORMAL)
            {
                int currencyLeft = spawnCurrency;

                while (currencyLeft > 0)
                {
                    AddEnemyToSpawnArray();
                    currencyLeft--;
                }
            }
            else
            {
                spawnCurrency *= 2;
                int currencyLeft = spawnCurrency;

                while (currencyLeft > 0)
                {
                    
                    currencyLeft--;
                }

                spawnCurrency /= 2;
            }
        }

        private void AddEnemyToSpawnArray()
        {
            enemiesToSpawn[0, 0]++;
        }

        public void StartWave()
        {
            if (waveLeft == 1)
            {
                currentWaveContent = enemiesToSpawn;
                ResetEnemiesToSpawn();
            }
            else
            {
                int enemyAmount = enemiesToSpawn[0, 0] / waveLeft;
                enemiesToSpawn[0, 0] -= enemyAmount;
                currentWaveContent[0, 0] += enemyAmount;
            }

            timeBeforeSpawn = 0f;
            waveLeft--;
        }
    }
}