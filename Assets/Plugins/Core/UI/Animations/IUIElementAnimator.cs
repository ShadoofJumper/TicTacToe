using System;

namespace Plugins.Core.UI.Animations
{
    public interface IUIElementAnimator
    {
        void AnimateShow(UIElement element, Action action);
        void AnimateHide(UIElement element, Action action);
    }
}