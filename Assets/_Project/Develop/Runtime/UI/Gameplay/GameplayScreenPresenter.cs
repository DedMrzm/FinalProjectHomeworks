using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenPresenter : IPresenter
    {
        private readonly GameplayScreenView _screen;

        private readonly ProjectPresentersFactory _projectPresentersFactory;
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly PopupService _popupService;

        private readonly List<IPresenter> _childPresenters = new();

        public GameplayScreenPresenter(
            GameplayScreenView screen,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _screen = screen;
            _coroutinesPerformer = coroutinesPerformer;
        }
        public void Initialize()
        {
            _screen.GoToMenuButtonClicked += OnGoToMenuButtonClicked;
        }


        public void Dispose()
        {
            _screen.GoToMenuButtonClicked -= OnGoToMenuButtonClicked;
        }

        private void OnGoToMenuButtonClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
        }
    }
}
