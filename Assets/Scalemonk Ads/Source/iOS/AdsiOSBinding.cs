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

        public void ShowVideo(string tag)
        {
            SMAdsShowVideo(tag);
        }

        public bool IsInterstitialReadyToShow(string analyticsLocation)
        {
            return SMIsInterstitialReadyToShow(analyticsLocation);
        }

        public bool IsRewardedVideoReadyToShow(string analyticsLocation)
        {
            return SMIsRewardedVideoReadyToShow(analyticsLocation);
        }

        public bool AreVideosEnabled()
        {
            return SMAreVideosEnabled();
        }

        public bool AreInterstitialsEnabled()
        {
            return SMAreInterstitialsEnabled();
        }

        #region callbacks from native binding

        public void CompletedVideoDisplay(string location)
        {
            Debug.LogFormat("[{0}] Completed video display at location {1}", _label, location);
            _adsInstance.CompletedVideoDisplay(location);
        }

        public void ClickedVideo(string location)
        {
            Debug.LogFormat("[{0}] Clicked video at location {1}", _label, location);
            _adsInstance.ClickedVideo(location);
        }

        public void FailedVideoDisplay(string location)
        {
            Debug.LogFormat("[{0}] Failed video display at location {1}", _label, location);
            _adsInstance.FailedVideoDisplay(location);
        }

        public void CompletedInterstitialDisplay(string location)
        {
            Debug.LogFormat("[{0}] Completed interstitial display at location {1}", _label, location);
            _adsInstance.CompletedInterstitialDisplay(location);
        }

        public void ClickedInterstitial(string location)
        {
            Debug.LogFormat("[{0}] Clicked interstitial at location {1}", _label, location);
            _adsInstance.ClickedInterstitial(location);
        }

        public void FailedInterstitialDisplay(string location)
        {
            Debug.LogFormat("[{0}] Failed interstitial display at location {1}", _label, location);
            _adsInstance.FailedInterstitialDisplay(location);
        }

        #endregion

        #region dll imports

        [DllImport("__Internal")]
        private static extern void SMAdsInitialize(string applicationId);

        [DllImport("__Internal")]
        private static extern void SMAdsShowInterstitial(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern void SMAdsShowVideo(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern bool SMIsInterstitialReadyToShow(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern bool SMIsRewardedVideoReadyToShow(string analyticsLocation);

        [DllImport("__Internal")]
        private static extern bool SMAreInterstitialsEnabled();

        [DllImport("__Internal")]
        private static extern bool SMAreVideosEnabled();

        #endregion
    
    }
}