using System.Collections.Generic;
using System.Linq;
using Plugins.Core.Common.Services;

namespace Plugins.Core.UI
{
    public class UIManager : IUIManager
    {
        private const string UI_ID = "UI";
        
        private readonly List<UIElement> _elements;
        private readonly UIFactory _factory;
        private readonly TimeService _timeService;
        private uint _openedElementsCount = 0;

        public UIManager(List<UIElement> elements, UIFactory factory, TimeService timeService)
        {
            _elements = elements;
            _factory = factory;
            _timeService = timeService;
        }

        public T Get<T>() where T : UIElement
        {
            var uiElement = _elements.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
            return uiElement == null ? null : _factory.Create(uiElement);
        }

        public void IncreaseOpenedUIElementsCounter()
        {
            _openedElementsCount++;
            OnUpdateOpenedElementsCount();
        }

        public void DecreaseOpenedUIElementsCounter()
        {
            _openedElementsCount--;
            OnUpdateOpenedElementsCount();
        }

        private void OnUpdateOpenedElementsCount()
        {
            if (_openedElementsCount > 0)
            {
                _timeService.AddTimeStopper(UI_ID);
            }
            else
            {
                _timeService.RemoveTimeStopper(UI_ID);
            }
        }
    }
}