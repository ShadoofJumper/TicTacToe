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

        //TODO: move this part to individual factory? or create array for store info about player params for mode
        private void CreatePlayers()
        {
            switch (_sessionSettings.BattleType)
            {
                case SessionBattleType.PlayerVsPlayer:
                    _playerOne = _playerFactory.Create(PlayerSide.Player1, PlayerControllerType.User);
                    _playerTwo = _playerFactory.Create(PlayerSide.Player2, PlayerControllerType.User);
                    break;
                case SessionBattleType.PlayerVsBot:
                    _playerOne = _playerFactory.Create(PlayerSide.Player1, PlayerControllerType.User);
                    _playerTwo = _playerFactory.Create(PlayerSide.Player2, PlayerControllerType.Bot);
                    break;
                case SessionBattleType.BotVsBot:
                    _playerOne = _playerFactory.Create(PlayerSide.Player1, PlayerControllerType.Bot);
                    _playerTwo = _playerFactory.Create(PlayerSide.Player2, PlayerControllerType.Bot);
                    break;
            }
        }
    }
}