using Plugins.Core.Common.Services;
using Plugins.Core.UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(UIInstaller), menuName = "Game/ZenjectInstallers/" + nameof(UIInstaller))]
    public class UIInstaller: ScriptableObjectInstaller
    {
        [SerializeField] private GameObject _ui;
        [SerializeField] private UIElement[] elements;

        public override void InstallBindings()
        {            
            var ui = GameObject.Instantiate(_ui);
            if (Application.isPlaying)//todo: temp fix for zenject validation
                DontDestroyOnLoad(ui);
            Container.Bind<Canvas>().FromInstance(ui.GetComponentInChildren<Canvas>()).AsSingle();
            Container.Bind<UIRoot>().FromInstance(ui.GetComponent<UIRoot>()).AsSingle();

            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
            
            foreach (var element in elements)
            {
                Container.BindInstance(element).AsTransient();
            }
        }
    }
}