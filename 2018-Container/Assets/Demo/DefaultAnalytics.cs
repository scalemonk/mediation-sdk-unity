using System.Collections.Generic;
using ScaleMonk.Ads;
using UnityEngine;

namespace Demo
{
    class DefaultAnalytics : IAnalytics
    {
        public void SendEvent(string eventName, Dictionary<string, string> eventParams)
        {
            Debug.Log("Sending event " + eventName);
            Debug.Log(eventParams.ToDebugString());
        }
    }
}