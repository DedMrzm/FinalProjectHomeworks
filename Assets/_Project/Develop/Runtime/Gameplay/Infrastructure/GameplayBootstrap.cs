using Assets._Project.Develop.Runtime.Configs.Gameplay;
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
        private DIContainer _projectContainer;
        private GameplayInputArgs _inputArgs;

        private ConfigsProviderService _configsProviderService;
        private ICoroutinesPerformer _coroutinesPerformer;

        private GameplayCycle _gameplayCycle;
        private UpdateService _updateService;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _projectContainer = container;

            _configsProviderService = _projectContainer.Resolve<ConfigsProviderService>();
            _coroutinesPerformer = _projectContainer.Resolve<ICoroutinesPerformer>();
            _updateService = FindObjectOfType<UpdateService>();

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistrations.Process(_projectContainer, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Инициализация геймплейной сцены");

            yield return _configsProviderService.LoadAsync();

            GameModeConfig config = _configsProviderService.GetConfig<GameModeConfig>();

            config.SetGameMode(_inputArgs.PickedGameMode);

            Debug.Log($"Уровень: {config.GameMode}");

            yield break;
        }


        public override void Run()
        {
            _gameplayCycle = new GameplayCycle(_projectContainer);

            _coroutinesPerformer.StartPerform(_gameplayCycle.Launch());

            _updateService.Initialize(_gameplayCycle);

            Debug.Log("Старт геймплейной сцены");
        }
    }
}
