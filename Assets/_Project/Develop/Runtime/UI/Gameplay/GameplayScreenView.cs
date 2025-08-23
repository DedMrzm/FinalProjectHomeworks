using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay.CymbolGenerator;
using Assets._Project.Develop.Runtime.UI.Gameplay.InputHandler;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        public event Action GoToMenuButtonClicked;

        [SerializeField] private TMP_Text _inputText;

        [SerializeField] private TMP_Text _correctAnswerText;

        [SerializeField] private Button _goToMenuButton;

        public void SetInputText(string text)
        {
            _inputText.text = text;
        }

        public void SetCorrectAnswerText(string text)
        {
            _correctAnswerText.text = text;
        }

        [field: SerializeField] public InputTextHandlerView InputView { get; private set; }

        [field: SerializeField] public CorrectAnswerView CorrectAnswerView { get; private set; }

        private void OnEnable()
        {
            _goToMenuButton.onClick.AddListener(OnGoToMenuButtonClicked);
        }

        private void OnDisable()
        {
            _goToMenuButton.onClick.RemoveListener(OnGoToMenuButtonClicked);
        }

        private void OnGoToMenuButtonClicked()
        {
            GoToMenuButtonClicked?.Invoke();
        }
    }
}