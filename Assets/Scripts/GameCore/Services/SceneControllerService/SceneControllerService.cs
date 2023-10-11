using System;
using Controllers.SceneView;
using GameCore.Services.GameMechanicsService;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace GameCore.Services.SceneControllerService
{
    /// <summary>
    /// Access point to scene view
    /// get field collider hit info
    /// subscribe in game mechanics field change
    /// </summary>
    public class SceneControllerService : ISceneControllerService, IInitializable, IDisposable
    {
        private ISceneView _sceneView;
        private IGameMechanicsService _gameMechanicsService;
        
        public SceneControllerService(ISceneView sceneView, IGameMechanicsService gameMechanicsService)
        {
            _sceneView = sceneView;
            _gameMechanicsService = gameMechanicsService;
        }
        
        public void Initialize()
        {
            _gameMechanicsService.OnPlaceMark += PutMarkInCell;
            _gameMechanicsService.OnRemoveMark += RemoveMarkOnCell;
        }

        public bool IsCellIndexOnPosition(Vector3 pos, out int result)
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hits = Physics2D.Raycast(worldPoint, UnityEngine.Vector2.zero);
            if (hits.collider != null)
            {
                result = _sceneView.FieldCells.IndexOf(hits.collider);
                Debug.Log("SceneControllerService. Try get cell: "+result);
                return true;
            }

            result = 0;
            return false;
        }

        public void ShowHintCell(int cellIndex)
        {
            _sceneView.ShowHintCell(cellIndex);
        }
        
        private void PutMarkInCell(int cellIndex, PlayerSide playerSide)
        {
            Debug.Log("SceneControllerService. PutMarkInCell: "+cellIndex);
            _sceneView.PlaceMark(playerSide, cellIndex);
        }

        private void RemoveMarkOnCell(int cellIndex)
        {
            _sceneView.RemoveMark(cellIndex);
        }

        public void Dispose()
        {
            _gameMechanicsService.OnPlaceMark -= PutMarkInCell;
            _gameMechanicsService.OnRemoveMark -= RemoveMarkOnCell;
        }
    }
}