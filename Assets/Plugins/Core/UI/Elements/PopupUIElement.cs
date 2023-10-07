using System;
using Plugins.Core.UI.Animations;
using Plugins.Core.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Core.UI.Elements
{
    public class PopupInputParameter
    {
        public static readonly PopupInputParameter Empty = new PopupInputParameter();
    }
    
    public class PopupOutputParameter
    {
        public static readonly PopupOutputParameter Empty = new PopupOutputParameter();
    }
    
    public abstract class PopupUIElement<T> : UIElement<T>, IScreen
    {
        protected override IUIElementAnimator Animator => new ScaleWindowAnimator();
    }

    public abstract class PopupUIElement<T, TArgs> : PopupUIElement<T>
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private bool isModal;
        [SerializeField] private bool isModalClose;
        
        private const string MODAL_BKG_NAME = "Background";
        private readonly Color _modalBkgColor= new Color(0, 0, 0, .87f);

        protected ExtImage _modalBkg;
        
        protected Action<TArgs> OnClosedAccepted;
        protected Action OnClosedDeclined;

        private bool _isShow = false;

        public override void Initialize()
        {
            CreateModalBkg();
        }

        private void CreateModalBkg()
        {
            if (isModal)
            {
                _modalBkg = UIUtils.CreateUIItem<ExtImage>(MODAL_BKG_NAME, transform);
                _modalBkg.color = _modalBkgColor;
                
                var modalBkgRectTransform = _modalBkg.rectTransform;
                modalBkgRectTransform.SetAsFirstSibling();
                modalBkgRectTransform.SetFullSpaceAnchors();
                modalBkgRectTransform.localScale = Vector3.one;

                if (isModalClose) 
                    _modalBkg.Action = HideDeclining;
            }
        }
        
        public UIElement<T> Show(T data, Action<TArgs> onClosedAccepted = null, Action onClosedDeclined = null)
        {
            base.Show(data);
            
            OnClosedAccepted = onClosedAccepted;
            OnClosedDeclined = onClosedDeclined;
            
            _closeButton?.onClick.AddListener(HideDeclining);

            _isShow = true;

            return this;
        }

        protected void HideSuccessfully(TArgs args)
        {
            if (_isShow)
            {
                _isShow = false;
                OnClosedAccepted?.Invoke(args);
                Close();
            }
        }

        protected void HideDeclining()
        {
            if (_isShow)
            {
                _isShow = false;
                OnClosedDeclined?.Invoke();
                Close();
            }
        }

        protected override void OnHide()
        {
            base.OnHide();
            
            _closeButton?.onClick.RemoveListener(HideDeclining);
        }
    }
}