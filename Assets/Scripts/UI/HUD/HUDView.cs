using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private Button _undoButton;
        [SerializeField] private Button _hintButton;

        [SerializeField] private TextMeshProUGUI _playerOneNameText;
        [SerializeField] private TextMeshProUGUI _playerTwoNameText;

        
        public event Action OnHintClickAction; 
        public event Action OnUndoClickAction; 

        private void Awake()
        {
            _undoButton.onClick.AddListener(OnHintClick);
            _undoButton.onClick.AddListener(OnUndoClick);
        }

        public void SetPlayerOneName(string playerName)
        {
            _playerOneNameText.text = playerName;
        }
        
        public void SetPlayerTwoName(string playerName)
        {
            _playerTwoNameText.text = playerName;
        }
        
        public void SetDisableButtons(bool value)
        {
            _undoButton.interactable = value;
            _hintButton.interactable = value;
        }
        
        private void OnHintClick()
        {
            OnHintClickAction?.Invoke();
        }
        
        private void OnUndoClick()
        {
            OnUndoClickAction?.Invoke();
        }

        private void OnDestroy()
        {
            _undoButton.onClick.RemoveListener(OnHintClick);
            _undoButton.onClick.RemoveListener(OnUndoClick);        }
    }  
}

