using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.Utilitis.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Project.Develop.Runtime.UI.MainMenu.Statistics
{
    public class OneStatisticPresenter
    {
        //Бизнес логика
        private readonly StatisticsTypes _statisticType;
        private readonly StatisticsService _statisticsService;

        //Визуал
        private readonly IconTextView _view;

        private IDisposable _disposable;

        public OneStatisticPresenter(
            StatisticsTypes statisticType,
            IconTextView view,
            StatisticsService statisticsService)
        {
            _statisticType = statisticType;
            _view = view;
            _statisticsService = statisticsService;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_statisticsService.Statistics[_statisticType]);

            _statisticsService.StatisticsChanged += OnStatisticChanged;
        }

        public void Dispose()
        {
            _statisticsService.StatisticsChanged -= OnStatisticChanged;
        }

        private void OnStatisticChanged() => UpdateValue(_statisticsService.Statistics[_statisticType]);

        private void UpdateValue(object value) => _view.SetText($"{_statisticType}: {value}");
    }
}
