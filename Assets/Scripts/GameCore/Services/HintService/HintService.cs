using System;
using Controllers.SceneView;
using UI.HUD;
using Zenject;

namespace GameCore.Services.HitnService
{
    public class HintService : IHintService, IInitializable, IDisposable
    {
        private SceneView _sceneView;
        private HUDView _hudView;
        private IGameMechanicsService _gameMechanicsService;

        public HintService(SceneView sceneView, HUDView hudView, IGameMechanicsService gameMechanicsService)
        {
            _sceneView = sceneView;
            _hudView = hudView;
            _gameMechanicsService = gameMechanicsService;
        }


        private void ShowHint()
        {
            int cellIndex = _gameMechanicsService.GetBestMoveForCurrentPlayer();
            _sceneView.ShowHintCell(cellIndex);
        }

        public void Initialize()
        {
            _hudView.OnHintClickAction += ShowHint;
        }


        public void Dispose()
        {
            _hudView.OnHintClickAction -= ShowHint;
        }
    }
}

