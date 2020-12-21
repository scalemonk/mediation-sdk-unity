//  AdsiOSBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System.Runtime.InteropServices;
using UnityEngine;

namespace ScaleMonk.Ads.iOS
{
    public class AdsiOSBinding :IAdsBinding
    {
        const string _label = "AdsIOSBinding";
        ScaleMonkAds _adsInstance;

        public void Initialize(ScaleMonkAds adsInstance)
        {
            _adsInstance = adsInstance;
            SMAdsInitialize(adsInstance.ApplicationId);
        }

        public void ShowInterstitial(string tag)
        {
            SMAdsShowInterstitial(tag);
        }

        public void ShowRewarded(string tag)
        {
            SMAdsShowRewarded(tag);
        }

        public bool IsInterstitialReadyToShow(string analyticsLocation)
        {
            return SMIsInterstitialReadyToShow(analyticsLocation);
        }

        public bool IsRewardedVideoReadyToShow(string analyticsLocation)
        {
            return SMIsRewardedReadyToShow(analyticsLocation);
        }

        public bool AreRewardedEnabled()
        {
            return SMAreRewardedEnabled();
        }

        public bool AreInterstitialsEnabled()
        {
            return SMAreInterstitialsEnabled();
        }

        public void SetHasGDPRConsent(bool consent)
        {
            SMSetHasGDPRConsent(consent);
        }

        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
            SMSetApplicationChildDirected(isChildDirected);
        }

        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            SMSetUserCantGiveGDPRConsent(cantGiveConsent);
        }

        #region callbacks from native binding

        public void CompletedVideoDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Completed video display at location \"{1}\"", _label, location);
            _adsInstance.CompletedRewardedDisplay(location);
        }

        public void ClickedVideo(string location)
        {
            AdsLogger.LogWithFormat("{0} | Clicked video at location \"{1}\"", _label, location);
            _adsInstance.ClickedRewarded(location);
        }

        public void FailedVideoDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Failed video display at location \"{1}\"", _label, location);
            _adsInstance.FailedRewardedDisplay(location);
        }

        public void CompletedInterstitialDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Completed interstitial display at location \"{1}\"", _label, location);
            _adsInstance.CompletedInterstitialDisplay(location);
        }

        public void ClickedInterstitial(string location)
        {
            AdsLogger.LogWithFormat("{0} | Clicked interstitial at location \"{1}\"", _label, location);
            _adsInstance.ClickedInterstitial(location);
        }

        public void FailedInterstitialDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Failed interstitial display at location \"{1}\"", _label, location);
            _adsInstance.FailedInterstitialDisplay(location);
        }
        
        public void InterstitialReady()
        {
            AdsLogger.LogWithFormat("{0} | Interstitial display to display", _label);
            _adsInstance.InterstitialReady();
        }
        public void VideoReady()
        {
            AdsLogger.LogWithFormat("{0} | Rewarded display to display", _label);
            _adsInstance.RewardedReady();
        }

        #endregion

        #region dll imports

        [DllImport("__Internal")]
        private static extern void SMAdsInitialize(string applicationId);

        [DllImport("__Internal")]
        private static extern void SMAdsShowInterstitial(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern void SMAdsShowRewarded(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern bool SMIsInterstitialReadyToShow(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern bool SMIsRewardedReadyToShow(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern bool SMAreInterstitialsEnabled();

        [DllImport("__Internal")]
        private static extern bool SMAreRewardedEnabled();
        
        [DllImport("__Internal")]
        private static extern void SMSetApplicationChildDirected(bool isChildDirected);
        
        [DllImport("__Internal")]
        private static extern void SMSetHasGDPRConsent(bool consent);
        
        [DllImport("__Internal")]
        private static extern void SMSetUserCantGiveGDPRConsent(bool isUnderage);

        #endregion
    
    }
}