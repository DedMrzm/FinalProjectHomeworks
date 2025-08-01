using Assets._Project.Develop.Runtime.UI.CommonView;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenLevelsMenuButtonClick;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }

        [SerializeField] private Button _openLevelsMenuButton;

        private void OnEnable()
        {
            _openLevelsMenuButton.onClick.AddListener(OnOpenLevelsMenuButtonClick);
        }

        private void OnDisable()
        {
            _openLevelsMenuButton.onClick.RemoveListener(OnOpenLevelsMenuButtonClick);
        }

        private void OnOpenLevelsMenuButtonClick()
        {
            OpenLevelsMenuButtonClick?.Invoke();
        }
    }
}
