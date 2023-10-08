using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.SceneController
{
    public class SceneController : MonoBehaviour, ISceneController
    {
        [SerializeField] private SpriteRenderer _backgroundSpriteRenderer;
        [SerializeField] private Transform[] _fieldCells = new Transform[]{};
        [SerializeField] private GameObject _markPrefab;
        [SerializeField] private Transform _marksParent;

        private const int FieldSize = 9; 
        
        private Sprite _player1Mark;
        private Sprite _player2Mark;

        private GameObject[] _fieldMarks = new GameObject[FieldSize];


        public void SetPlayerMark(PlayerSide playerSprite, Sprite markSprite)
        {
            switch (playerSprite)
            {
                case PlayerSide.Player1:
                    _player1Mark = markSprite;
                    break;
                default:
                    _player2Mark = markSprite;
                    break;
            }
        }
        
        public void SetPlayground(Sprite playerSprite)
        {
            _backgroundSpriteRenderer.sprite = playerSprite;
        }
        
        public void PlaceMark(PlayerSide playerSide, int cellIndex)
        {
            if(_fieldMarks[cellIndex] != null)
                Destroy(_fieldMarks[cellIndex]);
        }

        
        private void PaintMark(PlayerSide playerSide, int cellIndex)
        {
            GameObject mark = Instantiate(_markPrefab, GetCellPosition(cellIndex), Quaternion.identity, _marksParent);
            mark.GetComponent<SpriteRenderer>().sprite = GetPlayerSprite(playerSide);
        }

        private Sprite GetPlayerSprite(PlayerSide playerSide)
        {
            return playerSide == PlayerSide.Player1 ? _player1Mark : _player2Mark;
        }
        
        //private int GetCellIndex(int row, int cell) => cell + row * 3;
        
        private Vector3 GetCellPosition(int cellIndex)
        {
            if (_fieldCells[cellIndex] == null)
                Debug.LogError("[SceneController] Cant get field cell position");
            return _fieldCells[cellIndex].position;
        }
    }
}

