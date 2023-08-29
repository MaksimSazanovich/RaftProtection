using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private RaftHealth _raftHealth;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private NewWaveSpawner _waveSpawner;
    [SerializeField] private WaveTimerButton _waveTimerButton;

    public override void InstallBindings()
    {
        Container.Bind<RaftHealth>().FromInstance(_raftHealth).AsSingle();
        Container.Bind<EnemyController>().FromInstance(_enemyController).AsSingle();
        Container.Bind<NewWaveSpawner>().FromInstance(_waveSpawner).AsSingle();
        Container.Bind<WaveTimerButton>().FromInstance(_waveTimerButton).AsSingle();
    }
}