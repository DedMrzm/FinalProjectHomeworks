using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.Gameplay.Utils;
using Assets._Project.Develop.Runtime.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.InputHandler
{
    public class InputTextHandlerPresenter : IPresenter
    {
        private InputTextHandlerView _view;
        private InputTextService _service;

        public InputTextHandlerPresenter(
            InputTextHandlerView view, 
            InputTextService service)
        {
            _view = view;
            _service = service;
        }

        public void Initialize()
        {
            _view.ClearText();
            _service.InputedTextChanged += _view.AddSymbol;
            _service.TextCleared += _view.ClearText;
        }

        public void Dispose()
        {
            _service.InputedTextChanged -= _view.AddSymbol;
            _service.TextCleared -= _view.ClearText;
        }
    }
}
