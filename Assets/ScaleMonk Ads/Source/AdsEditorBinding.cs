//  AdsEditorBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using UnityEngine;

namespace ScaleMonk.Ads
{
    public class AdsEditorBinding : IAdsBinding
    {
        private MockAd _mockAdInstance;
        private ScaleMonkAds _scaleMonkAds;

        public void Initialize(ScaleMonkAds adsInstance)
        {
            _scaleMonkAds = adsInstance;
            Debug.Log("ScaleMonkAds initialized successfully");
#if UNITY_2018_4_OR_NEWER
            MockAd mockAdPrefab;
            if (isPortrait())
            {
                mockAdPrefab = Resources.Load<MockAd>("Prefabs/MockAd_portrait");
            }
            else
            {
                mockAdPrefab = Resources.Load<MockAd>("Prefabs/MockAd_landscape");
            }

            _mockAdInstance = GameObject.Instantiate(mockAdPrefab);
            _mockAdInstance.gameObject.SetActive(false);      
#endif
        }

        public void ShowInterstitial(string tag)
        {
            Debug.Log("Interstitial shown at " + tag);
#if UNITY_2018_4_OR_NEWER
            _mockAdInstance.gameObject.SetActive(true);
            _mockAdInstance.SetText("AN INTERSTITIAL AD WILL BE DISPLAYED HERE");
#endif
            _scaleMonkAds.CompletedInterstitialDisplay(tag);
        }

        public void ShowRewarded(string tag)
        {
            _scaleMonkAds.StartedRewardedDisplay(tag);
            Debug.Log("Rewarded shown at " + tag);
#if UNITY_2018_4_OR_NEWER
            _mockAdInstance.gameObject.SetActive(true);
            _mockAdInstance.SetText("A REWARDED AD WILL BE DISPLAYED HERE");
#endif
            _scaleMonkAds.CompletedRewardedDisplay(tag);
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

        private bool isPortrait()
        {
            return Screen.height > Screen.width;
        }
    }
}
