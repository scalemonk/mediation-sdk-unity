//  AdsiOSBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#if UNITY_IOS

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections.Generic;

namespace ScaleMonk.Ads.iOS
{
    public class AdsiOSBinding : IAdsBinding
    {
        const string _label = "AdsIOSBinding";
        ScaleMonkAdsSDK _adsInstance;

        public void Initialize(ScaleMonkAdsSDK adsInstance)
        {
            _adsInstance = adsInstance;
            SMAdsInitialize();
        }

        public void ShowInterstitial(string tag)
        {
            SMAdsShowInterstitial(tag);
        }

        public void ShowRewarded(string tag)
        {
            SMAdsShowRewarded(tag);
        }

        public string ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition)
        {
            return SMAdsShowBanner(tag, bannerSize.Width, bannerSize.Height, bannerPosition.ToSnakeCaseString());
        }
        
        public void StopBanner(string id)
        {
            SMAdsStopBannerWithBannerId(id);
        }

        public void StopBanner()
        {
            SMAdsStopBanner();
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

        [Obsolete("Use \"void SetHasGDPRConsent(GdprConsent status)\" method instead.")]
        public void SetHasGDPRConsent(bool consent)
        {
            // Using this implementation until iOS native one is in place
            SMSetHasGDPRConsent(consent);
        }

        public void SetHasGDPRConsent(GdprConsent consent)
        {
            // Using the above implementation until iOS native one is in place
            switch (consent)
            {
                case GdprConsent.Granted:
                    SetHasGDPRConsent(true);
                    break;
                case GdprConsent.NotGranted:
                    SetHasGDPRConsent(false);
                    break;
                case GdprConsent.NotApplicable:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(consent), consent, null);
            }
            
        }

        [Obsolete("Use \"void SetIsApplicationChildDirected(CoppaStatus status)\" method instead.")]
        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
            SMSetApplicationChildDirected(isChildDirected);
        }

        public void SetIsApplicationChildDirected(CoppaStatus status)
        {
            SMSetApplicationChildDirectedStatus(status.ToInt());
        }

        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            SMSetUserCantGiveGDPRConsent(cantGiveConsent);
        }
        
        public void SetCustomUserId(string customUserId)
        {
            SMSetCustomUserId(customUserId);
        }

        [Obsolete("Use \"void SetCustomSegmentationTags(HashSet<String tags)\" method instead.")]
        public void SetUserType(UserType userType)
        {
            SMSetUserType(userType.ToStringUserType());
        }

        public void SetCustomSegmentationTags(HashSet<String> tags)
        {
            SMSetCustomSegmentationTags(string.Join(",", tags));
        }

        #region callbacks from native binding

        public void CompletedRewardedDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Completed rewarded display at location \"{1}\"", _label, location);
            _adsInstance.CompletedRewardedDisplay(location);
        }

        public void ClickedRewarded(string location)
        {
            AdsLogger.LogWithFormat("{0} | Clicked rewarded at location \"{1}\"", _label, location);
            _adsInstance.ClickedRewarded(location);
        }

        public void FailedRewardedDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Failed rewarded display at location \"{1}\"", _label, location);
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
            AdsLogger.LogWithFormat("{0} | Interstitial ready to display", _label);
            _adsInstance.InterstitialReady();
        }
        
        public void InterstitialNotReady()
        {
            AdsLogger.LogWithFormat("{0} | Interstitial not ready to display", _label);
            _adsInstance.InterstitialNotReady();
        }
        
        public void RewardedReady()
        {
            AdsLogger.LogWithFormat("{0} | Rewarded ready to display", _label);
            _adsInstance.RewardedReady();
        }
        
        public void RewardedNotReady()
        {
            AdsLogger.LogWithFormat("{0} | Rewarded not ready to display", _label);
            _adsInstance.RewardedNotReady();
        }

        #endregion

        #region dll imports

        [DllImport("__Internal")]
        private static extern void SMAdsInitialize();

        [DllImport("__Internal")]
        private static extern void SMAdsShowInterstitial(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern void SMAdsShowRewarded(string analyticsLocation);
        
        [DllImport("__Internal")]
        private static extern string SMAdsShowBanner(string analyticsLocation, int width, int height, string position);
        
        [DllImport("__Internal")]
        private static extern void SMAdsStopBannerWithBannerId(string bannerId);
        
        [DllImport("__Internal")]
        private static extern void SMAdsStopBanner();

        [DllImport("__Internal")]
        private static extern bool SMIsInterstitialReadyToShow(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern bool SMIsRewardedReadyToShow(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern bool SMAreInterstitialsEnabled();

        [DllImport("__Internal")]
        private static extern bool SMAreRewardedEnabled();
        
        [Obsolete("Use \"SMSetApplicationChildDirectedStatus(int status)\" method instead.")]
        [DllImport("__Internal")]
        private static extern void SMSetApplicationChildDirected(bool isChildDirected);

        [DllImport("__Internal")]
        private static extern void SMSetApplicationChildDirectedStatus(int status);
        
        [DllImport("__Internal")]
        private static extern void SMSetHasGDPRConsent(bool consent);
        
        [DllImport("__Internal")]
        private static extern void SMSetUserCantGiveGDPRConsent(bool isUnderage);
        
        [DllImport("__Internal")]
        private static extern void SMSetCustomUserId(string customUserId);
        
        [DllImport("__Internal")]
        private static extern void SMSetUserType(string userType);

        [DllImport("__Internal")]
        private static extern void SMSetCustomSegmentationTags(string tags);

        #endregion

    }
}
#endif