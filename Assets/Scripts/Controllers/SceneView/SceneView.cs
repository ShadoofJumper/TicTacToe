using ModestTree;
using UnityEngine;

namespace Controllers.SceneView
{
    public class SceneView : MonoBehaviour, ISceneView
    {
        [SerializeField] private SpriteRenderer _backgroundSpriteRenderer;
        [SerializeField] private BoxCollider2D[] _fieldCells = new BoxCollider2D[]{};
        [SerializeField] private Transform _marksParent;

        //todo: move prefabs to other access point?
        [SerializeField] private GameObject _markPrefab;
        [SerializeField] private GameObject _hintPrefab;

        private const int FieldSize = 9; 
        
        private Sprite _player1Mark;
        private Sprite _player2Mark;

        private GameObject[] _fieldMarks = new GameObject[FieldSize];


        public void ShowHintCell(int cellIndex)
        {
            Vector3 hintPos = GetCellPosition(cellIndex);
            //note: mark will self destroy
            Instantiate(_hintPrefab, hintPos, Quaternion.identity);
        }

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
            PaintMark(playerSide, cellIndex);
        }

        public bool IsCellIndexOnPosition(Vector3 pos, out int result)
        {
            RaycastHit2D hits = Physics2D.Raycast(pos, Vector2.zero);
            if (hits.collider != null)
            {
                result = _fieldCells.IndexOf(hits.collider);
                Debug.Log("SceneView. Try get cell: "+result);
                return true;
            }

            result = 0;
            return false;
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
            return _fieldCells[cellIndex].transform.position;
        }
    }
}

