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

        private WalletService _walletService;
        private ICoroutinesPerformer _coroutinesPerformer;

        private float _delay = 0.05f;

        private readonly string DefeatTutorial;
        private readonly string BeginnerTutorial = "Введи такие же символы, что и внизу";
        private readonly string WinTutorial; 

        public TutorialService(WalletService walletService, ICoroutinesPerformer coroutinesPerformer)
        {
            _walletService = walletService;
            _coroutinesPerformer = coroutinesPerformer;

            WinTutorial = $"Поздравляю, ты выиграл!" +
                $"\nТеперь у тебя {_walletService.GetCurrency(CurrencyTypes.Gold).Value} золота" +
                $"\nНажми на любую клавишу, чтобы сыграть снова!";
            DefeatTutorial = $"" +
                $"Ты проиграл :(" +
                $"\n Теперь у тебя {_walletService.GetCurrency(CurrencyTypes.Gold).Value} золота" +
                $"\nНажми на любую клавишу, чтобы попробовать снова!";
        }

        public IEnumerator InputTextWithDelay(float delay, string text)
        {
            foreach (char c in text)
            {
                TextInputed?.Invoke(c.ToString());
                yield return new WaitForSeconds(delay);
            }
        }

        public void StartBeginnerTutorial()
        {
            TextCleared?.Invoke();
            _coroutinesPerformer.StartPerform(InputTextWithDelay(_delay, BeginnerTutorial));
        }

        public void StartDefeatTutorial()
        {
            TextCleared?.Invoke();
            _coroutinesPerformer.StartPerform(InputTextWithDelay(_delay, DefeatTutorial));
        }

        public void StartWinTutorial()
        {
            TextCleared?.Invoke();
            _coroutinesPerformer.StartPerform(InputTextWithDelay(_delay, WinTutorial));
        }
    }
}
