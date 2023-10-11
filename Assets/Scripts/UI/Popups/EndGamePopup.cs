using Plugins.Core.UI.Elements;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public sealed class EndGamePopupArgs
    {
        public readonly string Title;
        public readonly string SessionTime;

        public EndGamePopupArgs(string title, string time)
        {
            Title = title;
            SessionTime = time;
        }
    }
    public class EndGamePopup : PopupUIElement<EndGamePopupArgs, PopupOutputParameter>, IPopUp
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _time;
        [SerializeField] private Button _restartButton;

        public override void Initialize()
        {
            _restartButton.onClick.AsObservable().Subscribe(
                (x) => HideSuccessfully(PopupOutputParameter.Empty));
        }

        protected override void OnDataInitialized()
        {
            _title.text = Data.Title;
            _time.text = $"Time: {Data.SessionTime}";
        }

    }
}
