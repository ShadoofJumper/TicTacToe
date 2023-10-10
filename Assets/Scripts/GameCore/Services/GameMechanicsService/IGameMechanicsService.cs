using System;

namespace GameCore.Services.GameMechanicsService
{
    public interface IGameMechanicsService
    {
        event Action<int> OnRemoveMark;
        event Action<int, PlayerSide> OnPlaceMark;
        event Action<GameResult> OnCompleteGame;
        void UndoLastStep();
        int[] GetFreeCells();
        int GetBestMoveForCurrentPlayer();
        bool IsCellFree(int cellIndex);
    }
}