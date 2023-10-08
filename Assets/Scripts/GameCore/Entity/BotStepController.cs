using System;
using System.Linq;
using Controllers.SceneController;
using Random = UnityEngine.Random;

namespace GameCore.Entity
{
    public class BotStepController : IEntityStepController
    {
        private IGameMechanicsService _gameMechanicsService;
        private ISceneController _sceneController;

        private PlayerSide _playerSide;
        public event Action<int> OnCompleteStep;

        public BotStepController(PlayerSide playerSide, IGameMechanicsService gameMechanicsService, ISceneController sceneController)
        {
            _playerSide = playerSide;
            _gameMechanicsService = gameMechanicsService;
            _sceneController = sceneController;
        }
        
        public void StarStep()
        {
            //here logic for check game field
            int cellIndex = GetFreeCellIndex();
            _sceneController.PlaceMark(_playerSide, cellIndex);
            OnCompleteStep?.Invoke(cellIndex);
        }

        private int GetFreeCellIndex()
        {
            var freeCells = _gameMechanicsService.Field
                .Where(x => x == _gameMechanicsService.FreeCell)
                .ToArray();
            return freeCells[Random.Range(0, freeCells.Count())];
        }
    }
}