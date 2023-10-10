using Controllers.SceneView;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.InputService;

namespace GameCore.Entity.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private IInputService _inputService;
        private IGameMechanicsService _gameMechanicsService;
        private ISceneView _sceneView;

        private const string UserDefaultName = "Player";
        private const string ComputerDefaultName = "Computer";
        
        public PlayerFactory(IGameMechanicsService gameMechanicsService, ISceneView sceneView)
        {
            _gameMechanicsService = gameMechanicsService;
            _sceneView = sceneView;
        }
        
        public PlayerEntity Create(PlayerSide playerSide, PlayerControllerType controllerType)
        {
            IEntityStepController entityStepController = BuildPlayerController(playerSide, controllerType);
            string playerName = GetPlayerName(playerSide, controllerType);
            return new PlayerEntity(playerName, playerSide, entityStepController);
        }

        private string GetPlayerName(PlayerSide playerSide, PlayerControllerType controllerType)
        {
            string playerName = controllerType == PlayerControllerType.User ? UserDefaultName : ComputerDefaultName;
            playerName += playerSide == PlayerSide.Player1 ? " 1" : " 2";
            return playerName;
        }

        private IEntityStepController BuildPlayerController(PlayerSide playerSide, PlayerControllerType controllerType)
        {
            switch (controllerType)
            {
                case PlayerControllerType.Bot:
                    return new BotStepController(playerSide, _gameMechanicsService, _sceneView);
                default:
                    return new UserStepController(_inputService, _gameMechanicsService);
            }
        }
    }
}