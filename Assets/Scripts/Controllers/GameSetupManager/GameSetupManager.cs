using Plugins.Core.UI;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Controllers.GameSetupManager
{
    public struct SessionSettings
    {
        public SessionBattleType BattleType;
    }
    
    public class GameSetupManager : IInitializable
    {
        private IUIManager _uiManager;
        
        private const string BattleSceneName = "GamePlayScene";
        
        private SessionSettings _currentSessionSettings;
        
        public SessionSettings SessionSettings => _currentSessionSettings;

        public GameSetupManager(IUIManager uiManager)
        {
            _uiManager = uiManager;
        }
        
        public void Initialize()
        {
            _currentSessionSettings = new SessionSettings() {
                BattleType = SessionBattleType.PlayerVsPlayer
            };
        }
        
        public void SetBattleType(SessionBattleType battleType)
        {
            _currentSessionSettings.BattleType = battleType;
        }

        public void StartBattle()
        {
            SceneManager.LoadScene(BattleSceneName);
        }
    }
}