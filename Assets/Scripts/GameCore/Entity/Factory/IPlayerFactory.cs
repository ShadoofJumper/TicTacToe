namespace GameCore.Entity.PlayerFactory
{
    public interface IPlayerFactory
    {
        PlayerEntity Create(PlayerSide playerSide, PlayerControllerType controllerType);
    }
}