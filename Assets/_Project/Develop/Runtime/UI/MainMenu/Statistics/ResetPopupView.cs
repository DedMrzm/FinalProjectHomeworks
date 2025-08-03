using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    public class ResetPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _costOfResetText;

        [SerializeField] private Button _acceptToResetButton;

        [SerializeField] private Color _blockColor;
        [SerializeField] private Color _unBlockColor;

        public event Action ResetedStatistics;

        public void SetTextOfCost(string costOfResetText, CurrencyTypes currencyType)
        {
           _costOfResetText.text += $"{costOfResetText} {currencyType}";
        }

        public void OnAcceptToResetStatistics() => ResetedStatistics?.Invoke();

        public void SetButtonBlock()
        {
            _acceptToResetButton.interactable = false;
            _acceptToResetButton.GetComponent<Image>().color = _blockColor;
        }

        public void SetButtonUnBlock()
        {
            _acceptToResetButton.interactable = true;
            _acceptToResetButton.GetComponent<Image>().color = _unBlockColor;
        }
    }
}