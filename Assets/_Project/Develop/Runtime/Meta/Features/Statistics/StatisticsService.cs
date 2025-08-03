using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilitis.DataManagment;
using System;
using UnityEngine;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Utilitis.Reactive;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class StatisticsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        public event Action StatisticsChanged;
        public event Action ResetedStatistics;

        private WalletService _walletService;
        private PlayerDataProvider _playerDataProvder;

        private StartGameModeRulesConfig _rulesConfig;
        private ConfigsProviderService _configsProviderService;

        private ICoroutinesPerformer _coroutinesPerformer;

        private int _countOfWins = 0;
        private int _countOfDefeats = 0;

        private Dictionary<StatisticsTypes, object> _statistics = new();


        public StatisticsService(
            WalletService walletService,
            PlayerDataProvider playerDataProvider,
            ConfigsProviderService configsProviderService,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _playerDataProvder = playerDataProvider;
            _walletService = walletService;
            _configsProviderService = configsProviderService;
            _coroutinesPerformer = coroutinesPerformer;

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);

            Initialize();
        }

        public IReadOnlyDictionary<StatisticsTypes, object> Statistics => _statistics;

        private void Initialize()
        {
            foreach (StatisticsTypes statisticsType in Enum.GetValues(typeof(CurrencyTypes)))
                _statistics[statisticsType] = 0;
        }

        public void ResetStatistics()
        {
            _rulesConfig = _configsProviderService.GetConfig<StartGameModeRulesConfig>();
            if (_walletService.Enough(CurrencyTypes.Gold, _rulesConfig.CostOfReset))
            {
                _walletService.Spend(CurrencyTypes.Gold, _rulesConfig.CostOfReset);
                _countOfWins = 0;
                _countOfDefeats = 0;

                UpdateStatistics();

                _coroutinesPerformer.StartPerform(_playerDataProvder.Save());

                ResetedStatistics?.Invoke();

                Debug.Log("Статистика сброшена!");
                Debug.Log($"Теперь у вас {_walletService.GetCurrency(CurrencyTypes.Gold).Value} монет, и {_countOfDefeats} - проигрышей, {_countOfWins} - побед!");
            }
            else
            {
                Debug.Log($"Не хватает монет для сброса статистки, нужно {_rulesConfig.CostOfReset} монет, а у Вас {_walletService.GetCurrency(CurrencyTypes.Gold).Value}");
            }
        }

        public void ShowStatistics()
        {
            Debug.Log($"У вас {_countOfWins} - побед\n{_countOfDefeats} - поражение\n{_walletService.GetCurrency(CurrencyTypes.Gold).Value} - золота");
        }

        public void ProcessWin()
        {
            _countOfWins++;

            UpdateStatistics();

            _coroutinesPerformer.StartPerform(_playerDataProvder.Save());
        }

        public void ProcessDefeat()
        {
            _countOfDefeats++;

            UpdateStatistics();

            _coroutinesPerformer.StartPerform(_playerDataProvder.Save());
        }

        public void ReadFrom(PlayerData data)
        {
            _countOfWins = data.CountOfWins;
            _countOfDefeats = data.CountOfDefeats;
            UpdateStatistics();
        }

        public void WriteTo(PlayerData data)
        {
            data.CountOfWins = _countOfWins;
            data.CountOfDefeats = _countOfDefeats;
            UpdateStatistics();
        }

        private void UpdateStatistics()
        {
            _statistics[StatisticsTypes.Wins] = _countOfWins;
            _statistics[StatisticsTypes.Defeats] = _countOfDefeats;

            StatisticsChanged?.Invoke();
        }
    }
}
