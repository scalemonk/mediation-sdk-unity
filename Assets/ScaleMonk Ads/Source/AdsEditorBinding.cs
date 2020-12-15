//  AdsEditorBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// http://www.scalemonk.com/legal/en-US/mediation-license-agreement 
//

namespace ScaleMonk.Ads
{
    public class AdsEditorBinding : IAdsBinding
    {
        public void Initialize(ScaleMonkAds adsInstance)
        {
        }

        public void ShowInterstitial(string tag)
        {
        }

        public void ShowRewarded(string tag)
        {
        }

        public bool IsInterstitialReadyToShow(string analyticsLocation)
        {
            return true;
        }

        public bool IsRewardedVideoReadyToShow(string analyticsLocation)
        {
            return true;
        }

        public bool AreRewardedEnabled()
        {
            return true;
        }

        public bool AreInterstitialsEnabled()
        {
            return true;
        }

        public void SetHasGDPRConsent(bool consent)
        {
        }

        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
        }

        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            
        }
    }
}