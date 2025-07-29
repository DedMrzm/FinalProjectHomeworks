using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilitis.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerfomer>(CreateCoroutinesPerformer);

            container.RegisterAsSingle(CreateConfigsProviderService);

            container.RegisterAsSingle(CreateResourcesAssetsLoader);

            container.RegisterAsSingle(CreateSceneLoaderService);

            container.RegisterAsSingle(CreateSceneSwitcherService);
            
            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c) => new ResourcesAssetsLoader();

        private static CoroutinesPerfomer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerfomer coroutinesPerfomerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerfomer>("Utilities/CoroutinesPerformer");

            return Object.Instantiate(coroutinesPerfomerPrefab);
        }

        private static StandartLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            StandartLoadingScreen standartLoadingScreen = resourcesAssetsLoader
                .Load<StandartLoadingScreen>("Utilities/StandardLoadingScreen");

            return Object.Instantiate(standartLoadingScreen);
        }
    }
}
