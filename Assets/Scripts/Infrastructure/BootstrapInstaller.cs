using ShapesGame.Services.Asset;
using ShapesGame.Services.StaticData;
using Zenject;

namespace ShapesGame.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        }
    }
}
