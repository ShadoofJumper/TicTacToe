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
        [SerializeField] private Button _menuButton;

        [SerializeField] private TextMeshProUGUI _playerOneNameText;
        [SerializeField] private TextMeshProUGUI _playerTwoNameText;

        [SerializeField] private TextMeshProUGUI _playerTurnTitle;
        [SerializeField] private TextMeshProUGUI _timer;

        public event Action OnHintClickAction; 
        public event Action OnUndoClickAction; 
        public event Action OnMenuClickAction; 

        private void Awake()
        {
            _hintButton.onClick.AddListener(OnHintClick);
            _undoButton.onClick.AddListener(OnUndoClick);
            _menuButton.onClick.AddListener(OnMenuClick);
        }

        private void Start()
        {
            SetHintButtonActive(false);
            SetUndoButtonActive(false);
            _playerTurnTitle.text = "";
        }

        
        public void SetPlayerTurnTitle(string playerTurnName)
        {
            _playerTurnTitle.text = $"Turn: {playerTurnName}";
        }
        
        public void SetPlayerOneName(string playerName)
        {
            _playerOneNameText.text = playerName;
        }
        
        public void SetPlayerTwoName(string playerName)
        {
            _playerTwoNameText.text = playerName;
        }

        public void SetTime(int minutes, int seconds)
        {
            _timer.text = $"{minutes:00}:{seconds:00}";
        }
        #region Buttons
        public void SetHintButtonActive(bool value)
        {
            _hintButton.gameObject.SetActive(value);
        }

        public void SetUndoButtonActive(bool value)
        {
            _undoButton.gameObject.SetActive(value);
        }
        
        private void OnMenuClick()
        {
            OnMenuClickAction?.Invoke();
        }
        
        private void OnHintClick()
        {
            OnHintClickAction?.Invoke();
        }
        
        private void OnUndoClick()
        {
            OnUndoClickAction?.Invoke();
        }

        
        #endregion
        

        private void OnDestroy()
        {
            _hintButton.onClick.RemoveListener(OnHintClick);
            _undoButton.onClick.RemoveListener(OnUndoClick);  
            _menuButton.onClick.RemoveListener(OnMenuClick);  

        }
    }  
}

