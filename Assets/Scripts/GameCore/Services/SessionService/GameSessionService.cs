using Controllers.GameSetupManager;
using GameCore.Entity;
using GameCore.Entity.PlayerFactory;
using Zenject;

namespace GameCore.Services.SessionService
{
    public class GameSessionService : IInitializable
    {
        private GameSetupManager _gameSetupManager;
        private IPlayerFactory _playerFactory;

        private SessionSettings _sessionSettings;
        private PlayerEntity _playerOne;
        private PlayerEntity _playerTwo;
        
        public PlayerEntity PlayerOne => _playerOne;
        public PlayerEntity PlayerTwo => _playerTwo;

        public GameSessionService(GameSetupManager gameSetupManager, IPlayerFactory playerFactory)
        {
            _gameSetupManager = gameSetupManager;
            _playerFactory = playerFactory;
        }
        
        public void Initialize()
        {
            _sessionSettings = _gameSetupManager.SessionSettings;
            CreatePlayers();
        }

        private void CreatePlayers()
        {
            switch (_sessionSettings.BattleType)
            {
                case SessionBattleType.PlayerVsPlayer:
                    _playerOne = _playerFactory.Create(PlayerControllerType.User);
                    _playerTwo = _playerFactory.Create(PlayerControllerType.User);
                    break;
                case SessionBattleType.PlayerVsBot:
                    _playerOne = _playerFactory.Create(PlayerControllerType.User);
                    _playerTwo = _playerFactory.Create(PlayerControllerType.Bot);
                    break;
                case SessionBattleType.BotVsBot:
                    _playerOne = _playerFactory.Create(PlayerControllerType.Bot);
                    _playerTwo = _playerFactory.Create(PlayerControllerType.Bot);
                    break;
            }
        }
    }
}