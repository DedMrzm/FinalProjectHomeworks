using Assets._Project.Develop.Runtime.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.InputHandler
{
    public class InputTextHandlerView : MonoBehaviour, IView
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text) => _text.text = text;

        public void AddSymbol(string text) => _text.text += text;

        public void ClearText() => _text.text = string.Empty;
    }
}
