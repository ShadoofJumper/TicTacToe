using GameCore.Entity;
using GameCore.Services.SessionService;
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
        
        private const int FreeCell = 0;
        private const int Player1Cell = 1;
        private const int Player2Cell = 2;

        
        private int[] _currentField;

        private PlayerSide _currentPlayerStep;

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
            if (_currentField[cellIndex] != FreeCell)
                return false;

            _currentField[cellIndex] = playerSide == PlayerSide.Player1 ? Player1Cell : Player2Cell;
            CheckCompleteGame();
            return true;
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