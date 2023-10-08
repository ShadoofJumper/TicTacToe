namespace GameCore.Entity.PlayerFactory
{
    public interface IPlayerFactory
    {
        PlayerEntity Create(PlayerControllerType controllerType);
    }
}