using System;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.SessionService;
using UI.HUD;
using Zenject;

namespace GameCore.Services.UndoService
{
    public class UndoService : IUndoService, IInitializable, IDisposable
    {
        private HUDView _hudView;
        private GameSessionService _gameSessionService;
        private IGameMechanicsService _gameMechanicsService;

        public UndoService(HUDView hudView, 
            GameSessionService gameSessionService)
        {
            _hudView = hudView;
            _gameSessionService = gameSessionService;
        }

        
        public void Initialize()
        {
            if (_gameSessionService.BattleType == SessionBattleType.PlayerVsBot)
                ShowUndoButton();
            _hudView.OnUndoClickAction += UndoLastStep;
        }
        
        private void ShowUndoButton()
        {
            _hudView.SetUndoButtonActive(true);
        }
        
        private void UndoLastStep()
        {
            _gameMechanicsService.UndoLastStep();
        }

        public void Dispose()
        {
            _hudView.OnUndoClickAction -= UndoLastStep;
        }
    }
}