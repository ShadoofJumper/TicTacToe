using System;
using DG.Tweening;
using UnityEngine;

namespace Plugins.Core.UI.Animations
{
    public class ScaleWindowAnimator : IUIElementAnimator
    {
        private const float Duration = .3f;
        private readonly float min = .3f;
        private readonly Ease _ease = Ease.OutBack;

        public void AnimateShow(UIElement element, Action action)
        {
            DOTween
                .To(x => Scale(element, x), min, 1f, Duration)
                .SetEase(_ease)
                .OnComplete(() => action()).SetUpdate(true);
        }

        public void AnimateHide(UIElement element, Action action) =>
            DOTween.To(x => Scale(element, x), 1f, min, Duration)
                .SetEase(_ease)
                .OnComplete(() => action()).SetUpdate(true);
        
        void Scale(UIElement element, float value) => element.transform.localScale=Vector3.one*value;
    }
}