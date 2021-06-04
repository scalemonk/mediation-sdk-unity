//  AdsAndroidBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#if UNITY_ANDROID
using UnityEngine;

namespace ScaleMonk.Ads.Android
{
    public class AdsAndroidBinding : IAdsBinding
    {
        const string _label = "AdsAndroidBinding";
        private AndroidJavaObject _adsBinding;
        private AndroidJavaObject _activity;
        private AndroidJavaObject _analytics;
        private IAnalytics extraAnalytics;

        public void Initialize(ScaleMonkAds adsInstance)
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
            _adsBinding = new AndroidJavaObject("com.scalemonk.ads.unity.binding.AdsBinding", _activity);
        }

        public void ShowInterstitial(string tag)
        {
            _adsBinding.Call("showInterstitial", _activity, tag);
        }

        public void ShowRewarded(string tag)
        {
            _adsBinding.Call("showRewarded", _activity, tag);
        }

        public void ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition)
        {
            _adsBinding.Call("showBanner", _activity,
                bannerPosition.ToSnakeCaseString(),
                tag,
                bannerSize.Width,
                bannerSize.Height);
        }

        public void StopBanner(string tag)
        {
            _adsBinding.Call("stopBanner", _activity, tag);
        }

        public bool IsInterstitialReadyToShow(string tag)
        {
            return _adsBinding.Call<bool>("isInterstitialReadyToShow", tag);
        }

        public bool IsRewardedVideoReadyToShow(string tag)
        {
            return _adsBinding.Call<bool>("isRewardedVideoReadyToShow", tag);
        }

        public bool AreRewardedEnabled()
        {
            // TODO: Not yet implemented
            return false;
        }

        public bool AreInterstitialsEnabled()
        {
            // TODO: Not yet implemented
            return false;
        }

        public void SetHasGDPRConsent(bool consent)
        {
            _adsBinding.Call("setHasGDPRConsent", consent);
        }

        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
            _adsBinding.Call("setIsApplicationChildDirected", isChildDirected);
        }

        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            _adsBinding.Call("setUserCantGiveGDPRConsent", cantGiveConsent);
        }

        public void CreateAnalyticsBinding()
        {
            _adsBinding.Call("addAnalytics");
        }

        public void SetCustomUserId(string customUserId)
        {
            _adsBinding.Call("setCustomUserId", customUserId);
        }
    }
}
#endif