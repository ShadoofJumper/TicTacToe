
using UnityEngine;

namespace GameCore.Services.SceneControllerService
{
    public interface ISceneControllerService
    {
        bool IsCellIndexOnPosition(Vector3 pos, out int result);
    }
}