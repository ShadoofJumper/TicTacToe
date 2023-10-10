using System;
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
        private IEntityStepController _stepController;

        private string _playerName;
        private PlayerSide _playerSide;
        private int _playerCellValue;
        
        private Stack<Step> _steps = new Stack<Step>();

        public string PlayerName => _playerName;
        public PlayerSide PlayerSide => _playerSide;
        public event Action<int> OnCompleteStepAction;
        
        public PlayerEntity(string playerName, PlayerSide playerSide, IEntityStepController stepController)
        {
            _playerName = playerName;
            _playerSide = playerSide;
            _stepController = stepController;
            _stepController.OnCompleteStep += OnCompleteStep;
        }

        public void StartStep()
        {
            _stepController.StarStep();
        }

        private void OnCompleteStep(int cellIndex)
        {
            OnCompleteStepAction?.Invoke(cellIndex);
            _steps.Push(new Step(){CellIndex = cellIndex});
        }
        
        public void UnDoStep()
        {
            _steps.Pop();
        }
        
    }
}