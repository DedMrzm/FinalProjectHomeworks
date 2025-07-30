using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private ConfigsProviderService _configsProviderService;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            _configsProviderService = _container.Resolve<ConfigsProviderService>();

            GameplayContextRegistrations.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Начинается подгрузка конфигов");

            yield return _configsProviderService.LoadAsync();
            
            Debug.Log("Подгрузка конфигов завершена");

            GameModeConfig config = _container.Resolve<GameModeConfig>();

            Debug.Log($"Уровень: {config.GameMode}");

            Debug.Log("Инициализация геймплейной сцены");

            yield break;
        }


        public override void Run()
        {

            Debug.Log("Старт геймплейной сцены");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerfomer coroutinesPerfomer = _container.Resolve<ICoroutinesPerfomer>();
                coroutinesPerfomer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}
