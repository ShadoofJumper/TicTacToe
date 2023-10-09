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
        
        public HintService(SceneView sceneView, HUDView hudView)
        {
            _sceneView = sceneView;
            _hudView = hudView;
        }


        private void ShowHint()
        {
            //!!!!!!!!!!!here code for acces ai logic for getn hint index
            int cellIndex = 0;
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

