using UnityEngine;

namespace Internal.Scripts.WaveSpawner
{
    [CreateAssetMenu(fileName = "Level Map Config", menuName = "Config/Level Map Config")]
    public class LevelMapConfig : ScriptableObject
    {
        [SerializeField] private LevelConfig[] _levelConfigs;
        public LevelConfig[] LevelConfigs { get => _levelConfigs; }
    }
}