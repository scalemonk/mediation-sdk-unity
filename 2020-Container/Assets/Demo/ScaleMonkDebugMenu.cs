//  ScaleMonkDebugMenu.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System;
using ScaleMonk.Ads;
using UnityEngine;
using UnityEngine.UI;

namespace Demo
{
    public class ScaleMonkDebugMenu : MonoBehaviour
    {
        public Button InitButton;
        public Button ShowInterstitialButton;
        public Button ShowRewardedVideoButton;
        public Button ShowBannerButton;
        public Button StopBannerButton;
        public Button CoppaForChild;
        public Button CoppaForNonChild;
        public Button GrantGdprConsent;
        public Button DenyGdprConsent;
        public Button DisableGdprConsent;
        public Text LogField;

        private BannerPosition bannerPosition = BannerPosition.BottomCenter;
        public string menuTag = "DEBUG MENU";
        private ScaleMonkAdsSDK scaleMonkAds => ScaleMonkAds.SharedInstance;

        // Start is called before the first frame update
        void Start()
        {
            InitButton.onClick.AddListener(OnClickInit);
            ShowInterstitialButton.onClick.AddListener(OnClickShowInterstitial);
            ShowRewardedVideoButton.onClick.AddListener(OnClickShowRewarded);
            ShowBannerButton.onClick.AddListener(OnClickShowBanner);
            StopBannerButton.onClick.AddListener(OnClickStopBanner);
            CoppaForChild.onClick.AddListener(OnClickCoppaForChild);
            CoppaForNonChild.onClick.AddListener(OnClickCoppaForNonChild);
            GrantGdprConsent.onClick.AddListener(OnGrantGdprConsent);
            DenyGdprConsent.onClick.AddListener(OnDenyGdprConsent);
            DisableGdprConsent.onClick.AddListener(OnDisableGdprConsent);
            
            ScaleMonkAds.SharedInstance.AddAnalytics(new DefaultAnalytics());
        }

        private void OnClickShowRewarded()
        {
            scaleMonkAds.ShowRewarded(menuTag);
        }

        private void OnClickShowInterstitial()
        {
            scaleMonkAds.ShowInterstitial(menuTag);
        }

        private void OnClickShowBanner()
        {
            scaleMonkAds.ShowBanner(menuTag, bannerPosition);
        }

        private void OnClickStopBanner()
        {
            scaleMonkAds.StopBanner(menuTag);
        }
        
        private void OnClickCoppaForChild()
        {
            SetCoppaStatus(CoppaStatus.ChildTreatmentTrue);
        }

        private void OnClickCoppaForNonChild()
        {
            SetCoppaStatus(CoppaStatus.ChildTreatmentFalse);
        }

        private void OnDisableGdprConsent()
        {
            SetGdprConsent(GdprConsent.NotApplicable);
        }

        private void OnDenyGdprConsent()
        {
            SetGdprConsent(GdprConsent.NotGranted);
        }

        private void OnGrantGdprConsent()
        {
            SetGdprConsent(GdprConsent.Granted);
        }

        private void SetGdprConsent(GdprConsent consent)
        {
            AdsLogger.LogInfo($"Gdpr consent {consent}");
            scaleMonkAds.SetHasGDPRConsent(consent);
        }

        private void SetCoppaStatus(CoppaStatus coppaStatus)
        {
            AdsLogger.LogInfo($"Coppa status {coppaStatus}");
            scaleMonkAds.SetIsApplicationChildDirected(coppaStatus);
        }

        private void OnClickInit()
        {
            scaleMonkAds.InterstitialClickedEvent += Feedback("Interstitial Clicked");
            scaleMonkAds.RewardedClickedEvent += Feedback("Video Clicked");
            scaleMonkAds.InterstitialDisplayedEvent += Feedback("Interstitial Displayed");
            scaleMonkAds.RewardedDisplayedEvent += Feedback("Video Displayed");
            scaleMonkAds.RewardedStartedEvent += Feedback("Video Started");
            scaleMonkAds.RewardedNotDisplayedEvent += Feedback("Video Not Displayed");
            scaleMonkAds.InterstitialNotDisplayedEvent += Feedback("Interstitial Not Displayed");
            scaleMonkAds.InterstitialReadyEvent += Feedback("Interstitial Ready");
            scaleMonkAds.InterstitialNotReadyEvent += Feedback("Interstitial Not Ready");
            scaleMonkAds.RewardedReadyEvent += Feedback("Rewarded Ready");
            scaleMonkAds.RewardedNotReadyEvent += Feedback("Rewarded Not Ready");
            scaleMonkAds.BannerCompletedDisplayedEvent += Feedback("Banner Displayed");
            scaleMonkAds.BannerFailedDisplayedEvent += Feedback("Banner Not Displayed");

            ScaleMonkAds.Initialize(() =>
                {
                    // Here the SDK is initialized and you can interact with it
                    AdsLogger.LogInfo("SDK is ready to show Ads");
                    OnClickShowBanner();
                }
            );
        }

        private Action Feedback(string start)
        {
            return () => LogField.text = start + " at " + menuTag;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}