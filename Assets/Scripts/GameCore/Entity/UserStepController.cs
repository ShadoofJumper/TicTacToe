using System;
using GameCore.Services.InputService;

namespace GameCore.Entity
{
    public class UserStepController : IEntityStepController
    {
        private IInputService _inputService;
        public event Action<int> OnCompleteStep;

        public UserStepController(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        public void StarStep()
        {
            _inputService.OnClickCell += CompleteStep;
        }

        private void CompleteStep(int chosenCellIndex)
        {
            OnCompleteStep?.Invoke(chosenCellIndex);
            _inputService.OnClickCell -= CompleteStep;
        }
    }
}