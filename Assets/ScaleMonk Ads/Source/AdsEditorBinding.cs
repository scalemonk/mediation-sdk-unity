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
        private ScaleMonkAds _scaleMonkAds;
        private MockBannerAd _banner;
        
        public void Initialize(ScaleMonkAds adsInstance)
        {
            _scaleMonkAds = adsInstance;
            Debug.Log("ScaleMonkAds initialized successfully");
        }
        
#if UNITY_2018_4_OR_NEWER
        public MockAd CreateMockAdInstance() 
        {
            MockAd mockAdInstance;
            MockAd mockAdPrefab;
            if (isPortrait())
            {
                mockAdPrefab = Resources.Load<MockAd>("Prefabs/MockAd_portrait");
            }
            else
            {
                mockAdPrefab = Resources.Load<MockAd>("Prefabs/MockAd_landscape");
            }
            mockAdInstance = GameObject.Instantiate(mockAdPrefab);
            mockAdInstance.SetScalemonkAds(_scaleMonkAds);
            return mockAdInstance;
        }

        public MockBannerAd CreateBannerMockAdInstance()
        {
            MockBannerAd mockAdInstance;
            MockBannerAd mockAdPrefab;

            if (isPortrait())
            {
                mockAdPrefab = Resources.Load<MockBannerAd>("Prefabs/MockAd_banner_portrait");
            }
            else
            {
                mockAdPrefab = Resources.Load<MockBannerAd>("Prefabs/MockAd_banner_landscape");
            }
            
            mockAdInstance = GameObject.Instantiate(mockAdPrefab);

            return mockAdInstance;
        }
#endif
        public void ShowInterstitial(string tag)
        {
            Debug.Log("Interstitial shown at " + tag);
#if UNITY_2018_4_OR_NEWER
            var mockAdInstance = CreateMockAdInstance();
            mockAdInstance.SetMode(MockAd.Mode.Interstitial);
            mockAdInstance.SetTag(tag);
#endif
        }
        public void ShowRewarded(string tag)
        {
            Debug.Log("Rewarded shown at " + tag);
#if UNITY_2018_4_OR_NEWER
            _scaleMonkAds.StartedRewardedDisplay(tag);
            var mockAdInstance = CreateMockAdInstance();
            mockAdInstance.SetMode(MockAd.Mode.Rewarded);
            mockAdInstance.SetTag(tag);
#endif
        }

        public void ShowBanner(string tag, BannerPosition bannerPosition)
        {
            Debug.Log("Banner shown at " + tag);
#if UNITY_2018_4_OR_NEWER
            _scaleMonkAds.CompletedBannerDisplay(tag);
            _banner = CreateBannerMockAdInstance();
#endif
        }

        public void StopBanner(string tag)
        {
            Debug.Log("Banner stopped at " + tag);
#if UNITY_2018_4_OR_NEWER
            _banner.Stop();
#endif
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