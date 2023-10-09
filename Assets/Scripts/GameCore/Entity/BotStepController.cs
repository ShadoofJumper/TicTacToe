using System;
using System.Linq;
using Controllers.SceneView;
using Random = UnityEngine.Random;

namespace GameCore.Entity
{
    public class BotStepController : IEntityStepController
    {
        private IGameMechanicsService _gameMechanicsService;
        private ISceneView _sceneView;

        private PlayerSide _playerSide;
        public event Action<int> OnCompleteStep;

        public BotStepController(PlayerSide playerSide, IGameMechanicsService gameMechanicsService, ISceneView sceneView)
        {
            _playerSide = playerSide;
            _gameMechanicsService = gameMechanicsService;
            _sceneView = sceneView;
        }
        
        public void StarStep()
        {
            //here logic for check game field
            int cellIndex = GetFreeCellIndex();
            _sceneView.PlaceMark(_playerSide, cellIndex);
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