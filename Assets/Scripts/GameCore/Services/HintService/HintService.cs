using System;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.SceneControllerService;
using GameCore.Services.SessionService;
using UI.HUD;
using Zenject;

namespace GameCore.Services.HitnService
{
    public class HintService : IHintService, IInitializable, IDisposable
    {
        private HUDView _hudView;
        private IGameMechanicsService _gameMechanicsService;
        private ISceneControllerService _sceneControllerService;
        private GameSessionService _gameSessionService;

        public HintService(ISceneControllerService sceneControllerService, 
            HUDView hudView, 
            IGameMechanicsService gameMechanicsService,
            GameSessionService gameSessionService)
        {
            _sceneControllerService = sceneControllerService;
            _hudView = hudView;
            _gameMechanicsService = gameMechanicsService;
            _gameSessionService = gameSessionService;
        }

        public void Initialize()
        {
            if (_gameSessionService.BattleType == SessionBattleType.PlayerVsBot)
                ShowHintButton();
            _hudView.OnHintClickAction += ShowHint;
        }

        private void ShowHintButton()
        {
            _hudView.SetHintButtonActive(true);
        }
        
        private void ShowHint()
        {
            int cellIndex = _gameMechanicsService.GetBestMoveForCurrentPlayer();
            _sceneControllerService.ShowHintCell(cellIndex);
        }
        
        public void Dispose()
        {
            _hudView.OnHintClickAction -= ShowHint;
        }
    }
}

