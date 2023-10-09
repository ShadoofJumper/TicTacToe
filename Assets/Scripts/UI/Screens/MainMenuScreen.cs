﻿using Controllers.GameSetupManager;
using Plugins.Core.UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class MainMenuScreen : ScreenUIElement<object>
    {
        [SerializeField] private Button _playerVsPlayer;
        [SerializeField] private Button _playerVsComputer;
        [SerializeField] private Button _computerVsComputer;

        private GameSetupManager _gameSetupManager;
        
        [Inject]
        public void Construct(GameSetupManager gameSetupManager)
        {
            _gameSetupManager = gameSetupManager;
        }
        
        public override void Initialize()
        {
        }
        
        protected override void OnShow()
        {
            base.OnShow();

            _playerVsPlayer.onClick.AddListener(OnClickPlayerVsPlayer);
            _playerVsComputer.onClick.AddListener(OnClickPlayerVsComputer);
            _computerVsComputer.onClick.AddListener(OnClickComputerVsComputer);

        }

        protected override void OnHide()
        {
            base.OnHide();
            _playerVsPlayer.onClick.RemoveListener(OnClickPlayerVsPlayer);
            _playerVsComputer.onClick.RemoveListener(OnClickPlayerVsComputer);
            _computerVsComputer.onClick.RemoveListener(OnClickComputerVsComputer);
        }

        private void OnClickPlayerVsPlayer()
        {
            SelectMode(SessionBattleType.PlayerVsPlayer);
        }
        private void OnClickPlayerVsComputer()
        {
            SelectMode(SessionBattleType.PlayerVsPlayer);
        }
        private void OnClickComputerVsComputer()
        {
            SelectMode(SessionBattleType.PlayerVsPlayer);
        }


        private void SelectMode(SessionBattleType battleType)
        {
            _gameSetupManager.SetBattleType(battleType);
            _gameSetupManager.StartBattle();
        }
    }

}