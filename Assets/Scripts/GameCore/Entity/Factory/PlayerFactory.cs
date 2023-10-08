namespace GameCore.Entity.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        public PlayerEntity Create(PlayerControllerType controllerType)
        {
            IEntityStepController entityStepController = BuildPlayerController(controllerType);
            return new PlayerEntity(entityStepController);
        }

        private IEntityStepController BuildPlayerController(PlayerControllerType controllerType)
        {
            switch (controllerType)
            {
                case PlayerControllerType.Bot:
                    return new BotStepController();
                default:
                    return new UserStepController();
            }
        }
    }
}