using Controllers.GameSetupManager;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(ApplicationInstaller), menuName = "Game/ZenjectInstallers/" + nameof(ApplicationInstaller))]
    public class ApplicationInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSetupManager>().AsSingle().NonLazy();
        }
    }
}