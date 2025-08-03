using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action ResetStatisticsButtonClicked;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }

        [SerializeField] private Button _resetStatisticsButton;
        [SerializeField] private TMP_Text _resetStatisticsCostText;

        private void OnEnable()
        {
            _resetStatisticsButton.onClick.AddListener(OnResetStatisticsButtonClicked);
        }

        private void OnDisable()
        {
            _resetStatisticsButton.onClick.RemoveListener(OnResetStatisticsButtonClicked);
        }

        private void OnResetStatisticsButtonClicked()
        {
            ResetStatisticsButtonClicked?.Invoke();
        }
    }
}
