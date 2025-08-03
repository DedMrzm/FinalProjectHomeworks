using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.DataManagment;
using Assets._Project.Develop.Runtime.Utilitis.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;

        private PlayerDataProvider _playerDataProvider;
        private ICoroutinesPerformer _coroutinesPerformer;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация сцены меню");

            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Старт сцены меню");
        }

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerfomer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerfomer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(GameModes.Chars)));
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerfomer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerfomer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(GameModes.Digits)));
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
                Debug.Log("Сохранение было вызвано");
            }
        }
    }
}
