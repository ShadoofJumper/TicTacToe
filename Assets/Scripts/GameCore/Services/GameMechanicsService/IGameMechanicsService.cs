using System;
using GameCore.Entity;

namespace GameCore.Services.GameMechanicsService
{
    public interface IGameMechanicsService
    {
        event Action OnComputerStartTurn;
        event Action OnComputerEndTurn;
        event Action<string> PlayerStartTurn;
        event Action<int> OnRemoveMark;
        event Action<int, PlayerSide> OnPlaceMark;
        event Action<GameResult> OnCompleteGame;
        void SetupGameMechanics(PlayerEntity playerOne, PlayerEntity playerTwo);
        void UndoLastStep();
        int[] GetFreeCells();
        int GetBestMoveForCurrentPlayer();
        bool IsCellFree(int cellIndex);
    }
}