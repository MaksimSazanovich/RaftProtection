using AbyssMoth.Codebase.Infrastructure.Services.Storage;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    public override void InstallBindings() => BindServices();


    private void BindServices()
    {
        Container.Bind<IStorageService>().To<JsonToFileStorageService>().AsSingle();
    }
}