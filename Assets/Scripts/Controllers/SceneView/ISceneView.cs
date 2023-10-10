using System.Collections.Generic;
using UnityEngine;

namespace Controllers.SceneView
{
    public interface ISceneView
    {
        IList<Collider2D> FieldCells { get; }
        void ShowHintCell(int cellIndex);
        void SetPlayerMarkSpite(PlayerSide playerSprite, Sprite markSprite);
        void SetPlaygroundSprite(Sprite playerSprite);
        void PlaceMark(PlayerSide playerSide, int cellIndex);
        void RemoveMark(int cellIndex);
    }
}
