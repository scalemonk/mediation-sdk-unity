//  IAdsBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System;

namespace ScaleMonk.Ads
{
    public interface IAdsBinding
    {
        void Initialize(ScaleMonkAdsSDK adsInstance);
        void ShowInterstitial(string tag);
        void ShowRewarded(string tag);
        void ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition);
        void StopBanner(string tag);
        bool IsInterstitialReadyToShow(string tag);
        bool IsRewardedVideoReadyToShow(string tag);
        bool AreRewardedEnabled();
        bool AreInterstitialsEnabled();
        void SetHasGDPRConsent(bool consent);
        void SetIsApplicationChildDirected(bool isChildDirected);
        void SetUserCantGiveGDPRConsent(bool cantGiveConsent);
        void CreateAnalyticsBinding();
        void SetCustomUserId(string customUserId);
        void SetUserType(UserType userType);
    }
}