using System;

namespace GameCore.Services.TimerService
{
    public class TimerCallbacks
    {
        public bool HasUpdateTimeCallback;
        public bool HasUpdateRatioCallback;
        
        public Action OnStart { get; set; }
        public Action<float> OnUpdateTime { get; set; }
        public Action<float> OnUpdateRatio { get; set; }
        public Action OnComplete { get; set; }
        public Action OnBreak { get; set; }
        public Action<bool> OnFinish { get; set; }

        public void CallOnUpdate(float time, float ratio)
        {
            if (HasUpdateTimeCallback)
                OnUpdateTime.Invoke(time);
            
            if (HasUpdateRatioCallback)
                OnUpdateRatio.Invoke(ratio);
        }
    }
}