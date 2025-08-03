using DG.Tweening;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    public class ResetPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _text;

        public event Action ResetedStatistics;

        public void SetText(string text) => _text.text = text;

        public void OnAcceptToResetStatistics() => ResetedStatistics?.Invoke();

        protected override void ModifyShowAnimation(DG.Tweening.Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            animation
                .Insert(0, _text
                .DOFade(1, 0.2f)
                .From(0));
        }
    }
}