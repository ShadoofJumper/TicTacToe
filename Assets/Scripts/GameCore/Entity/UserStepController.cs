using System;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.InputService;

namespace GameCore.Entity
{
    public class UserStepController : IEntityStepController
    {
        private IInputService _inputService;
        private IGameMechanicsService _gameMechanicsService;

        public event Action<int> OnCompleteStep;

        public UserStepController(IInputService inputService, IGameMechanicsService gameMechanicsService)
        {
            _inputService = inputService;
            _gameMechanicsService = gameMechanicsService;
        }
        
        public void StarStep()
        {
            _inputService.OnClickCell += TryPlaceMark;
        }

        private void TryPlaceMark(int cellIndex)
        {
            if (_gameMechanicsService.IsCellFree(cellIndex))
                CompleteStep(cellIndex);
        }

        private void CompleteStep(int chosenCellIndex)
        {
            OnCompleteStep?.Invoke(chosenCellIndex);
            _inputService.OnClickCell -= CompleteStep;
        }

        public void Dispose()
        {
        }
    }
}