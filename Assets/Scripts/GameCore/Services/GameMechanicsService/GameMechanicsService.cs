using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Entity;
using UnityEngine;
using Utilities;

namespace GameCore.Services.GameMechanicsService
{
    public enum GameResult
    {
        InProgress,
        WinPlayerOne,
        WinPlayerTwo,
        Draw
    }
    /// <summary>
    /// Service manage man game mechanics
    /// give access to game field state
    /// check game end
    /// </summary>
    public class GameMechanicsService : IGameMechanicsService
    {
        private PlayerEntity _player1;
        private PlayerEntity _player2;

        private const int FieldSize = 9;
        
        private const int _freeCellValue = 0;
        private const int _player1CellValue = 1;
        private const int _player2CellValue = 2;
        
        private int[] _currentField;
        private PlayerEntity _currentStepPlayer;

        public event Action OnComputerStartTurn;
        public event Action OnComputerEndTurn;
        public event Action<string> PlayerStartTurn;
        public event Action<int> OnRemoveMark;
        public event Action<int, PlayerSide> OnPlaceMark;
        public event Action<GameResult> OnCompleteGame;
        
        public void SetupGameMechanics(PlayerEntity playerOne, PlayerEntity playerTwo)
        {
            _player1 = playerOne;
            _player2 = playerTwo;
            
            _currentField = new int[FieldSize];
            StartGame();
        }

        public void UndoLastStep()
        {
            if (IsFieldEmpty())
                return;
            
            List<int> cellsUndo = new List<int>();
            
            cellsUndo.Add(_player2.UndoStep());
            cellsUndo.Add(_player1.UndoStep());
            cellsUndo.ForEach(cellIndex =>
            {
                _currentField[cellIndex] = _freeCellValue;
                OnRemoveMark?.Invoke(cellIndex);
            });
        }
        
        private void StartGame()
        {
            _currentStepPlayer = _player1;
            StartPlayerStep();
        }

        private void CompleteMove(int cellIndex)
        {
            if(_currentStepPlayer.IsComputer)
                OnComputerEndTurn?.Invoke();
            PutValueOnField(cellIndex, _currentStepPlayer.PlayerSide);
            _currentStepPlayer.OnCompleteStepAction -= CompleteMove;
            if (IsGameComplete(out GameResult gameResult))
            {
                CompleteGame(gameResult);
                return;
            }
            
            _currentStepPlayer = GetNextStepPlayer();
            StartPlayerStep();
        }

        private void StartPlayerStep()
        {
            if(_currentStepPlayer.IsComputer)
                OnComputerStartTurn?.Invoke();
            PlayerStartTurn?.Invoke(_currentStepPlayer.PlayerName);
            _currentStepPlayer.OnCompleteStepAction += CompleteMove;
            _currentStepPlayer.StartStep();
        }

        private void PutValueOnField(int cellIndex, PlayerSide playerSide)
        {
            _currentField[cellIndex] =  GetPlayerCellValue(playerSide);
            OnPlaceMark?.Invoke(cellIndex, playerSide);
        }

        
        #region Game end complete methods

        private void CompleteGame(GameResult gameResult)
        {
            OnCompleteGame?.Invoke(gameResult);
        }
        private bool IsGameComplete(out GameResult gameResult)
        {
            if (CheckWin(PlayerSide.Player1))
            {
                gameResult = GameResult.WinPlayerOne;
                return true;
            }
            else if (CheckWin(PlayerSide.Player2))
            {
                gameResult = GameResult.WinPlayerTwo;
                return true;
            }
            else if(IsBoardFull())
            {
                gameResult = GameResult.Draw;
                return true;
            }
            
            gameResult = GameResult.InProgress;
            return false;
        }
        private bool CheckWin(PlayerSide playerSide)
        {
            int playerCellValue = GetPlayerCellValue(playerSide);
            //check horizontal and vertical lines
            for (int i = 0; i < 3; i++)
            {
                if ((_currentField[(i*3) + 0] == playerCellValue && _currentField[(i*3)+ 1] == playerCellValue && _currentField[(i*3) + 2] == playerCellValue) ||
                    (_currentField[0 + i] == playerCellValue && _currentField[3 + i] == playerCellValue && _currentField[6 + i] == playerCellValue))
                {
                    return true;
                }
            }

            //check diagonal
            if ((_currentField[0] == playerCellValue && _currentField[4] == playerCellValue && _currentField[8] == playerCellValue) ||
                (_currentField[2] == playerCellValue && _currentField[4] == playerCellValue && _currentField[6] == playerCellValue))
            {
                return true;
            }

            return false;
        }
        private bool IsBoardFull()
        {
            for (int i = 0; i < 9; i++)
            {
                if (_currentField[i] == _freeCellValue)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
        
        #region Helper methods

        public bool IsFieldEmpty()
        {
            for (int i = 0; i < _currentField.Length; i++)
            {
                if (_currentField[i] != _freeCellValue)
                    return false;
            }

            return true;
        }
        
        public int[] GetFreeCells()
        {
            List<int> freeCells = new List<int>();
            for (int i = 0; i < _currentField.Length; i++)
            {
                if(_currentField[i] == _freeCellValue)
                    freeCells.Add(i);
            }

            return freeCells.ToArray();
        }
        public bool IsCellFree(int cellIndex)
        {
            return _currentField[cellIndex] == _freeCellValue;
        }
        public int GetBestMoveForCurrentPlayer()
        {
            int playerValue = _currentStepPlayer.PlayerSide == PlayerSide.Player1 ? _player1CellValue : _player2CellValue;
            int opponentValue = _currentStepPlayer.PlayerSide == PlayerSide.Player2 ? _player1CellValue : _player2CellValue;
            
            int bestMove = TicTacToeAI.GetBestMove(_currentField, playerValue, opponentValue, _freeCellValue);
            return bestMove;
        }

        private PlayerEntity GetNextStepPlayer() 
            => _currentStepPlayer.PlayerSide == PlayerSide.Player1 ? _player2 : _player1;

        private int GetPlayerCellValue(PlayerSide playerSide) =>
            playerSide == PlayerSide.Player1 ? _player1CellValue : _player2CellValue;
        
        private int GetCellIndex(int row, int cell) => cell + row * 3;

        #endregion
        
    }        

}