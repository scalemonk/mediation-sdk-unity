//  IAdsBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System;
using Assets.ScaleMonk_Ads;

namespace ScaleMonk.Ads
{
    public interface IAdsBinding
    {
        void Initialize(ScaleMonkAds adsInstance);
        void ShowInterstitial(string tag);
        void ShowRewarded(string tag);
        void ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition);
        void StopBanner(string tag);
        bool IsInterstitialReadyToShow(string tag);
        bool IsRewardedVideoReadyToShow(string tag);
        bool AreRewardedEnabled();
        bool AreInterstitialsEnabled();
        [Obsolete("Use \"void SetHasGDPRConsent(GdprConsent consent)\" method instead.")]
        void SetHasGDPRConsent(bool consent);
        void SetHasGDPRConsent(GdprConsent consent);
        void SetIsApplicationChildDirected(bool isChildDirected);
        void SetUserCantGiveGDPRConsent(bool cantGiveConsent);
        void CreateAnalyticsBinding();
    }
}