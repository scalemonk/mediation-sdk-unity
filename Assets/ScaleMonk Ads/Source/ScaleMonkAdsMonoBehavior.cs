//  ScaleMonkAdsMonoBehavior.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// http://www.scalemonk.com/legal/en-US/mediation-license-agreement 
//

using UnityEngine;

namespace ScaleMonk.Ads
{
    public class ScaleMonkAdsMonoBehavior : MonoBehaviour
    {
        const string _label = "AdsMonoBehaviour";

        static GameObject _gameObject;
        static ScaleMonkAds _adsInstance;

        public static void Initialize(ScaleMonkAds adsInstance)
        {
            _adsInstance = adsInstance;
            CreateGameObject();
        }

        static void CreateGameObject()
        {
            if (_gameObject != null)
            {
                Debug.Log("AdsMonoBehaviour game object already created.");
                return;
            }

            _gameObject = new GameObject("AdsMonoBehaviour");
            _gameObject.AddComponent<ScaleMonkAdsMonoBehavior>();
            DontDestroyOnLoad(_gameObject);
        }

        public void CompletedVideoDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Completed video display at location \"{1}\"", _label, location);
            _adsInstance.CompletedRewardedDisplay(location);
        }

        public void StartedVideoDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Started video display at location \"{1}\"", _label, location);
            _adsInstance.StartedRewardedDisplay(location);
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
    
    }
}