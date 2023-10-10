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
            //here cell to scene view for paint mark
            //save in field data
            _currentField[cellIndex] = GetPlayerCellValue(_currentStepPlayer.PlayerSide);
            _currentStepPlayer.OnCompleteStepAction -= CompleteMove;
            _currentStepPlayer = GetNextStepPlayer();
            StartPlayerStep();
        }

        private void StartPlayerStep()
        {
            Debug.Log($"[Game mechanics] StartPlayerStep {_currentStepPlayer.PlayerSide}");
            _currentStepPlayer.OnCompleteStepAction += CompleteMove;
            _currentStepPlayer.StartStep();
        }
        
        public bool SetMark(PlayerSide playerSide, int cell, int row)
        {
            int cellIndex = GetCellIndex(row, cell);
            if (_currentField[cellIndex] != FreeCellValue)
                return false;

            _currentField[cellIndex] = GetPlayerCellValue(playerSide);
            CheckCompleteGame();
            return true;
        }

        public int GetBestMoveForCurrentPlayer()
        {
            int playerValue = _currentStepPlayer.PlayerSide == PlayerSide.Player1 ? _player1CellValue : _player2CellValue;
            int opponentValue = _currentStepPlayer.PlayerSide == PlayerSide.Player2 ? _player1CellValue : _player2CellValue;
            
            int bestMove = TicTacToeAI.GetBestMove(_currentField, playerValue, opponentValue, _freeCellValue);
            Debug.Log($"Get best move: [{_currentStepPlayer}], {playerValue}, {opponentValue}. index:{bestMove}");
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

        private PlayerEntity GetNextStepPlayer() 
            => _currentStepPlayer.PlayerSide == PlayerSide.Player1 ? _player2 : _player1;

        private int GetPlayerCellValue(PlayerSide playerSide) =>
            playerSide == PlayerSide.Player1 ? _player1CellValue : _player2CellValue;
        private int GetCellIndex(int row, int cell) => cell + row * 3;
    }        

}