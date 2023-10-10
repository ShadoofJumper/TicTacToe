namespace GameCore
{
    public interface IGameMechanicsService
    {
        int FreeCellValue { get; }
        int[] Field { get; }
        bool SetMark(PlayerSide playerSide, int cell, int row);
        int GetBestMoveForCurrentPlayer();
    }
}