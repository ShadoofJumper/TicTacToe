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
        private const string BattleSceneName = "GameScene";
        
        private SessionSettings _currentSessionSettings;
        
        public SessionSettings SessionSettings => _currentSessionSettings;
        
        public void Initialize()
        {
            _currentSessionSettings = new SessionSettings()
            {
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