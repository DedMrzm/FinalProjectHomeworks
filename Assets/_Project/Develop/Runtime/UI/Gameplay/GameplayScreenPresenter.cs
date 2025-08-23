using Assets._Project.Develop.Runtime.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Gameplay.Utils;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay.CymbolGenerator;
using Assets._Project.Develop.Runtime.UI.Gameplay.InputHandler;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;

        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly GameplayPresentersFactory _gameplayPresentersFactory;
        private readonly SymbolsGenerator _symbolsGenerator;
        private readonly TutorialService _tutorialService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly List<IPresenter> _childPresenters = new();

        public GameplayScreenPresenter(
            GameplayScreenView screen,
            ICoroutinesPerformer coroutinesPerformer,
            SceneSwitcherService sceneSwitcherService,
            GameplayPresentersFactory gameplayPresentersFactory,
            SymbolsGenerator symbolsGenerator,
            UpdateService updateService,
            TutorialService tutorialService)
        {
            _screen = screen;
            _coroutinesPerformer = coroutinesPerformer;
            _sceneSwitcherService = sceneSwitcherService;
            _gameplayPresentersFactory = gameplayPresentersFactory;
            _symbolsGenerator = symbolsGenerator;
            _tutorialService = tutorialService;
        }
        public void Initialize()
        {
            _screen.GoToMenuButtonClicked += OnGoToMenuButtonClicked;

            CreateCorrectAnswerPresenter();
            CreateInputTextHandlerPresenter();
            ConnectTutorialParts();

            foreach (IPresenter presenter in _childPresenters)
                presenter.Initialize();
        }

        public void Dispose()
        {
            _screen.GoToMenuButtonClicked -= OnGoToMenuButtonClicked;

            foreach (IPresenter presenter in _childPresenters)
                presenter.Dispose();

            _childPresenters.Clear();
        }

        private void ConnectTutorialParts()
        {
            _screen.TutorialView.Initialize(_tutorialService);
        }

        private void CreateCorrectAnswerPresenter()
        {
            CorrectAnswerPresenter correctAnswerPresenter = _gameplayPresentersFactory.CreateCorrectAnswerPresenter(_symbolsGenerator, _screen.CorrectAnswerView);

            _childPresenters.Add(correctAnswerPresenter);
        }

        private void CreateInputTextHandlerPresenter()
        {
            InputTextHandlerPresenter inputTextHandlerPresenter = _gameplayPresentersFactory.CreateInputTextHandlerPresenter(_screen.InputView);

            //_updateService.AddUpdatableService(inputTextHandlerPresenter);

            _childPresenters.Add(inputTextHandlerPresenter);
        }

        private void OnGoToMenuButtonClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
        }
    }
}
