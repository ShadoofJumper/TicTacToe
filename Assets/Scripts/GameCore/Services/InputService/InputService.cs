using System;
using Controllers.SceneView;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Zenject;

namespace GameCore.Services.InputService
{
    public class InputService : IInputService, ITickable
    {
        private ISceneView _sceneView;
        public event Action<int> OnClickCell;

        public InputService(ISceneView sceneView)
        {
            _sceneView = sceneView;
        }
        
        public void Tick()
        {
            Vector3 mousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (_sceneView.IsCellIndexOnPosition(mousePos, out int result))
                {
                    OnClickCell?.Invoke(result);
                }
            }
        }
    }
}