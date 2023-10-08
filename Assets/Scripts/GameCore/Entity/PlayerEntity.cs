using System.Collections.Generic;

namespace GameCore.Entity
{
    public struct Step
    {
        public int Row;
        public int Cell;

        public Step(int row, int cell)
        {
            Row = row;
            Cell = cell;
        }
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

        private void OnCompleteStep(int cell, int row)
        {
            _steps.Push(new Step(cell, row));
        }
        
        public void UnDoStep()
        {
            _steps.Pop();
        }
        
    }
}