using System.Collections.Generic;
using Controllers.GameSetupManager;
using GameCore.Entity;
using GameCore.Entity.PlayerFactory;
using Zenject;

namespace GameCore.Services.SessionService
{
    public struct PlayersControllerPair
    {
        public PlayerControllerType PlayerOne;
        public PlayerControllerType PlayerTwo;

        public PlayersControllerPair(PlayerControllerType playerOne, PlayerControllerType playerTwo)
        {
            PlayerOne = playerOne;
            PlayerTwo = playerTwo;
        }
    }
    public class GameSessionService : IInitializable
    {
        private GameSetupManager _gameSetupManager;
        private IPlayerFactory _playerFactory;

        private SessionSettings _sessionSettings;
        private PlayerEntity _playerOne;
        private PlayerEntity _playerTwo;
        
        public PlayerEntity PlayerOne => _playerOne;
        public PlayerEntity PlayerTwo => _playerTwo;

        private Dictionary<SessionBattleType, PlayersControllerPair> _controllerPairs
            = new Dictionary<SessionBattleType, PlayersControllerPair>() {
                [SessionBattleType.PlayerVsPlayer] = 
                    new PlayersControllerPair(PlayerControllerType.User, PlayerControllerType.User),
                [SessionBattleType.PlayerVsBot] = 
                        new PlayersControllerPair(PlayerControllerType.User, PlayerControllerType.Bot),
                [SessionBattleType.BotVsBot] = 
                    new PlayersControllerPair(PlayerControllerType.User, PlayerControllerType.Bot)
            };

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
            var controllerPair = _controllerPairs[_sessionSettings.BattleType];
            _playerOne = _playerFactory.Create(PlayerSide.Player1, controllerPair.PlayerOne);
            _playerTwo = _playerFactory.Create(PlayerSide.Player2, controllerPair.PlayerTwo);
        }
    }
}