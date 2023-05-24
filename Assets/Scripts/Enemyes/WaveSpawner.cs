using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Waves[] waves;
    private int currentEnemyIndex;
    private int currentWaveIndex;
    private int enemiesLeftToSpawn;
    [SerializeField] private GameObject[] spawners;

    private GameObject previousSpawner;

    private void Start()
    {
        enemiesLeftToSpawn = waves[0].WaveSettings.Length;

        LaunchWave();
    }

    //private void Update()
    //{
    //    Debug.Log(currentWaveIndex);
    //}

    private IEnumerator SpawnEnemyInWave()
    {
        if(enemiesLeftToSpawn > 0)
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
        waves[currentWaveIndex].WaveSettings[currentEnemyIndex].neededSpawner = spawners[Random.Range(0, spawners.Length - 1)];
        if (previousSpawner != waves[currentWaveIndex].WaveSettings[currentEnemyIndex].neededSpawner)
        {
            previousSpawner = waves[currentWaveIndex].WaveSettings[currentEnemyIndex].neededSpawner;
            Instantiate(waves[currentWaveIndex].WaveSettings[currentEnemyIndex].Enemy, waves[currentWaveIndex].WaveSettings[currentEnemyIndex].neededSpawner.transform.position, Quaternion.identity);
            enemiesLeftToSpawn--;
        }
        else SpawnOneEnemyInWave();
    }

    public void LaunchWave()
    {
        StartCoroutine(SpawnEnemyInWave());
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
    internal GameObject neededSpawner;
    [SerializeField] private float spawnDelay;
    public float SpawnDelay { get => spawnDelay; }
}
