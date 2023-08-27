using System.Collections;
using UnityEngine;
using Zenject;

public class NewWaveSpawner : MonoBehaviour
{
    //[SerializeField] private Level[] _levels;

    [SerializeField] private DiContainer _container;
    private EnemyController _enemyController;

    [SerializeField] private LevelMapConfig _levelMapConfig;

    [SerializeField] private Transform[] spawners;

    private int _enemiesLeftToSpawn;

    private int _currentLevel;
    private int _currentWave;
    private int _currentMiniWave;
    private int _currentGroupOfEnemies;

    [SerializeField] private GameObject[] _enemies;

    [Inject]
    private void Construct(DiContainer container, EnemyController enemyController)
    {
        _container = container;
        _enemyController = enemyController;
    }

    private void Start()
    {
        _enemiesLeftToSpawn = GetEnemiesLeftToSpawn();
        StartCoroutine(SpawnEnemyInWave());
    }

    private void OnEnable()
    {
        _enemyController.OnEnemiesAreNull += LaunchWave;
    }

    private void OnDisable()
    {
        _enemyController.OnEnemiesAreNull -= LaunchWave;
    }

    private IEnumerator SpawnEnemyInWave()
    {
        if (_currentMiniWave <= _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves.Length - 1)
        {
            Debug.Log("_currentMiniWave: " + _currentMiniWave);
            Debug.Log("_levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves.Length - 1: " + (_levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves.Length - 1));
            yield return new WaitForSeconds(_levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave].SpawnDelay);
            SpawnMiniWave();
            Debug.Log("qwert");
            StartCoroutine(SpawnEnemyInWave());
        }
        else
        {
            Debug.Log("WaveEnd");            
        }
    }

    private void SpawnMiniWave()
    {
        _currentGroupOfEnemies = 0;
        for (int i = _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave].GroupOfEnemies.Length; i > 0; i--)
        {
            for (int j = 0; j < _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave].GroupOfEnemies[_currentGroupOfEnemies].Count; j++)
            {
                SpawnOneEnemyInWave();
            }
            _currentGroupOfEnemies++;
        }
        _currentMiniWave++;
    }

    private int GetEnemiesLeftToSpawn()
    {
        int enemiesLeftToSpawn = 0;

        _currentGroupOfEnemies = 0;
        for (int i = _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave].GroupOfEnemies.Length; i > 0; i--)
        {
            for (int j = 0; j < _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave].GroupOfEnemies[_currentGroupOfEnemies].Count; j++)
            {
                enemiesLeftToSpawn++;
            }
            _currentGroupOfEnemies++;
        }
        _currentGroupOfEnemies = 0;
        return enemiesLeftToSpawn;
    }
    public void SpawnOneEnemyInWave()
    {
        //_container.InstantiatePrefab(waves[currentWaveIndex].WaveSettings[currentEnemyIndex].Enemy, GetSpawnPosition(), Quaternion.identity, transform);
        _container.InstantiatePrefab(GetEnemy(_levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave].GroupOfEnemies[_currentGroupOfEnemies].Enemy), GetSpawnPosition(), Quaternion.identity, transform);
        _enemiesLeftToSpawn--;
        _enemyController.AddEnemy(GetEnemy(_levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave].GroupOfEnemies[_currentGroupOfEnemies].Enemy));
    }

    private void LaunchWave()
    {
        if (_currentMiniWave == _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves.Length && _currentWave != _levelMapConfig.LevelConfigs[_currentLevel].Waves.Length - 1)
        {
            Debug.Log("LaunchWave");
            _currentMiniWave = 0;
            _currentGroupOfEnemies = 0;
            _currentWave++;
            StartCoroutine(SpawnEnemyInWave());
        }
        else
            Debug.Log("Victory!!!");
    }
    public GameObject GetEnemy(EnemiesTypes enemy)
    {
        return _enemies[(int)enemy];
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