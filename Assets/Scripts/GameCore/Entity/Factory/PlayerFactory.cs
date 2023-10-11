using System;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.InputService;
using GameCore.Services.TimerService;

namespace GameCore.Entity.PlayerFactory
{
    public class PlayerFactory : IPlayerFactory
    {
        private IInputService _inputService;
        private IGameMechanicsService _gameMechanicsService;
        private ITimerService _timerService;

        private const string UserDefaultName = "Player";
        private const string ComputerDefaultName = "Computer";
        
        public PlayerFactory(IGameMechanicsService gameMechanicsService, IInputService inputService, ITimerService timerService)
        {
            _gameMechanicsService = gameMechanicsService;
            _inputService = inputService;
            _timerService = timerService;
        }
        
        public PlayerEntity Create(PlayerSide playerSide, PlayerControllerType controllerType)
        {
            IEntityStepController entityStepController = BuildPlayerController(controllerType);
            string playerName = GetPlayerName(playerSide, controllerType);
            return new PlayerEntity(playerName, playerSide, entityStepController);
        }

        private string GetPlayerName(PlayerSide playerSide, PlayerControllerType controllerType)
        {
            string playerName = controllerType == PlayerControllerType.User ? UserDefaultName : ComputerDefaultName;
            playerName += playerSide == PlayerSide.Player1 ? " 1" : " 2";
            return playerName;
        }

        private IEntityStepController BuildPlayerController(PlayerControllerType controllerType)
        {
            switch (controllerType)
            {
                case PlayerControllerType.Bot:
                    return new BotStepController(_gameMechanicsService, _timerService);
                default:
                    return new UserStepController(_inputService, _gameMechanicsService);
            }
        }
    }
}