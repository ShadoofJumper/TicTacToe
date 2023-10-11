using Plugins.Core.UI;
using UI.Screens;
using Zenject;

namespace Meta
{
    public class MainMenuEntryPoint : IInitializable
    {
        private IUIManager _uiManager;
        
        public MainMenuEntryPoint(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }
        public void Initialize()
        {
            _uiManager.Get<MainMenuScreen>().Show(null);
        }
    }
}

