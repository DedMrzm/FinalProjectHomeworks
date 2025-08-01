﻿using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;

        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private Image _anticlicker;
        [SerializeField] private CanvasGroup _body;

        [SerializeField] private PopupAnimationTypes _animationType;

        private float _anticlickerDefaultAlpha;

        private Tween _currentAnimation;

        private void Awake()
        {
            _anticlickerDefaultAlpha = _anticlicker.color.a;
            _mainGroup.alpha = 0;
        }

        public void OnCloseButtonClick() => CloseRequest?.Invoke();

        public Tween Show()
        {
            KillCurrentAnimation();

            OnPreShow();

            _mainGroup.alpha = 1;

            Sequence animation = PopupAnimationCreator
                .CreateShowAnimation(_body, _anticlicker, _animationType, _anticlickerDefaultAlpha);

            ModifyShowAnimation(animation);

            animation.OnComplete(OnPostShow);

            return _currentAnimation = animation.SetUpdate(true).Play();
        }


        public Tween Hide()
        {
            KillCurrentAnimation();

            OnPreHide();

            Sequence animation = PopupAnimationCreator
                .CreateHideAnimation(_body, _anticlicker, _animationType, _anticlickerDefaultAlpha);

            ModifyHideAnimation(animation);

            animation.OnComplete(OnPostHide);

            return _currentAnimation = animation.SetUpdate(true).Play();
        }

        protected virtual void ModifyShowAnimation(Sequence animation) { }
        protected virtual void ModifyHideAnimation(Sequence animation) { }
        protected virtual void OnPostShow() { }

        protected virtual void OnPreShow() { }

        protected virtual void OnPostHide() { }

        protected virtual void OnPreHide() { }

        private void OnDestroy()
        {
            KillCurrentAnimation();
        }

        private void KillCurrentAnimation()
        {
            if(_currentAnimation != null)
                _currentAnimation.Kill();
        }
    }
}
