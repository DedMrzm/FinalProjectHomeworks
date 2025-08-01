﻿using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core.TestPopup
{
    public class TestPopupView : PopupViewBase
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text) => _text.text = text;

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