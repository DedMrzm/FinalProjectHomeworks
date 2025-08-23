using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.Gameplay.CymbolGenerator;
using Assets._Project.Develop.Runtime.UI.Gameplay.InputHandler;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        private readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public InputTextHandlerPresenter CreateInputTextHandlerPresenter(
            InputTextHandlerView view)
        {
            return new InputTextHandlerPresenter(
                view, 
                _container.Resolve<InputTextService>());
        }

        public CorrectAnswerPresenter CreateCorrectAnswerPresenter(
            SymbolsGenerator symbolsGenerator,
            CorrectAnswerView view)
        {
            return new CorrectAnswerPresenter(symbolsGenerator, view);
        }
    }
}
