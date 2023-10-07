using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Plugins.Core.UI.Elements
{
    public class ExtImage : Image, IPointerDownHandler
    {
        public string Id { get; private set; }
        public Action Action { get; set; }
        public Action<string> IdAction { get; set; }

        public void Init(string id)
        {
            Id = id;
        }

        public void DeInit()
        {
            Action = null;
            IdAction = null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Action?.Invoke();
            IdAction?.Invoke(Id);
        }
    }
}
