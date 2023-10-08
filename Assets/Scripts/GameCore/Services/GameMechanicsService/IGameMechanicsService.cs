namespace GameCore
{
    public interface IGameMechanicsService
    {
        int FreeCell { get; }
        int[] Field { get; }
        bool SetMark(PlayerSide playerSide, int cell, int row);
    }
}