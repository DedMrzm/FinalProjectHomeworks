using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Gameplay.Utils;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.UI.Gameplay.CymbolGenerator;
using Assets._Project.Develop.Runtime.UI.Gameplay.InputHandler;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilitis.AssetsManagment;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private static UpdateService _updateService;

        public static void Process(DIContainer container, UpdateService updateService)
        {
            _updateService = updateService;

            Debug.Log("Процесс регистрации сервисов на сцене геймплея");
            container.RegisterAsSingle(CreateSymbolsGenerator);
            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();
            container.RegisterAsSingle(CreateGameplayPresentersFactory);
            container.RegisterAsSingle(CreateInputTextService);
            container.RegisterAsSingle(CreateTutorialService);
        }

        public static SymbolsGenerator CreateSymbolsGenerator(DIContainer c)
            => new SymbolsGenerator(c.Resolve<ConfigsProviderService>());


        public static TutorialService CreateTutorialService(DIContainer c)
            => new TutorialService(
                c.Resolve<WalletService>(),
                c.Resolve<ICoroutinesPerformer>());

        public static InputTextService CreateInputTextService(DIContainer c)
        {
            InputTextService inputTextService = new InputTextService();
            _updateService.AddUpdatableService(inputTextService);

            return inputTextService;
        }

        public static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
            => new GameplayPresentersFactory(c);

        public static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot gameplayUIRootPrefab = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            Debug.Log(gameplayUIRootPrefab);

            return Object.Instantiate(gameplayUIRootPrefab);
        }

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();

            GameplayScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreenView, uiRoot.HUDLayer);

            GameplayScreenPresenter presenter = new GameplayScreenPresenter(
                view,
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<GameplayPresentersFactory>(),
                c.Resolve<SymbolsGenerator>(),
                _updateService);

            return presenter;
        }
    }
}
