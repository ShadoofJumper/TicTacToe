using Meta;
using Zenject;

namespace MainMenu.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuEntryPoint>().AsSingle().NonLazy();
        }
    }
}

