//  AndroidJavaBridge.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#if UNITY_ANDROID

using System.Collections.Generic;
using UnityEngine;

namespace ScaleMonk.Ads.Android
{
    public class AndroidJavaBridge : IBridge
    {
        private readonly AndroidJavaObject _adsBinding;
        private readonly AndroidJavaObject _activity;
        
        public AndroidJavaBridge()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
            _adsBinding = new AndroidJavaObject("com.scalemonk.ads.unity.binding.AdsBinding", _activity);
        }

        public void CallNativeMethod(string methodName, params object[] args)
        {
            _adsBinding.Call(methodName, args);
        }
        
        public void CallNativeMethodWithActivity(string methodName, params object[] args)
        {
            var list = new List<object> {_activity};
            foreach (var o in args)
            {
                list.Add(o);
            }

            _adsBinding.Call(methodName, list.ToArray());
        }

        public bool CallBooleanNativeMethod(string methodName, params object[] args)
        {
            return _adsBinding.Call<bool>(methodName, args);
        }
    }
}
#endif
