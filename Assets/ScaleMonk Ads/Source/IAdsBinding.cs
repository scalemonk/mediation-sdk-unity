//  IAdsBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// http://www.scalemonk.com/legal/en-US/mediation-license-agreement 
//

namespace ScaleMonk.Ads
{
    public interface IAdsBinding
    {
        void Initialize(ScaleMonkAds adsInstance);
        void ShowInterstitial(string tag);
        void ShowRewarded(string tag);
        bool IsInterstitialReadyToShow(string analyticsLocation);
        bool IsRewardedVideoReadyToShow(string analyticsLocation);
        bool AreRewardedEnabled();
        bool AreInterstitialsEnabled();
        void SetHasGDPRConsent(bool consent);
        void SetIsApplicationChildDirected(bool isChildDirected);
        void SetUserCantGiveGDPRConsent(bool cantGiveConsent);
    }
}