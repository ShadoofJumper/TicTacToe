namespace GameCore
{
    public interface IGameMechanicsService
    {
        int FreeCellValue { get; }
        int[] Field { get; }
        int GetBestMoveForCurrentPlayer();
        bool IsCellFree(int cellIndex);
    }
}