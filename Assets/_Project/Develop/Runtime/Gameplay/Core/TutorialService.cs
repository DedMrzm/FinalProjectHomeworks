using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilitis.CoroutinesManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Core
{
    public class TutorialService
    {
        public event Action<string> TextInputed;
        public event Action TextCleared;
        //public event Action BeginnerTutorialStarted;
        //public event Action DefeatTutorialStarted;
        //public event Action WinTutorialStarted;

        private const KeyCode NextCode = KeyCode.Space;
        private const KeyCode RestartCode = KeyCode.Space;
        private const KeyCode GoToMenuCode = KeyCode.Space;

        private WalletService _walletService;
        private ICoroutinesPerformer _coroutinesPerformer;

        private float _delay = 0.02f;

        private readonly string DefeatTutorial;
        private readonly string BeginnerTutorial = "Введи такие же символы, что и внизу";
        private readonly string WinTutorial;

        private bool _beginnerTutorialPassed = false;

        private Coroutine _currentProcess;

        public TutorialService(WalletService walletService, ICoroutinesPerformer coroutinesPerformer)
        {
            _walletService = walletService;
            _coroutinesPerformer = coroutinesPerformer;

            WinTutorial = $"Поздравляю, ты выиграл!" +
                $"\nТеперь у тебя {_walletService.GetCurrency(CurrencyTypes.Gold).Value} золота" +
                $"\nНажми на {KeyCode.Space}, чтобы сыграть снова!";
            DefeatTutorial = $"" +
                $"Ты проиграл :(" +
                $"\n Теперь у тебя {_walletService.GetCurrency(CurrencyTypes.Gold).Value} золота" +
                $"\nНажми на {KeyCode.Space}, чтобы попробовать снова!";
        }

        public IEnumerator InputTextWithDelay(float delay, string text, bool isInstantly = false)
        {
            if(isInstantly == false)
                foreach (char c in text)
                {
                    TextInputed?.Invoke(c.ToString());
                    yield return new WaitForSeconds(delay);
                }
            else
                TextInputed?.Invoke(text);
        }

        public void StartBeginnerTutorial()
        {
            ClearProcessFlow();
            _currentProcess = _coroutinesPerformer.StartPerform(InputTextWithDelay(_delay, BeginnerTutorial, _beginnerTutorialPassed));
            _beginnerTutorialPassed = true;
        }

        public IEnumerator StartDefeatTutorial()
        {
            ClearProcessFlow();
            _currentProcess = _coroutinesPerformer.StartPerform(InputTextWithDelay(_delay, DefeatTutorial));

            yield return new WaitWhile(() => Input.GetKeyDown(RestartCode) == false);
        }

        public IEnumerator StartWinTutorial()
        {
            ClearProcessFlow();
             _currentProcess = _coroutinesPerformer.StartPerform(InputTextWithDelay(_delay, WinTutorial));

            yield return new WaitWhile(() => Input.GetKeyDown(RestartCode) == false);
        }

        private void ClearProcessFlow()
        {
            if (_currentProcess != null)
                _coroutinesPerformer.StopPerform(_currentProcess);

            TextCleared?.Invoke();
        }
    }
}
