using Internal.Scripts.LocalStorage;
using Zenject;

namespace Internal.Scripts.Zenject
{
    public sealed class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings() =>
            BindServices();


        private void BindServices()
        {
            Container
                .Bind<IStorageService>()
                .To<JsonToFileStorageService>()
                .AsSingle()
                .NonLazy();
        }
    }
}