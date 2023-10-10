using System.Collections.Generic;
using UnityEngine;

namespace Controllers.SceneView
{
    public interface ISceneView
    {
        IList<Collider2D> FieldCells { get; }
        void ShowHintCell(int cellIndex);
        void SetPlayerMark(PlayerSide playerSprite, Sprite markSprite);
        void SetPlayground(Sprite playerSprite);
        void PlaceMark(PlayerSide playerSide, int cellIndex);
    }
}
