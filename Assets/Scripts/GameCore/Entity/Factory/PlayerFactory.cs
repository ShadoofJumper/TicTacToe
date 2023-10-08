using Controllers.SceneController;

namespace GameCore.Entity.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private IGameMechanicsService _gameMechanicsService;
        private ISceneController _sceneController;

        public PlayerFactory(IGameMechanicsService gameMechanicsService, ISceneController sceneController)
        {
            _gameMechanicsService = gameMechanicsService;
            _sceneController = sceneController;
        }
        
        public PlayerEntity Create(PlayerSide playerSide, PlayerControllerType controllerType)
        {
            IEntityStepController entityStepController = BuildPlayerController(playerSide, controllerType);
            return new PlayerEntity(entityStepController);
        }

        private IEntityStepController BuildPlayerController(PlayerSide playerSide, PlayerControllerType controllerType)
        {
            //TODO: here create controller instan with DI inject
            switch (controllerType)
            {
                case PlayerControllerType.Bot:
                    return new BotStepController(playerSide, _gameMechanicsService, _sceneController);
                default:
                    return new UserStepController();
            }
        }
    }
}