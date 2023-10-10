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
        
        private const int FreeCellValue = 0;
        private const int Player1CellValue = 1;
        private const int Player2CellValue = 2;

        private int[] _currentField;

        private PlayerSide _currentPlayerStep;

        public int[] Field => _currentField;
        public int FreeCell => FreeCellValue;

        public GameMechanicsService(GameSessionService gameSessionService)
        {
            _gameSessionService = gameSessionService;
        }
        
        public void Initialize()
        {
            _currentField = new int[FieldSize];
            _player1 = _gameSessionService.PlayerOne;
            _player2 = _gameSessionService.PlayerTwo;
        }
        
        public bool SetMark(PlayerSide playerSide, int cell, int row)
        {
            int cellIndex = GetCellIndex(row, cell);
            if (_currentField[cellIndex] != FreeCellValue)
                return false;

            _currentField[cellIndex] = playerSide == PlayerSide.Player1 ? Player1CellValue : Player2CellValue;
            CheckCompleteGame();
            return true;
        }

        public int GetBestMoveForCurrentPlayer()
        {
            int playerValue = _currentPlayerStep == PlayerSide.Player1 ? Player1CellValue : Player2CellValue;
            int opponentValue = _currentPlayerStep == PlayerSide.Player2 ? Player1CellValue : Player2CellValue;
            
            int bestMove = TicTacToeAI.GetBestMove(_currentField, playerValue, opponentValue, FreeCell);
            Debug.Log($"Get best move: [{_currentPlayerStep}], {playerValue}, {opponentValue}. index:{bestMove}");
            return bestMove;
        }
        
        private void CheckCompleteGame()
        {
            //here logic for check field for comple game
        }

        private void CompleteGame()
        {
            //here logic for cell ui manager and show complete popup
        }
        
        private int GetCellIndex(int row, int cell) => cell + row * 3;
    }        

}