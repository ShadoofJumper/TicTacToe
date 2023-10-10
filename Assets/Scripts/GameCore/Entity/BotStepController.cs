using System;
using System.Linq;
using Controllers.SceneView;
using GameCore.Services.GameMechanicsService;
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
            int cellIndex = GetFreeCellIndex();
            OnCompleteStep?.Invoke(cellIndex);
        }

        private int GetFreeCellIndex()
        {
            var freeCells = _gameMechanicsService.GetFreeCells();
            return freeCells[Random.Range(0, freeCells.Count())];
        }
    }
}