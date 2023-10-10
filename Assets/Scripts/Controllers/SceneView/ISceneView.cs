using UnityEngine;

namespace Controllers.SceneView
{
    public interface ISceneView
    {
        void ShowHintCell(int cellIndex);
        void SetPlayerMark(PlayerSide playerSprite, Sprite markSprite);
        void SetPlayground(Sprite playerSprite);
        void PlaceMark(PlayerSide playerSide, int cellIndex);
        bool IsCellIndexOnPosition(Vector3 pos, out int result);
    }
}
