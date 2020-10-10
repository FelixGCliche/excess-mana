
using System;
using Script.Util;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;

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
        private int[] enemiesToSpawn;
        private int[] currentWaveContent;
        private int activeSpawnPoints;
        private int liveEnemyCount;
        private int spawnCurrency;
        private int waveLeft;
        private float timeBeforeSpawn;
        private Random rndEnemy;
        
        private void Start()
        {
            spawnPoints = new [] {northSpawnpoint, eastSpawnpoint,
                southSpawnpoint, westSpawnpoint};
            
            enemies = new [] {fireEnemyPrefab, earthEnemyPrefab,
                windEnemyPrefab, waterEnemyPrefab};
            
            enemiesToSpawn = new int[spawnPoints.Length];
            currentWaveContent = new int[spawnPoints.Length];

            ResetEnemiesToSpawn();
            ResetCurentWaveContent();

            activeSpawnPoints = 1;
            liveEnemyCount = 0;
            spawnCurrency = initialSpawnCurrency;
            waveLeft = 0;
            timeBeforeSpawn = 0f;
            rndEnemy = new Random();
        }

        private void Update()
        {
            timeBeforeSpawn -= Time.deltaTime;
            if (timeBeforeSpawn < 0)
            {
                SpawnEnemy(CardinalDirection.NORTH);
                SpawnEnemy(CardinalDirection.EAST);
                SpawnEnemy(CardinalDirection.SOUTH);
                SpawnEnemy(CardinalDirection.WEST);
                timeBeforeSpawn = timeBetweenSpawn;
            }
            
        }

        private void SpawnEnemy(CardinalDirection direction)
        {
            if (currentWaveContent[(int) direction] > 0)
            {
                Instantiate(enemies[rndEnemy.Next(0, enemies.Length)], spawnPoints[(int) direction].transform.position,
                    spawnPoints[(int) direction].transform.rotation);
                currentWaveContent[(int) direction]--;
            }
        }

        private void ResetEnemiesToSpawn()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                enemiesToSpawn[i] = 0;
            }
        }

        private void ResetCurentWaveContent()
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                currentWaveContent[i] = 0;
            }
        }

        private void DecrementLiveEnemyCount()
        {
            liveEnemyCount--;
        }

        public void NextDay()
        {
            spawnCurrency += spawnCurrencyProgression;
        }

        public void CalculateNextNightContent(int waveAmount, WaveDifficulty difficulty)
        {
            waveLeft = waveAmount;
            if (difficulty == WaveDifficulty.NORMAL)
            {
                int currencyLeft = spawnCurrency;

                while (currencyLeft > 0)
                {
                    AddEnemyToSpawnArray(currencyLeft % activeSpawnPoints);
                    currencyLeft--;
                }
            }
            else
            {
                if (activeSpawnPoints < 4)
                    activeSpawnPoints++;
                spawnCurrency *= hardWaveCurrencyMultiplier;
                int currencyLeft = spawnCurrency;

                while (currencyLeft > 0)
                {
                    AddEnemyToSpawnArray(currencyLeft % activeSpawnPoints);
                    currencyLeft--;
                }

                spawnCurrency /= hardWaveCurrencyMultiplier;
            }
        }

        private void AddEnemyToSpawnArray(int direction)
        {
            enemiesToSpawn[direction]++;
        }

        public void StartWave()
        {
            int enemyAmount = enemiesToSpawn[0] / waveLeft;
            enemiesToSpawn[0] -= enemyAmount;
            currentWaveContent[0] += enemyAmount;
            
            enemyAmount = enemiesToSpawn[1] / waveLeft;
            enemiesToSpawn[1] -= enemyAmount;
            currentWaveContent[1] += enemyAmount;
            
            enemyAmount = enemiesToSpawn[2] / waveLeft;
            enemiesToSpawn[2] -= enemyAmount;
            currentWaveContent[2] += enemyAmount;
            
            enemyAmount = enemiesToSpawn[3] / waveLeft;
            enemiesToSpawn[3] -= enemyAmount;
            currentWaveContent[3] += enemyAmount;

            timeBeforeSpawn = 0f;
            waveLeft--;
        }
    }
}