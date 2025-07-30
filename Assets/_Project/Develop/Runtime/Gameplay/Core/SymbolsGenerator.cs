using Assets._Project.Develop.Runtime.Infrastructure.DI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using System.Linq;

namespace Assets._Project.Develop.Runtime.Gameplay.Core
{
    public class SymbolsGenerator
    {
        private DIContainer _projectContainer;
        private ConfigsProviderService _configsProviderService;

        private Dictionary<GameModes, string> _gameModes = new Dictionary<GameModes, string>()
        {
            {GameModes.Digits, "0123456789"},
            {GameModes.Chars, "abcdefghjklmnopqrstwxyz" }
        };

        private GameModes _pickedGameMode;

        private string _correctAnswer;

        public SymbolsGenerator(DIContainer projectContainer)
        {
            _projectContainer = projectContainer;

            _configsProviderService = _projectContainer.Resolve<ConfigsProviderService>();
            _pickedGameMode = _configsProviderService.GetConfig<GameModeConfig>().GameMode;
        }

        public string Generate()
        {
            for(int i = 0; i < 3; i++)
            {
                _correctAnswer += string.Concat(_gameModes[_pickedGameMode][Random.Range(0, _gameModes[_pickedGameMode].Length-1)]);
            }

            Debug.Log("Правильный ответ: " + _correctAnswer);

            return _correctAnswer;
        }
    }
}
