using System;
using System.Collections.Generic;

namespace GameCore.Entity
{
    public class PlayerEntity : IDisposable
    {
        private IEntityStepController _stepController;

        private string _playerName;
        private PlayerSide _playerSide;
        private int _playerCellValue;
        private bool _isComputer = false;
        
        private Stack<int> _steps = new Stack<int>();

        public string PlayerName => _playerName;
        public PlayerSide PlayerSide => _playerSide;
        public bool IsComputer => _isComputer;
        
        public event Action<int> OnCompleteStepAction;
        
        public PlayerEntity(string playerName, PlayerSide playerSide, IEntityStepController stepController)
        {
            _playerName = playerName;
            _playerSide = playerSide;
            _stepController = stepController;
            _stepController.OnCompleteStep += OnCompleteStep;
            if (stepController is BotStepController)
                _isComputer = true;
        }

        public void StartStep()
        {
            _stepController.StarStep();
        }

        private void OnCompleteStep(int cellIndex)
        {
            OnCompleteStepAction?.Invoke(cellIndex);
            _steps.Push(cellIndex);
        }
        
        public int UndoStep()
        {
            return _steps.Pop();
        }

        public void Dispose()
        {
            _stepController.Dispose();
        }
    }
}