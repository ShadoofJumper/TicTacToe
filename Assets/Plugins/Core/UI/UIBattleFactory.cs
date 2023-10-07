using Plugins.Core.UI.Utils;
using UnityEngine;
using Zenject;

namespace Plugins.Core.UI
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UIBattleFactory
    {
        private readonly UIBattleRoot _root;
        private readonly DiContainer _container;

        public UIBattleFactory(UIBattleRoot root, DiContainer container)
        {
            _root = root;
            _container = container;
        }

        public T Create<T>(T source,string containerName = "") where T : UIElement
        {
            var parent =GetParent(source, containerName);

            var element = _container.InstantiatePrefab(source).GetComponent<T>();
            element.transform.SetParent(parent, false);
            element.Initialize();
            return element;
        }

        private RectTransform GetParent<T>(T source, string containerName) where T : UIElement
        {
            var parent = _root.ResolveParent(source);
            if (!string.IsNullOrEmpty(containerName))
            {
                var container = parent.Find(containerName);
                if (container == null)
                {
                    var go = new GameObject().AddComponent<RectTransform>();
                    go.SetFullSpaceAnchors();
                    go.name = containerName;
                    go.transform.SetParent(parent, false);
                    go.gameObject.layer = parent.gameObject.layer;
                    container = go;
                }

                parent = container.GetComponent<RectTransform>();
            }

            return parent;
        }
    }
}