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

        public string ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition)
        {
            return _androidJavaBridge.CallStringNativeMethodWithActivity("showBanner",
                bannerPosition.ToSnakeCaseString(),
                tag,
                bannerSize.Width,
                bannerSize.Height);
        }

        public void StopBanner(string id)
        {
            _androidJavaBridge.CallNativeMethodWithActivity("stopBanner", id);
        }
        
        public void StopBanner()
        {
            _androidJavaBridge.CallNativeMethodWithActivity("stopBanner");
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

        public void SetCustomUserId(string customUserId)
        {
            _androidJavaBridge.CallNativeMethod("setCustomUserId", customUserId);
        }

        public void SetUserType(UserType userType)
        {
            _androidJavaBridge.CallNativeMethod("setUserType", userType.ToStringUserType());
        }
    }
}