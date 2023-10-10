using Controllers.SceneView;
using GameCore.Entity.PlayerFactory;
using GameCore.Services.GameMechanicsService;
using GameCore.Services.HitnService;
using GameCore.Services.InputService;
using GameCore.Services.SceneControllerService;
using GameCore.Services.SessionService;
using GameCore.Services.UndoService;
using UI.HUD;
using UnityEngine;
using Zenject;

namespace GameCore.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _hudView;
        [SerializeField] private SceneView _sceneView;
        
        public override void InstallBindings()
        {
            var sceneView = GameObject.Instantiate(_sceneView);
            Container.Bind<SceneView>().FromInstance(sceneView.GetComponent<SceneView>()).AsSingle().WhenInjectedInto<SceneControllerService>();;
            
            var hudView = GameObject.Instantiate(_hudView);
            Container.Bind<HUDView>().FromInstance(hudView.GetComponent<HUDView>()).AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameSessionService>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameMechanicsService>().AsSingle().NonLazy();;
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<HintService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UndoService>().AsSingle();

            Container.BindInterfacesAndSelfTo<SceneControllerService>().AsSingle();

            Container.BindInterfacesTo<PlayerFactory>().AsSingle();
        }
    }
}