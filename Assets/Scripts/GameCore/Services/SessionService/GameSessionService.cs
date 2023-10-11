using System;
using System.Collections.Generic;
using Controllers.GameSetupManager;
using GameCore.Entity;
using GameCore.Entity.PlayerFactory;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.SessionTimerService;
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
    public class GameSessionService : IInitializable, IDisposable
    {
        private GameSetupManager _gameSetupManager;
        private IPlayerFactory _playerFactory;
        private IGameMechanicsService _gameMechanicsService;
        private IUIManager _uiManager;
        private ISessionTimerService _sessionTimerService;
        private HUDView _hudView;

        private SessionSettings _sessionSettings;
        private PlayerEntity _playerOne;
        private PlayerEntity _playerTwo;

        private const string MenuName = "MainMenuScene";

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
            ISessionTimerService sessionTimerService,
            HUDView hudView)
        {
            _hudView = hudView;
            _uiManager = uiManager;
            _gameMechanicsService = gameMechanicsService;
            _gameSetupManager = gameSetupManager;
            _playerFactory = playerFactory;
            _sessionTimerService = sessionTimerService;
        }
        
        public void Initialize()
        {
            _sessionSettings = _gameSetupManager.SessionSettings;
            CreatePlayers();
            _gameMechanicsService.OnCompleteGame += ShowCompleteGame;
            _gameMechanicsService.PlayerStartTurn += OnStartPlayerTurn;
            _hudView.OnMenuClickAction += ExitGameSession;
            _gameMechanicsService.SetupGameMechanics(_playerOne, _playerTwo);
        }

        private void OnStartPlayerTurn(string playerName)
        {
            _hudView.SetPlayerTurnTitle(playerName);
        }

        private void ShowCompleteGame(GameResult gameResult)
        {
            string timeFromSessionStart =
                $"{_sessionTimerService.MinutesFromStart:00}:{_sessionTimerService.SecondsFromStart:00}";
            _uiManager.Get<EndGamePopup>().Show(new EndGamePopupArgs(GetEndGameTitle(gameResult), timeFromSessionStart)).SubscribeOnClose(
                _ => RestartLevel());
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(Application.loadedLevel);
        }

        private void ExitGameSession()
        {
            SceneManager.LoadScene(MenuName);
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

        public void Dispose()
        {
            _gameMechanicsService.OnCompleteGame -= ShowCompleteGame;
            _gameMechanicsService.PlayerStartTurn -= OnStartPlayerTurn;
            _hudView.OnMenuClickAction -= ExitGameSession;
            _playerOne.Dispose();
            _playerTwo.Dispose();
        }
    }
}