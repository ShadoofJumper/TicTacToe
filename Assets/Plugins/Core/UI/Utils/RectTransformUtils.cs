using UnityEngine;

namespace Plugins.Core.UI.Utils
{
    public static class RectTransformUtils
    {
        public static void SetFullSpaceAnchors(this RectTransform rectTransform)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
    }
}