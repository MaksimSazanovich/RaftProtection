using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Enemyes
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private Waves[] waves;
        private int currentEnemyIndex;
        private int currentWaveIndex;
        private int enemiesLeftToSpawn;
        [SerializeField] private Transform[] spawners;
        [SerializeField] private DiContainer _container;

        [SerializeField] private GameObject[] _enemies;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }
        private void Start()
        {
            enemiesLeftToSpawn = waves[0].WaveSettings.Length;

            SetEnemyesIndex();

            LaunchWave();
        }

        private void SetEnemyesIndex()
        {
        
        }

        private IEnumerator SpawnEnemyInWave()
        {
            if (enemiesLeftToSpawn > 0)
            {
                yield return new WaitForSeconds(waves[currentWaveIndex].WaveSettings[currentEnemyIndex].SpawnDelay);
                SpawnOneEnemyInWave();
                currentEnemyIndex++;
                StartCoroutine(SpawnEnemyInWave());
            }
            else
            {
                if (currentWaveIndex < waves.Length - 1)
                {
                    currentWaveIndex++;
                    enemiesLeftToSpawn = waves[currentWaveIndex].WaveSettings.Length;
                    currentEnemyIndex = 0;
                    Debug.Log("qwert");
                }
            }
        }

        public void SpawnOneEnemyInWave()
        {
            //Instantiate();
            _container.InstantiatePrefab(waves[currentWaveIndex].WaveSettings[currentEnemyIndex].Enemy, GetSpawnPosition(), Quaternion.identity, transform);
            enemiesLeftToSpawn--;
        }

        public void LaunchWave()
        {
            StartCoroutine(SpawnEnemyInWave());
        }

        private Vector3 GetSpawnPosition()
        {
            int index = Random.Range(0, 2);
            float spawnPositionX;
            if (index == 0)
                spawnPositionX = spawners[0].position.x;
            else
                spawnPositionX = spawners[2].position.x;

            float spawnPositionY = Random.Range(spawners[0].position.y, spawners[1].position.y);

            return new Vector3(spawnPositionX, spawnPositionY, 0);
        }
    }

    [System.Serializable]
    public class Waves
    {
        [SerializeField] private WaveSettings[] waveSettings;
        public WaveSettings[] WaveSettings { get => waveSettings; }
    }

    [System.Serializable]
    public class WaveSettings
    {
        [SerializeField] private GameObject enemy;
        public GameObject Enemy { get => enemy; }

        [SerializeField] private float spawnDelay;
        public float SpawnDelay { get => spawnDelay; }

        [SerializeField] private EnemiesTypes _enemy;
        public EnemiesTypes _Enemy { get => _enemy; }

        [SerializeField] private List<int> count = new() { 0, 0, 0 };
    }
}