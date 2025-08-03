using Assets._Project.Develop.Runtime.Gameplay.Core;
using Assets._Project.Develop.Runtime.UI.Core;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.CymbolGenerator
{
    public class CorrectAnswerPresenter : IPresenter
    {
        private SymbolsGenerator _symbolsGenerator;

        private CorrectAnswerView _view;

        public CorrectAnswerPresenter(SymbolsGenerator symbolsGenerator, CorrectAnswerView view)
        {
            _symbolsGenerator = symbolsGenerator;
            _view = view;
        }

        public void Dispose()
        {
            _symbolsGenerator.CorrectAnswerChanged -= _view.SetText;
        }

        public void Initialize()
        {
            Debug.Log("Work");
            _symbolsGenerator.CorrectAnswerChanged += _view.SetText;
        }
    }
}
