using System;

namespace GameCore.Services.TimerService
{
    public interface ITimerService
    {
        TimerHandle RegisterTimer(TimerHandle timer);

        TimerHandle OnUpdateTime(float duration, Action<float> action);
        
        /// <summary>
        /// when timer stopped
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="action"></param>
        /// <returns>false if broken</returns>
        TimerHandle OnFinish(float duration, Action<bool> action);
        
        /// <summary>
        /// when successfully finished timer
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        TimerHandle OnComplete(float duration, Action action);
        
        /// <summary>
        /// when timer broke
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        TimerHandle OnBreak(float duration, Action action);
    }
}