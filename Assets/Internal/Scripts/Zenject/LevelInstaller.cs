using Internal.Scripts.Manager_Controller;
using Internal.Scripts.Raft;
using Internal.Scripts.UI;
using Internal.Scripts.WaveSpawner;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Zenject
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private RaftHealth _raftHealth;


        [SerializeField] private EnemyController _enemyController;
        [SerializeField] private NewWaveSpawner _waveSpawner;
        [SerializeField] private WaveTimerButton _waveTimerButton;
        [SerializeField] private TouchDetector _touchDetector;
        [SerializeField] private Raft.Raft _raft;
        [SerializeField] private LevelManager _levelManager;

        public override void InstallBindings()
        {
            Container.Bind<RaftHealth>().FromInstance(_raftHealth).AsSingle();
            Container.Bind<Raft.Raft>().FromInstance(_raft).AsSingle();

            Container.Bind<EnemyController>().FromInstance(_enemyController).AsSingle();
            Container.Bind<NewWaveSpawner>().FromInstance(_waveSpawner).AsSingle();
            Container.Bind<WaveTimerButton>().FromInstance(_waveTimerButton).AsSingle();
            Container.Bind<TouchDetector>().FromInstance(_touchDetector).AsSingle();
            Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle();
        }
    }
}