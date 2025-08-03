using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;

namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    public class ResetPopupPresenter : PopupPresenterBase
    {
        private readonly ResetPopupView _view;
        private readonly StatisticsService _statisticsService;
        private readonly WalletService _walletService;

        private readonly StartGameModeRulesConfig _rulesConfig;

        public ResetPopupPresenter(
            ResetPopupView view,
            ICoroutinesPerformer coroutinesPerformer,
            StatisticsService statisticsService,
            StartGameModeRulesConfig rulesConfig,
            WalletService walletService) : base(coroutinesPerformer)
        {
            _view = view;
            _statisticsService = statisticsService;
            _rulesConfig = rulesConfig;
            _walletService = walletService;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.ResetedStatistics += _statisticsService.ResetStatistics;

            if(_rulesConfig.CostOfReset >= _walletService.GetCurrency(CurrencyTypes.Gold).Value)
            {
                _view.SetButtonBlock();
            }
            else
            {
                _view.SetButtonUnBlock();
            }

            _view.SetTextOfCost(_rulesConfig.CostOfReset.ToString(), CurrencyTypes.Gold);

            _statisticsService.ResetedStatistics += _view.OnCloseButtonClick;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.ResetedStatistics -= _statisticsService.ResetStatistics;

            _statisticsService.ResetedStatistics -= _view.OnCloseButtonClick;
        }
    }
}