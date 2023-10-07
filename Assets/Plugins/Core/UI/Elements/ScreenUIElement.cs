using Plugins.Core.UI.Animations;

namespace Plugins.Core.UI.Elements
{
    public abstract class ScreenUIElement<T> : UIElement<T>, IScreen, INoData
    {
        protected override IUIElementAnimator Animator => new InstantWindowAnimator();

        protected override void OnDataInitialized()
        {
        }
    }
}