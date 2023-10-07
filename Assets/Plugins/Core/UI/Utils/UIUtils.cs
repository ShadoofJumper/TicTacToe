using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Core.UI.Utils
{
    public static class UIUtils
    {
        public static T CreateUIItem<T>(string name = "UIItem", Transform parent = null) where T : Component
        {
            var component = (new GameObject()).AddComponent<T>();
            component.name = name;
            component.transform.SetParent(parent);
            return component;
        }
        
        public static Canvas CreateCanvas(string name, string layer, int order, Vector2Int resolution, Camera camera)
        {
            var canvas = new GameObject().AddComponent<Canvas>(); /* diContainer.InstantiatePrefab(_hintCanvasPrefab).GetComponent<RectTransform>();*/;
            canvas.worldCamera = camera;
            canvas.gameObject.name = name;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.sortingOrder = order;
            canvas.sortingLayerName = layer;

            var canvasScaler = canvas.gameObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = resolution;
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = .5f;
                
            return canvas;
        }
    }
}