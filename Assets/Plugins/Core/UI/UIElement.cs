using System;
using Plugins.Core.UI.Animations;
using Plugins.Core.UI.Elements;
using UniRx;
using UnityEngine;
using Zenject;

namespace Plugins.Core.UI
{
    public abstract class UIElement<T> : UIElement, IShowable
    {
        private bool _dataIsSet;
        protected T Data;

        public UIElement<T> SetData(T data)
        {
            Data = data;
            _dataIsSet = true;
            OnDataInitialized();
            return this;
        }
        
        public UIElement<T> Show(T data)
        {
            Data = data;
            _dataIsSet = true;
            OnDataInitialized();
            ShowInternal();
            return this;
        }

        protected abstract void OnDataInitialized();
        
        public void Close() => HideInternal();
        public void Show()
        {
            var data = this as INoData;
            if (data == null && !_dataIsSet)
            {
                throw new InvalidOperationException(
                    $"Can not show {GetType().Name} before initialization {nameof(SetData)}(T)/{nameof(Show)}(T)");
            }

            ShowInternal();
        }

        public void Hide() => HideInternal();
    }

    public abstract class UIElement : MonoBehaviour, IInitializable
    {
        protected IUIManager UIManager;
        protected abstract IUIElementAnimator Animator { get; }
        private readonly Subject<bool> _visibilitySubject = new Subject<bool>();

        protected Action OnCompleteAnimateShow;

        [Inject]
        private void Init(IUIManager uiManager)
        {
            UIManager = uiManager;
        }

        protected void ShowInternal()
        {
            OnShow();
            gameObject.SetActive(true);
            Animator.AnimateShow(this, () =>
            {
                _visibilitySubject.OnNext(true);
                OnCompleteAnimateShow?.Invoke();
            });
            
            UIManager.IncreaseOpenedUIElementsCounter();
        }

        protected void HideInternal()
        {
            OnHide();
            Animator.AnimateHide(this, () =>
            {
                _visibilitySubject.OnNext(false);
                Destroy(gameObject);
            });
            
            UIManager.DecreaseOpenedUIElementsCounter();
        }

        protected virtual void OnShow()
        {
        }

        protected virtual void OnHide()
        {
        }

        public IDisposable SubscribeOnShow(Action<UIElement> OnShow) =>
            _visibilitySubject.Where(x => x).Subscribe(_ => OnShow(this));

        public IDisposable SubscribeOnClose(Action<UIElement> OnHide) =>
            _visibilitySubject.Where(x => !x).Subscribe(_ => OnHide(this));

        public abstract void Initialize();
    }
}