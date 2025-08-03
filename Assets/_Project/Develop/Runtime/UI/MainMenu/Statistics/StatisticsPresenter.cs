using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.UI.MainMenu.Statistics
{
    public class StatisticsPresenter : IPresenter
    {
        private readonly StatisticsService _statisticsService;

        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;

        private readonly IconTextListView _view;

        private List<OneStatisticPresenter> _statisticsPresenters = new();
        public StatisticsPresenter(
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewFactory,
            IconTextListView view,
            StatisticsService statisticsService)
        {
            _presentersFactory = presentersFactory;
            _viewsFactory = viewFactory;
            _view = view;
            _statisticsService = statisticsService;
        }

        public void Initialize()
        {
            foreach (StatisticsTypes statisticType in _statisticsService.Statistics.Keys)
            {
                IconTextView statisticView = _viewsFactory.Create<IconTextView>(ViewIDs.OneStatisticView);

                _view.Add(statisticView);

                OneStatisticPresenter oneStatisticPresenter = _presentersFactory.CreateOneStatisticPresenter(
                    statisticView,
                    statisticType,
                    _statisticsService);
                    

                oneStatisticPresenter.Initialize();
                _statisticsPresenters.Add(oneStatisticPresenter);
            }
        }

        public void Dispose()
        {
            foreach (OneStatisticPresenter oneStatisticPresenter in _statisticsPresenters)
            {
                _view.Remove(oneStatisticPresenter.View);

                _viewsFactory.Release(oneStatisticPresenter.View);

                oneStatisticPresenter.Dispose();
            }

            _statisticsPresenters.Clear();
        }
    }
}
