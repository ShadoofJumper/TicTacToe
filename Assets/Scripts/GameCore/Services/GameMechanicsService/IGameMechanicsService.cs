namespace GameCore
{
    public interface IGameMechanicsService
    {
        bool SetMark(PlayerSide playerSide, int cell, int row);
    }
}