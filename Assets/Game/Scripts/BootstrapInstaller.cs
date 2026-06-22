using Analytics;
using AssetLoader;
using Save;
using Zenject;

namespace Game
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapEntryPoint>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LoadedAssets>().AsSingle();
            Container.Bind<IAssetLoader>().To<LocalAssetLoader>().AsSingle();
            Container.Bind<IAnalytics>().To<FireAnalytics>().AsSingle().NonLazy();
            Container.Bind<ISaveLoader>().To<SaveLoader>().AsSingle();
        }
    }
}