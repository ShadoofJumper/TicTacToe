using Plugins.Core.UI.Elements;
using UnityEngine;

namespace Plugins.Core.UI
{
    public class UIBattleRoot : MonoBehaviour
    {
        [SerializeField] private RectTransform _staticRoot;
        [SerializeField] private RectTransform _dynamicRoot;
        
        public RectTransform DynamicRoot=>_dynamicRoot;

        public RectTransform ResolveParent(UIElement element)
        {
            if (element is IDynamicUI)
                return _dynamicRoot;
            else
                return _staticRoot;
        }
    }
}