using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.DataManagment.DataProviders;
using Assets._Project.Develop.Runtime.Utilitis.DataManagment;
using Assets._Project.Develop.Runtime.Utilitis.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;

namespace Assets._Project.Develop.Runtime.Meta.Features
{
    public class StatisticsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private WalletService _walletService;
        private PlayerDataProvider _playerDataProvder;

        private StartGameModeRulesConfig _rulesConfig;
        private ConfigsProviderService _configsProviderService;

        private int _countOfWins;
        private int _countOfDefeats;

        public StatisticsService(WalletService walletService, PlayerDataProvider playerDataProvider, ConfigsProviderService configsProviderService)
        {
            _playerDataProvder = playerDataProvider;
            _walletService = walletService;
            _configsProviderService = configsProviderService;

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public void ResetStatistics()
        {
            _rulesConfig = _configsProviderService.GetConfig<StartGameModeRulesConfig>();
            if (_walletService.Enough(CurrencyTypes.Gold, _rulesConfig.CostOfReset))
            {
                _walletService.Spend(CurrencyTypes.Gold, _rulesConfig.CostOfReset);
                _countOfWins = 0;
                _countOfDefeats = 0;

                _playerDataProvder.Save();

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
        }

        public void ProcessDefeat()
        {
            _countOfDefeats++;
        }

        public void ReadFrom(PlayerData data)
        {
            _countOfWins = data.CountOfWins;
            _countOfDefeats = data.CountOfDefeats;
        }

        public void WriteTo(PlayerData data)
        {
            data.CountOfWins = _countOfWins;
            data.CountOfDefeats = _countOfDefeats;
        }
    }
}
