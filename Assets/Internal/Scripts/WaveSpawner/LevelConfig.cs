using System;
using Internal.Scripts.Enemyes;
using UnityEngine;

namespace Internal.Scripts.WaveSpawner
{
    [CreateAssetMenu(fileName = "Level Config", menuName = "Config/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private Wave[] _waves;
        public Wave[] Waves { get => _waves; }
    }

    [Serializable]
    public class Wave
    {
        [SerializeField] MiniWave[] _miniWaves;
        public MiniWave[] MiniWaves { get => _miniWaves; }
    }

    [Serializable]
    public class MiniWave
    {
        [SerializeField] private GroupOfEnemies[] _groupOfEnemies;
        public GroupOfEnemies[] GroupOfEnemies { get => _groupOfEnemies; }

        [SerializeField] private float _spawnDelay;
        public float SpawnDelay { get => _spawnDelay; }
    }

    [Serializable]
    public class GroupOfEnemies
    {
        [SerializeField] private EnemiesTypes _enemy;
        public EnemiesTypes Enemy { get => _enemy; }

        [SerializeField] private int _count = 1;
        public int Count { get => _count; }
    }
}