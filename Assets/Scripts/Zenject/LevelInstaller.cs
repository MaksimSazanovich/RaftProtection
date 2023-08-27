using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private RaftHealth _raftHealth;
    [SerializeField] private EnemyController _enemyController;

    public override void InstallBindings()
    {
        Container.Bind<RaftHealth>().FromInstance(_raftHealth).AsSingle();
        Container.Bind<EnemyController>().FromInstance(_enemyController).AsSingle();
    }
}