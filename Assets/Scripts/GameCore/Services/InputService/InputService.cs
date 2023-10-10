using System;
using GameCore.Services.SceneControllerService;
using UnityEngine;
using Zenject;

namespace GameCore.Services.InputService
{
    /// <summary>
    /// Create callback for cell click on scene
    /// </summary>
    public class InputService : IInputService, ITickable
    {
        private ISceneControllerService _sceneControllerService;
        public event Action<int> OnClickCell;

        public InputService(ISceneControllerService sceneControllerService)
        {
            _sceneControllerService = sceneControllerService;
        }
        
        public void Tick()
        {
            Vector3 mousePos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (_sceneControllerService.IsCellIndexOnPosition(mousePos, out int result))
                {
                    OnClickCell?.Invoke(result);
                }
            }
        }
    }
}