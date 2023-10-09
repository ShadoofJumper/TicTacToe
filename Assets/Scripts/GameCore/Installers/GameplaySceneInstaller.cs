using Controllers.SceneController;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.InputService;
using GameCore.Services.SessionService;
using UnityEngine;
using Zenject;

namespace GameCore.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private SceneController _sceneController;
        public override void InstallBindings()
        {
            Container.Bind<ISceneController>().FromInstance(_sceneController);

            Container.BindInterfacesAndSelfTo<GameSessionService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameMechanicsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
        }
    }
}