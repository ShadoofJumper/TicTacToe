using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameCore.Services.TimerService
{
    public class TimerService : ITimerService, IFixedTickable
    {
        private readonly List<TimerHandle> _timers = new List<TimerHandle>();
        public TimerHandle RegisterTimer(TimerHandle timer)
        {
            _timers.Add(timer);
            return timer;
        }

        public void FixedTick() => TickTime(Time.fixedDeltaTime);

        private void TickTime(float delta)
        {
            for (var i = _timers.Count - 1; i != -1; --i)
            {
                var timer = _timers[i];

                if (timer.UpdateInternal(delta))
                    _timers.Remove(timer);
            }
        }
        
        public TimerHandle OnUpdateTime(float duration, Action<float> action) =>
            RegisterTimer(new TimerHandle(duration).SetOnUpdateTime(action));
        
        public TimerHandle OnFinish(float duration, Action<bool> action) => 
            RegisterTimer(new TimerHandle(duration).SetOnFinish(action));
        public TimerHandle OnComplete(float duration, Action action) => 
            RegisterTimer(new TimerHandle(duration).SetOnComplete(action));
        public TimerHandle OnBreak(float duration, Action action) => 
            RegisterTimer(new TimerHandle(duration).SetOnBreak(action));
    }
}