//  AdsEditorBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using JetBrains.Annotations;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public class AdsEditorBinding : IAdsBinding
    {
        private ScaleMonkAdsSDK _scaleMonkAds;
        private MockBannerAd _banner;
        private UserType? _userType;
        [CanBeNull] private string _customUserId;

        public void Initialize(ScaleMonkAdsSDK adsInstance)
        {
            _scaleMonkAds = adsInstance;
            _scaleMonkAds.InitializationCompleted();
            Debug.Log("ScaleMonkAds initialized successfully");
        }

#if UNITY_2018_4_OR_NEWER
        public MockAd CreateMockAdInstance()
        {
            MockAd mockAdInstance;
            MockAd mockAdPrefab;

            var mockAdPrefabName = isPortrait() ? "Prefabs/MockAd_portrait" : "Prefabs/MockAd_landscape";
            mockAdPrefab = Resources.Load<MockAd>(mockAdPrefabName);

            mockAdInstance = GameObject.Instantiate(mockAdPrefab);
            mockAdInstance.SetScaleMonkAds(_scaleMonkAds);
            return mockAdInstance;
        }

        public MockBannerAd CreateBannerMockAdInstance(BannerSize bannerSize, BannerPosition bannerPosition)
        {
            MockBannerAd mockAdInstance;
            MockBannerAd mockAdPrefab;

            var mockAdPrefabName = isPortrait() ? "Prefabs/MockAd_banner_portrait" : "Prefabs/MockAd_banner_landscape";
            mockAdPrefab = Resources.Load<MockBannerAd>(mockAdPrefabName);

            mockAdInstance = GameObject.Instantiate(mockAdPrefab);

            var bannerCanvas = (RectTransform) mockAdInstance.transform.Find("Canvas/BgColor");

            // Remove title from Rectangle banners because it's too small to show it.
            if (bannerSize == BannerSize.Rectangle)
            {
                var textChild = bannerCanvas.transform.Find("Title");
                textChild.parent = null;
            }

            ResizeAndPositionBannerCanvas(bannerSize, bannerPosition, bannerCanvas);

            return mockAdInstance;
        }

        private static void ResizeAndPositionBannerCanvas(BannerSize bannerSize, BannerPosition bannerPosition,
            RectTransform bannerCanvas)
        {
            var editorPosition = bannerPosition.toEditorPosition();
            bannerCanvas.sizeDelta = new Vector2(bannerSize.Width, bannerSize.Height);
            bannerCanvas.anchorMin = editorPosition.AnchorMin;
            bannerCanvas.anchorMax = editorPosition.AnchorMax;
            bannerCanvas.pivot = editorPosition.Pivot;
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

        public void ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition)
        {
            Debug.Log("Banner shown at " + tag);
#if UNITY_2018_4_OR_NEWER
            if (_banner == null)
            {
                _scaleMonkAds.CompletedBannerDisplay(tag);
                _banner = CreateBannerMockAdInstance(bannerSize, bannerPosition);
            }
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

        public void CreateAnalyticsBinding()
        {
            Debug.Log("Analytics Added");
        }

        public void SetCustomUserId(string customUserId)
        {
            _customUserId = customUserId;
        }

        public void SetUserType(UserType userType)
        {
            _userType = userType;
        }

        private bool isPortrait()
        {
            return Screen.height > Screen.width;
        }
    }
}