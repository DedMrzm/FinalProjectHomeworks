using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    public class ResetPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _costOfResetText;

        public event Action ResetedStatistics;

        public void SetTextOfCost(string costOfResetText, CurrencyTypes currencyType)
        {
           _costOfResetText.text += $"{costOfResetText} {currencyType}";
        }

        public void OnAcceptToResetStatistics() => ResetedStatistics?.Invoke();
    }
}