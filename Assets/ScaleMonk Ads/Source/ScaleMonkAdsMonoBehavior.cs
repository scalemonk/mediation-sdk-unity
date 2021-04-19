//  ScaleMonkAdsMonoBehavior.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using UnityEngine;

namespace ScaleMonk.Ads
{
    public class ScaleMonkAdsMonoBehavior : MonoBehaviour
    {
        const string _label = "ScaleMonkAdsMonoBehavior";

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

        public void CompletedRewardedDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Completed video display at location \"{1}\"", _label, location);
            _adsInstance.CompletedRewardedDisplay(location);
        }

        public void StartedRewardedDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Started video display at location \"{1}\"", _label, location);
            _adsInstance.StartedRewardedDisplay(location);
        }

        public void ClickedRewarded(string location)
        {
            AdsLogger.LogWithFormat("{0} | Clicked video at location \"{1}\"", _label, location);
            _adsInstance.ClickedRewarded(location);
        }

        public void FailedRewardedDisplay(string location)
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
    
        public void FailedBannerDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Failed banner display at location \"{1}\"", _label, location);
            _adsInstance.FailedBannerDisplay(location);
        }

        public void CompletedBannerDisplay(string location)
        {
            AdsLogger.LogWithFormat("{0} | Completed banner display at location \"{1}\"", _label, location);
            _adsInstance.CompletedBannerDisplay(location);
        }
    }
}