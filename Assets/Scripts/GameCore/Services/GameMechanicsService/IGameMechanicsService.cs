using System;
using GameCore.Services.GameMechanicsService;

namespace GameCore
{
    public interface IGameMechanicsService
    {
        event Action<int, PlayerSide> OnPlaceMark;
        event Action<GameResult> OnCompleteGame;
        int[] GetFreeCells();
        int GetBestMoveForCurrentPlayer();
        bool IsCellFree(int cellIndex);
    }
}