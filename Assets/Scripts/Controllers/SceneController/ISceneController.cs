using UnityEngine;

namespace Controllers.SceneController
{
    public interface ISceneController
    {
        void SetPlayerMark(PlayerSide playerSprite, Sprite markSprite);
        void SetPlayground(Sprite playerSprite);
        void PlaceMark(PlayerSide playerSide, int cellIndex);
    }
}
