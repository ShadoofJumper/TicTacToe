﻿using Controllers.SceneView;
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
    public class SceneControllerService : ISceneControllerService, IInitializable
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
        }

        public bool IsCellIndexOnPosition(Vector3 pos, out int result)
        {
            RaycastHit2D hits = Physics2D.Raycast(pos, UnityEngine.Vector2.zero);
            if (hits.collider != null)
            {
                result = _sceneView.FieldCells.IndexOf(hits.collider);
                Debug.Log("SceneControllerService. Try get cell: "+result);
                return true;
            }

            result = 0;
            return false;
        }
        

        private void PutMarkInCell(int cellIndex, PlayerSide playerSide)
        {
            Debug.Log("SceneControllerService. PutMarkInCell: "+cellIndex);
            _sceneView.PlaceMark(playerSide, cellIndex);
        }
    }
}