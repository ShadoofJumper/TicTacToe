using Plugins.Core.UI.Elements;
using UnityEngine;

namespace Plugins.Core.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform _popups;
        [SerializeField] private RectTransform _screens;
        [SerializeField] private RectTransform _staticRoot;
        [SerializeField] private RectTransform _dynamicRoot;
        
        public RectTransform DynamicRoot=>_dynamicRoot;

        public RectTransform ResolveParent(UIElement element)
        {
            if (element is IScreen)
                return _screens;
            else if (element is IPopUp)
                return _popups;
            else if (element is IDynamicUI)
                return _dynamicRoot;
            else
                return _staticRoot;
        }
    }
}