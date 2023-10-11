using UI.HUD;
using UnityEngine;
using Zenject;

namespace GameCore.Services.SessionTimerService
{
    public class SessionTimerService : ISessionTimerService, IInitializable, ITickable
    {
        private HUDView _hudView;
        
        private float _startTime;
        private float _elapsedTime;
        
        public int MinutesFromStart => Mathf.FloorToInt(_elapsedTime / 60);
        public int SecondsFromStart => Mathf.FloorToInt(_elapsedTime % 60);

        public SessionTimerService(HUDView hudView)
        {
            _hudView = hudView;
        }
        
        public void Initialize()
        {
            _startTime = Time.time;
        }
        
        public void Tick()
        {
            _elapsedTime = Time.time - _startTime;
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            _hudView.SetTime(Mathf.FloorToInt(_elapsedTime / 60), Mathf.FloorToInt(_elapsedTime % 60));
        }
    }
}