//  AdsAndroidBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

namespace ScaleMonk.Ads.Android
{
    public class AdsAndroidBinding : IAdsBinding
    {
        private readonly IBridge _androidJavaBridge;

        public AdsAndroidBinding(IBridge androidJavaBridge)
        {
            _androidJavaBridge = androidJavaBridge;
        }

        public void Initialize(ScaleMonkAdsSDK adsInstance)
        {
            _androidJavaBridge.CallNativeMethod("initialize");
        }
        
        public void ShowInterstitial(string tag)
        {
            _androidJavaBridge.CallNativeMethodWithActivity("showInterstitial", tag);
        }

        public void ShowRewarded(string tag)
        {
            _androidJavaBridge.CallNativeMethodWithActivity("showRewarded", tag);
        }

        public void ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition)
        {
            _androidJavaBridge.CallNativeMethodWithActivity("showBanner",
                bannerPosition.ToSnakeCaseString(),
                tag,
                bannerSize.Width,
                bannerSize.Height);
        }

        public void StopBanner(string tag)
        {
            _androidJavaBridge.CallNativeMethodWithActivity("stopBanner", tag);
        }

        public bool IsInterstitialReadyToShow(string tag)
        {
            return _androidJavaBridge.CallBooleanNativeMethod("isInterstitialReadyToShow", tag);
        }

        public bool IsRewardedVideoReadyToShow(string tag)
        {
            return _androidJavaBridge.CallBooleanNativeMethod("isRewardedVideoReadyToShow", tag);
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
            _androidJavaBridge.CallNativeMethod("setHasGDPRConsent", consent);
        }

        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
            _androidJavaBridge.CallNativeMethod("setIsApplicationChildDirected", isChildDirected);
        }

        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            _androidJavaBridge.CallNativeMethod("setUserCantGiveGDPRConsent", cantGiveConsent);
        }

        public void CreateAnalyticsBinding()
        {
            _androidJavaBridge.CallNativeMethod("addAnalytics");
        }

        public void SetCustomUserId(string customUserId)
        {
            _androidJavaBridge.CallNativeMethod("setCustomUserId", customUserId);
        }
    }
}