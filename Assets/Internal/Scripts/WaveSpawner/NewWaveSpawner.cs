using System;
using System.Collections;
using Internal.Scripts.Enemyes;
using Internal.Scripts.LocalStorage;
using Internal.Scripts.Manager_Controller;
using Internal.Scripts.UI;
using NaughtyAttributes;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Internal.Scripts.WaveSpawner
{
    public class NewWaveSpawner : MonoBehaviour
    {
        //[SerializeField] private Level[] _levels;

        [SerializeField] private DiContainer _container;
        private EnemyController _enemyController;
        private WaveTimerButton _waveTimerButton;

        [SerializeField, Expandable] private LevelMapConfig _levelMapConfig;

        public LevelMapConfig LevelMapConfig
        {
            get => _levelMapConfig;
        }

        [SerializeField] private Transform[] spawners;

        private int _currentLevel;

        public int CurrentLevel
        {
            get => _currentLevel;
        }

        private int _currentWave;

        public int CurrentWave
        {
            get => _currentWave;
        }

        private int _currentMiniWave;

        public int CurrentMiniWave
        {
            get => _currentMiniWave;
        }

        private int _currentGroupOfEnemies;

        public int CurrentGroupOfEnemies
        {
            get => _currentGroupOfEnemies;
        }

        private IStorageService _storageService;
        private Progress progress;
        private LevelManager _levelManager;

        [SerializeField] private bool canWaitSpawnDelay = true;

        [SerializeField] private GameObject[] _enemies;

        public event Action OnNextMiniWave;
        public event Action OnWaveEnd;
        public event Action OnWin;
        public event Action OnNextLevel;

        [Inject]
        private void Construct(DiContainer container, EnemyController enemyController, WaveTimerButton waveTimerButton,
            IStorageService storageService, LevelManager levelManager)
        {
            _container = container;
            _waveTimerButton = waveTimerButton;
            _storageService = storageService;
            _levelManager = levelManager;
            _enemyController = enemyController;
        }

        private void Start()
        {
            _storageService.Load<Progress>(SaveKey.LevelIndex, data =>
            {
                progress = data ?? new Progress(0);
                _currentLevel = progress.index;
                Debug.Log(_currentLevel);
            });

            StartCoroutine(SpawnEnemyInWave());
        }

        private void OnEnable()
        {
            _enemyController.OnEnemiesAreNull += TryFinishLevel;
            _waveTimerButton.OnTimerStart += LaunchWave;
            _waveTimerButton.OnTimerEnd += ResetSpawnDelay;
            _levelManager.OnNextLevel += NextLevel;
        }

        private void OnDisable()
        {
            _enemyController.OnEnemiesAreNull -= TryFinishLevel;
            _waveTimerButton.OnTimerStart -= LaunchWave;
            _waveTimerButton.OnTimerEnd -= ResetSpawnDelay;
            _levelManager.OnNextLevel -= NextLevel;
        }

        private IEnumerator SpawnEnemyInWave()
        {
            if (_currentMiniWave <=
                _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves.Length - 1)
            {
                if (canWaitSpawnDelay)
                {
                    yield return new WaitForSeconds(_levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave]
                        .MiniWaves[_currentMiniWave].SpawnDelay);
                }
                else yield return null;
                
                SpawnMiniWave();
                Debug.Log("qwert");
                canWaitSpawnDelay = true;
            }
            else
            {
                Debug.Log("WaveEnd");
                OnWaveEnd?.Invoke();
            }
        }

        private void SpawnMiniWave()
        {
            _currentGroupOfEnemies = 0;
            for (int i = _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave]
                     .GroupOfEnemies.Length;
                 i > 0;
                 i--)
            {
                for (int j = 0;
                     j < _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave]
                         .GroupOfEnemies[_currentGroupOfEnemies].Count;
                     j++)
                {
                    SpawnOneEnemyInWave();
                }

                _currentGroupOfEnemies++;
            }

            OnNextMiniWave?.Invoke();
            _currentMiniWave++;
            StartCoroutine(SpawnEnemyInWave());
        }

        private int GetEnemiesLeftToSpawn()
        {
            int enemiesLeftToSpawn = 0;

            _currentGroupOfEnemies = 0;
            for (int i = _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave]
                     .GroupOfEnemies.Length;
                 i > 0;
                 i--)
            {
                for (int j = 0;
                     j < _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave]
                         .GroupOfEnemies[_currentGroupOfEnemies].Count;
                     j++)
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
            _container.InstantiatePrefab(
                GetEnemy(_levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave]
                    .GroupOfEnemies[_currentGroupOfEnemies].Enemy), GetSpawnPosition(), Quaternion.identity, transform);
            _enemyController.AddEnemy(GetEnemy(_levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves[_currentMiniWave].GroupOfEnemies[_currentGroupOfEnemies].Enemy));
        }

        private void LaunchWave()
        {
            if (_currentMiniWave == _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves.Length &&
                _currentWave != _levelMapConfig.LevelConfigs[_currentLevel].Waves.Length - 1)
            {
                Debug.Log("LaunchWave");
                _currentMiniWave = 0;
                _currentGroupOfEnemies = 0;
                _currentWave++;
                StartCoroutine(SpawnEnemyInWave());
            }
        }

        private void TryFinishLevel()
        {
            if (_currentMiniWave == _levelMapConfig.LevelConfigs[_currentLevel].Waves[_currentWave].MiniWaves.Length &&
                    _currentWave == _levelMapConfig.LevelConfigs[_currentLevel].Waves.Length - 1)
            {
                OnWin?.Invoke();
                Debug.Log("<color=yellow>Victory!!!</color>");
            }
        }

        public void NextLevel()
        {
            _currentLevel++;
            progress.index = _currentLevel;
            _storageService.Save(SaveKey.LevelIndex, progress);
            OnNextLevel?.Invoke();
        }

        private void ResetSpawnDelay()
        {
            // StopCoroutine(SpawnEnemyInWave());
            // SpawnMiniWave();
            canWaitSpawnDelay = false;
            Debug.Log("qwert");
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
}