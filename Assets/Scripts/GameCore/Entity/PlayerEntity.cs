using System.Collections.Generic;

namespace GameCore.Entity
{
    //TODO: maybe remove struct if it will store just one variable
    public struct Step
    {
        public int CellIndex;
    }
    
    public class PlayerEntity
    {
        private PlayerSide _playerSide;
        private IEntityStepController _stepController;

        private Stack<Step> _steps = new Stack<Step>();

        public PlayerEntity(IEntityStepController stepController)
        {
            _stepController = stepController;
            _stepController.OnCompleteStep += OnCompleteStep;
        }

        public void StartStep()
        {
            _stepController.StarStep();
        }

        private void OnCompleteStep(int cellIndex)
        {
            _steps.Push(new Step(){CellIndex = cellIndex});
        }
        
        public void UnDoStep()
        {
            _steps.Pop();
        }
        
    }
}