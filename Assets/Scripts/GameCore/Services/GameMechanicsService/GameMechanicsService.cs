using System.Collections.Generic;
using GameCore.Entity;
using GameCore.Services.SessionService;
using UnityEngine;
using Utilities;
using Zenject;

namespace GameCore.Services.GameMechanicsService
{
    public enum GameResult
    {
        InProgress,
        WinPlayerOne,
        WinPlayerTwo,
        Draw
    }
    public class GameMechanicsService : IGameMechanicsService, IInitializable
    {
        private GameSessionService _gameSessionService;
        
        private PlayerEntity _player1;
        private PlayerEntity _player2;

        private const int FieldSize = 9;
        
        private const int _freeCellValue = 0;
        private const int _player1CellValue = 1;
        private const int _player2CellValue = 2;

        //TODO: maybe remove this and add method for get free cells
        public int FreeCellValue => _freeCellValue;

        private int[] _currentField;

        private PlayerEntity _currentStepPlayer;

        public int[] Field => _currentField;

        public GameMechanicsService(GameSessionService gameSessionService)
        {
            _gameSessionService = gameSessionService;
        }
        
        public void Initialize()
        {
            _currentField = new int[FieldSize];
            _player1 = _gameSessionService.PlayerOne;
            _player2 = _gameSessionService.PlayerTwo;
            StartGame();
        }

        private void StartGame()
        {
            Debug.Log("[Game mechanics] start game!");
            _currentStepPlayer = _player1;
            StartPlayerStep();
        }

        private void CompleteMove(int cellIndex)
        {
            Debug.Log($"[Game mechanics] CompleteMove {_currentStepPlayer.PlayerSide}, {cellIndex}");
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
            Debug.Log($"[Game mechanics] StartPlayerStep {_currentStepPlayer.PlayerSide}");
            _currentStepPlayer.OnCompleteStepAction += CompleteMove;
            _currentStepPlayer.StartStep();
        }

        private void PutValueOnField(int cellIndex, PlayerSide playerSide)
        {
            _currentField[cellIndex] =  GetPlayerCellValue(playerSide);
        }

        
        #region Game end complete methods

        private void CompleteGame(GameResult gameResult)
        {
            //here logic for cell ui manager and show complete popup
        }
        
        private bool IsGameComplete(out GameResult gameResult)
        {
            if (!IsBoardFull())
            {
                gameResult = GameResult.InProgress;
                return false;
            }

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
            else
            {
                gameResult = GameResult.Draw;
                return true;
            }
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
        public bool IsCellFree(int cellIndex)
        {
            return _currentField[cellIndex] == FreeCellValue;
        }
        public int GetBestMoveForCurrentPlayer()
        {
            int playerValue = _currentStepPlayer.PlayerSide == PlayerSide.Player1 ? _player1CellValue : _player2CellValue;
            int opponentValue = _currentStepPlayer.PlayerSide == PlayerSide.Player2 ? _player1CellValue : _player2CellValue;
            
            int bestMove = TicTacToeAI.GetBestMove(_currentField, playerValue, opponentValue, _freeCellValue);
            Debug.Log($"Get best move: [{_currentStepPlayer}], {playerValue}, {opponentValue}. index:{bestMove}");
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