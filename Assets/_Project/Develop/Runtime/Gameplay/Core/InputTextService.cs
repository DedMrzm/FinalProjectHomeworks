using Assets._Project.Develop.Runtime.Gameplay.Utils;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Core
{
    public class InputTextService : IUpdatable
    {
        public event Action<string> InputedTextChanged;
        public event Action TextCleared;

        private bool _isRunning = false;

        private string _inputedText = "";

        public string CurrentSymbol
        {
            get
            {
                if (_inputedText.Length > 0)
                {
                    return _inputedText[_inputedText.Length - 1].ToString();
                }

                return "";
            }
        }

        public string InputedText => _inputedText;

        public void Enable() => _isRunning = true;

        public void Disable()
        {
            _isRunning = false;
            ClearInputedText();
        }

        private string InputSymbol()
        {
            string temp = Input.inputString;

            if (string.IsNullOrEmpty(temp) == false && temp.Length == 1)
                _inputedText += temp;
            else
                return null;

            InputedTextChanged?.Invoke(temp);

            return _inputedText;
        }

        public void ClearInputedText()
        {
            _inputedText = string.Empty;
            TextCleared?.Invoke();
        }

        public void Update(float deltaTime)
        {
            if(_isRunning ==  false)
                return;

            InputSymbol();
        }
    }
}
