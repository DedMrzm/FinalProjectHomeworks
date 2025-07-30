using Assets._Project.Develop.Runtime.Infrastructure.DI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Core
{
    public class SymbolsGenerator
    {
        private DIContainer _projectContainer;

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
            _pickedGameMode = projectContainer.Resolve<GameModeConfig>().GameMode;
        }

        public string Generate()
        {
            for(int i = 0; i < 3; i++)
            {
                _correctAnswer.Substring(_gameModes[_pickedGameMode][Random.Range(0, _gameModes[_pickedGameMode].Length-1)]);
            }

            Debug.Log("Правильный ответ: " + _correctAnswer);

            return _correctAnswer;
        }
    }
}
