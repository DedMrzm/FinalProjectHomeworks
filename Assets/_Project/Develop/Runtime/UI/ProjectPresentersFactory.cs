using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Core.TestPopup;
using Assets._Project.Develop.Runtime.UI.Gameplay.CymbolGenerator;
using Assets._Project.Develop.Runtime.UI.MainMenu.Statistics;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilitis.Reactive;
using Assets._Project.Develop.Runtime.Utilitis.SceneManagment;

namespace Assets._Project.Develop.Runtime.UI
{
    public class ProjectPresentersFactory
    {
        private readonly DIContainer _container;

        public ProjectPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public CurrencyPresenter CreateCurrencyPresenter(
            IconTextView view,
            IReadOnlyVariable<int> currency,
            CurrencyTypes currencyType)
        {
            return new CurrencyPresenter(
                currency,
                currencyType,
                _container.Resolve<ConfigsProviderService>().GetConfig<CurrencyIconsConfig>(),
                view);
        }

        public StatisticsPresenter CreateStatisticsPresenter(
            IconTextListView view)
        {
            return new StatisticsPresenter(
                this,
                _container.Resolve<ViewsFactory>(),
                view,
                _container.Resolve<StatisticsService>());
        }

        public OneStatisticPresenter CreateOneStatisticPresenter(
            IconTextView view,
            StatisticsTypes statisticsType,
            StatisticsService statisticsService)
        {
            return new OneStatisticPresenter(
                statisticsType,
                view,
                statisticsService);
        }
        public CorrectAnswerPresenter CreateCorrectAnswerPresenter(
            SymbolsGenerator symbolsGenerator,
            CorrectAnswerView view)
        {
            return new CorrectAnswerPresenter(symbolsGenerator, view);
        }

        public WalletPresenter CreateWalletPresenter(IconTextListView view)
        {
            return new WalletPresenter(_container.Resolve<WalletService>(),
                this,
                _container.Resolve<ViewsFactory>(),
                view);
        }

        public ResetPopupPresenter CreateResetPopupPresenter(ResetPopupView view)
        {
            return new ResetPopupPresenter(
                view,
                _container.Resolve<ICoroutinesPerformer>(),
                _container.Resolve<StatisticsService>(),
                _container.Resolve<ConfigsProviderService>().GetConfig<StartGameModeRulesConfig>(),
                _container.Resolve<WalletService>());
        }
    }
}
