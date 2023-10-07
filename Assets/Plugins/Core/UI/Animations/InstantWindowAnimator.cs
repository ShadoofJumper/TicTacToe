using System;

namespace Plugins.Core.UI.Animations
{
    public class InstantWindowAnimator : IUIElementAnimator
    {
        public void AnimateShow(UIElement element, Action action) => action?.Invoke();
        public void AnimateHide(UIElement element, Action action) => action?.Invoke();
    }
}