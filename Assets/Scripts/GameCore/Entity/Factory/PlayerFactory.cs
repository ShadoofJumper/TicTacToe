using Controllers.SceneView;

namespace GameCore.Entity.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private IGameMechanicsService _gameMechanicsService;
        private ISceneView _sceneView;

        public PlayerFactory(IGameMechanicsService gameMechanicsService, ISceneView sceneView)
        {
            _gameMechanicsService = gameMechanicsService;
            _sceneView = sceneView;
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
                    return new BotStepController(playerSide, _gameMechanicsService, _sceneView);
                default:
                    return new UserStepController();
            }
        }
    }
}