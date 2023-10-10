using System;
using System.Collections.Generic;
using Controllers.GameSetupManager;
using GameCore.Entity;
using GameCore.Entity.PlayerFactory;
using GameCore.Services.GameMechanicsService;
using Plugins.Core.UI;
using UI.HUD;
using UI.Popups;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameCore.Services.SessionService
{
    /// <summary>
    /// Service get data for game session (type of mode atc.)
    /// Create session players
    /// manager game complete
    /// </summary>
    public class GameSessionService : IInitializable
    {
        private GameSetupManager _gameSetupManager;
        private IPlayerFactory _playerFactory;
        private IGameMechanicsService _gameMechanicsService;
        private IUIManager _uiManager;
        private HUDView _hudView;

        private SessionSettings _sessionSettings;
        private PlayerEntity _playerOne;
        private PlayerEntity _playerTwo;
        
        public PlayerEntity PlayerOne => _playerOne;
        public PlayerEntity PlayerTwo => _playerTwo;

        public SessionBattleType BattleType => _sessionSettings.BattleType;
        
        private readonly Dictionary<SessionBattleType, (PlayerControllerType playerOneType, PlayerControllerType playerTwoType)> gameModesContollers
            = new Dictionary<SessionBattleType, (PlayerControllerType playerOneType, PlayerControllerType playerTwoType)>
            {
                [SessionBattleType.PlayerVsPlayer] = (PlayerControllerType.User, PlayerControllerType.User),
                [SessionBattleType.PlayerVsBot] = (PlayerControllerType.User, PlayerControllerType.Bot),
                [SessionBattleType.BotVsBot] = (PlayerControllerType.Bot, PlayerControllerType.Bot),
            };

        public GameSessionService(GameSetupManager gameSetupManager, 
            IPlayerFactory playerFactory, 
            IGameMechanicsService gameMechanicsService,
            IUIManager uiManager,
            HUDView hudView)
        {
            _hudView = hudView;
            _uiManager = uiManager;
            _gameMechanicsService = gameMechanicsService;
            _gameSetupManager = gameSetupManager;
            _playerFactory = playerFactory;
        }
        
        public void Initialize()
        {
            _sessionSettings = _gameSetupManager.SessionSettings;
            _gameMechanicsService.OnCompleteGame += ShowCompleteGame;
            CreatePlayers();
        }

        private void ShowCompleteGame(GameResult gameResult)
        {
            _uiManager.Get<EndGamePopup>().Show(new EndGamePopupArgs(GetEndGameTitle(gameResult), 0)).SubscribeOnClose(
                _ => RestartLevel());
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(Application.loadedLevel);
        }

        private void CreatePlayers()
        {
            var gameModeControllers = gameModesContollers[_sessionSettings.BattleType];
            _playerOne = _playerFactory.Create(PlayerSide.Player1, gameModeControllers.playerOneType);
            _playerTwo = _playerFactory.Create(PlayerSide.Player2, gameModeControllers.playerTwoType);
            _hudView.SetPlayerOneName(_playerOne.PlayerName);
            _hudView.SetPlayerTwoName(_playerTwo.PlayerName);
        }

        private string GetEndGameTitle(GameResult gameResult)
        {
            if (gameResult == GameResult.WinPlayerOne)
            {
                return _playerOne.PlayerName + "win!";
            }
            else if (gameResult == GameResult.WinPlayerTwo)
            {
                return _playerTwo.PlayerName + "win!";
            }
            else if (gameResult == GameResult.Draw)
            {
                return "Draw!";
            }

            return "";
        }
    }
}