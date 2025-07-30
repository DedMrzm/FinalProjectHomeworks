using Assets._Project.Develop.Runtime.Gameplay.Configs;
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
        private DIContainer _projectContainer;
        private ConfigsProviderService _configsProviderService;

        private GameplayInputArgs _inputArgs;
        private CoroutinesPerformer _coroutinesPerformer;

        private GameplayCycle _gameplayCycle;


        public override void ProcessRegistrations(DIContainer projectContainer, IInputSceneArgs sceneArgs = null)
        {
            _projectContainer = projectContainer;

            _configsProviderService = _projectContainer.Resolve<ConfigsProviderService>();
            _coroutinesPerformer = (CoroutinesPerformer)_projectContainer.Resolve<ICoroutinesPerfomer>();

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            Debug.Log(_inputArgs);

            GameplayContextRegistrations.Process(_projectContainer);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Начинается подгрузка конфигов");

            yield return _configsProviderService.LoadAsync();
            
            Debug.Log("Подгрузка конфигов завершена");

            GameModeConfig config = _configsProviderService.GetConfig<GameModeConfig>();

            config.SetGameMode(_inputArgs.GameMode);

            Debug.Log($"Уровень: {config.GameMode}");

            Debug.Log("Инициализация геймплейной сцены");

            yield break;
        }


        public override void Run()
        {
            _gameplayCycle = new GameplayCycle(_projectContainer);

            _coroutinesPerformer.StartPerform(_gameplayCycle.Launch());

            Debug.Log("Старт геймплейной сцены");
        }

        private void Update()
        {
            _gameplayCycle?.Update(Time.deltaTime);
        }
    }
}
