using System;
using UnityEngine;

namespace GameCore.Services.TimerService
{
 public class TimerHandle
    {
        public float Duration;
        public int ID;
        
        readonly TimerCallbacks _optional = new TimerCallbacks();

        private bool _hasStartCallback;
        private bool _hasUpdateCallback;
        private bool _hasCompleteCallback;
        private bool _hasFinishCallback;
        private bool _hasBreakCallback;

        private bool _isInitialized;
        private bool _isBroken;
        private bool _isCompleted;

        private float _passedTime;

        public float TimeLeft => Duration - _passedTime;
        
        public TimerHandle(float duration) => Duration = duration;

        public TimerHandle SetOnStart(Action onStart)
        {
            _optional.OnStart = onStart;
            _hasStartCallback = true;
            return this;
        }
        
        public TimerHandle SetOnUpdateTime(Action<float> onUpdateTime)
        {
            _optional.OnUpdateTime = onUpdateTime;
            _optional.HasUpdateTimeCallback = true;
            _hasUpdateCallback = true;
            return this;
        }
        
        public TimerHandle SetOnUpdateRatio(Action<float> onUpdateRatio)
        {
            _optional.OnUpdateRatio = onUpdateRatio;
            _optional.HasUpdateRatioCallback = true;
            _hasUpdateCallback = true;
            return this;
        }
        
        public TimerHandle SetOnComplete(Action onComplete)
        {
            _optional.OnComplete = onComplete;
            _hasCompleteCallback = true;
            return this;
        }
        
        public TimerHandle SetOnBreak(Action onBreak)
        {
            _optional.OnBreak = onBreak;
            _hasBreakCallback = true;
            return this;
        }
        
        public TimerHandle SetOnFinish(Action<bool> onFinish)
        {
            _optional.OnFinish = onFinish;
            _hasFinishCallback = true;
            return this;
        }
        
        public bool UpdateInternal(float delta)
        {
            if (!_isInitialized)
                Init();
            
            if (_isBroken || _isCompleted)
                return true;

            _passedTime += delta;

            if (_hasUpdateCallback)
                _optional.CallOnUpdate(_passedTime, _passedTime / Duration);

            if (_passedTime >= Duration)
            {
                _isCompleted = true;
                
                if (_hasCompleteCallback)
                {
                    _optional.OnComplete.Invoke();
                }

                if (_hasFinishCallback)
                {
                    _optional.OnFinish.Invoke(true);
                }

                return true;
            }

            return false;
        }

        public void Break()
        {
            if (_isCompleted || _isBroken)
                return;

            if (_hasBreakCallback)
                _optional.OnBreak.Invoke();
            
            if (_hasFinishCallback)
                _optional.OnFinish.Invoke(false);
            _isBroken = true;
        }

        void Init()
        {
            _isInitialized = true;
            
            if (_hasStartCallback)
                _optional.OnStart.Invoke();

            if (Duration <= 0f)
                Duration = Mathf.Epsilon;

            _passedTime = 0f;
        }
    }
}