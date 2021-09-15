//  AdsAndroidBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html
//


using System;
using System.Collections.Generic;
using System.Linq;

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
            return _androidJavaBridge.CallBooleanNativeMethod("isRewardedReadyToShow", tag);
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

        [Obsolete("Use \"void SetHasGDPRConsent(GdprConsent status)\" method instead.")]
        public void SetHasGDPRConsent(bool consent)
        {
            var newConsent = consent ? GdprConsent.Granted : GdprConsent.NotGranted;
            SetHasGDPRConsent(newConsent);
        }

        public void SetHasGDPRConsent(GdprConsent consent)
        {
            _androidJavaBridge.CallNativeMethod("setHasGDPRConsent", (int) consent);
        }

        [Obsolete("Use \"void SetIsApplicationChildDirected(CoppaStatus status)\" method instead.")]
        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
            _androidJavaBridge.CallNativeMethod("setIsApplicationChildDirected", isChildDirected);
        }

        public void SetIsApplicationChildDirected(CoppaStatus status)
        {
            _androidJavaBridge.CallNativeMethod("setIsApplicationChildDirected", status.ToInt());
        }

        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            _androidJavaBridge.CallNativeMethod("setUserCantGiveGDPRConsent", cantGiveConsent);
        }

        public void SetCustomUserId(string customUserId)
        {
            _androidJavaBridge.CallNativeMethod("setCustomUserId", customUserId);
        }

        public void SetCustomSegmentationTags(HashSet<String> tags)
        {
            // We need to transform it to array because Unity 2017 doesn't support Join with HashSet
            _androidJavaBridge.CallNativeMethod("setCustomSegmentationTags", string.Join(",", tags.ToArray()));
        }
        
        [Obsolete("Use \"void SetCustomSegmentationTags(HashSet<String tags)\" method instead.")]
        public void SetUserType(UserType userType)
        {
            _androidJavaBridge.CallNativeMethod("setUserType", userType.ToStringUserType());
        }
    }
}