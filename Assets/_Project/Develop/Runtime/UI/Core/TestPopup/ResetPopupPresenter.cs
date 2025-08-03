using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;

namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    public class ResetPopupPresenter : PopupPresenterBase
    {
        private readonly ResetPopupView _view;
        private readonly StatisticsService _statisticsService;

        public ResetPopupPresenter(
            ResetPopupView view,
            ICoroutinesPerformer coroutinesPerformer,
            StatisticsService statisticsService) : base(coroutinesPerformer)
        {
            _view = view;
            _statisticsService = statisticsService;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.ResetedStatistics += _statisticsService.ResetStatistics;
        }

        public override void Dispose()
        {
            base.Dispose();

            _view.ResetedStatistics -= _statisticsService.ResetStatistics;
        }
    }
}