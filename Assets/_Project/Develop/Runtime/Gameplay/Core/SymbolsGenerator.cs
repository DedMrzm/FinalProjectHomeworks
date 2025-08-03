using Assets._Project.Develop.Runtime.Infrastructure.DI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using Assets._Project.Develop.Runtime.Utilitis.ConfigsManagment;
using System.Linq;
using Assets._Project.Develop.Runtime.Configs.Gameplay;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Core
{
    public class SymbolsGenerator
    {
        public event Action<string> CorrectAnswerChanged;

        private ConfigsProviderService _configsProviderService;

        private Dictionary<GameModes, string> _generatorSettings = new Dictionary<GameModes, string>()
        {
            {GameModes.Digits, "0123456789"},
            {GameModes.Chars, "abcdefghjklmnopqrstwxyz" }
        };

        private GameModes _pickedGameMode;

        private string _correctAnswer;

        public SymbolsGenerator(ConfigsProviderService configsProviderService)
        {
            _configsProviderService = configsProviderService;
            _pickedGameMode = _configsProviderService.GetConfig<GameModeConfig>().GameMode;
        }

        public string Generate()
        {
            _correctAnswer = "";
            for(int i = 0; i < _configsProviderService.GetConfig<GameModeConfig>().CountOfGeneratedSymbols; i++)
            {
                _correctAnswer += string.Concat(_generatorSettings[_pickedGameMode][Random.Range(0, _generatorSettings[_pickedGameMode].Length-1)]);
            }

            CorrectAnswerChanged?.Invoke(_correctAnswer);
            return _correctAnswer;
        }
    }
}
