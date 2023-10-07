using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Core.Common.Services
{
    public class TimeService
    {
        private readonly List<string> _timeStoppers = new List<string>();
        public void AddTimeStopper(string id)
        {
            if (!_timeStoppers.Contains(id))
            {
                _timeStoppers.Add(id);
                UpdateTimeSpeed();
            }
        }

        public void RemoveTimeStopper(string id)
        {
            if (_timeStoppers.Contains(id))
            {
                _timeStoppers.Remove(id);
                UpdateTimeSpeed();
            }
        }

        private void UpdateTimeSpeed()
        {
            Time.timeScale = _timeStoppers.Count == 0 ? 1 : 0;
        }
    }
}