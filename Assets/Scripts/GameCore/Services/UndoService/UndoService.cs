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
            GameSessionService gameSessionService,
            IGameMechanicsService gameMechanicsService)
        {
            _hudView = hudView;
            _gameSessionService = gameSessionService;
            _gameMechanicsService = gameMechanicsService;
        }

        
        public void Initialize()
        {
            if (_gameSessionService.BattleType == SessionBattleType.PlayerVsBot)
            {
                ShowUndoButton();
                _gameMechanicsService.OnComputerStartTurn += OnComputerStartTurn;
                _gameMechanicsService.OnComputerEndTurn += OnComputerEndTurn;
                _hudView.OnUndoClickAction += UndoLastStep;
            }
        }
        
        
        private void OnComputerStartTurn()
        {
            _hudView.SetUndoButtonActive(false);
        }
        
        private void OnComputerEndTurn()
        {
            _hudView.SetUndoButtonActive(true);
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
            if (_gameSessionService.BattleType == SessionBattleType.PlayerVsBot)
            {
                _hudView.OnUndoClickAction -= UndoLastStep;
                _gameMechanicsService.OnComputerStartTurn -= OnComputerStartTurn;
                _gameMechanicsService.OnComputerEndTurn -= OnComputerEndTurn;
            }
        }
    }
}