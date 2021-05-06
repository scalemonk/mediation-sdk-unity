//  ScaleMonkDebugMenu.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System;
using System.Collections;
using System.Collections.Generic;
using ScaleMonk.Ads;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMonkDebugMenu : MonoBehaviour
{
    public Button InitButton;
    public Button ShowInterstitialButton;
    public Button ShowRewardedVideoButton;
    public Button ShowBannerButton;
    public Button StopBannerButton;
    public Text LogField;

    private BannerPosition bannerPosition = BannerPosition.BottomCenter;
    public string menuTag = "DEBUG MENU";

    // Start is called before the first frame update
    void Start()
    {
        InitButton.onClick.AddListener(OnClickInit);
        ShowInterstitialButton.onClick.AddListener(OnClickShowInterstitial);
        ShowRewardedVideoButton.onClick.AddListener(OnClickShowRewarded);
        ShowBannerButton.onClick.AddListener(OnClickShowBanner);
        StopBannerButton.onClick.AddListener(OnClickStopBanner);
    }

    private void OnClickShowRewarded()
    {
        ScaleMonkAds.SharedInstance.ShowRewarded(menuTag);
    }

    private void OnClickShowInterstitial()
    {
        ScaleMonkAds.SharedInstance.ShowInterstitial(menuTag);
    }

    private void OnClickShowBanner()
    {
        ScaleMonkAds.SharedInstance.ShowBanner(menuTag, bannerPosition);
    }

    private void OnClickStopBanner()
    {
        ScaleMonkAds.SharedInstance.StopBanner(menuTag);
    }

    private void OnClickInit()
    {
        ScaleMonkAds.InterstitialClickedEvent += Feedback("Interstitial Clicked");
        ScaleMonkAds.RewardedClickedEvent += Feedback("Video Clicked");
        ScaleMonkAds.InterstitialDisplayedEvent += Feedback("Interstitial Displayed");
        ScaleMonkAds.RewardedDisplayedEvent += Feedback("Video Displayed");
        ScaleMonkAds.RewardedStartedEvent += Feedback("Video Started");
        ScaleMonkAds.RewardedNotDisplayedEvent += Feedback("Video Not Displayed");
        ScaleMonkAds.InterstitialNotDisplayedEvent += Feedback("Interstitial Not Displayed");
        ScaleMonkAds.InterstitialReadyEvent += Feedback("Interstitial Ready");
        ScaleMonkAds.InterstitialNotReadyEvent += Feedback("Interstitial Not Ready");
        ScaleMonkAds.RewardedReadyEvent += Feedback("Rewarded Ready");
        ScaleMonkAds.RewardedNotReadyEvent += Feedback("Rewarded Not Ready");
        ScaleMonkAds.BannerCompletedDisplayedEvent += Feedback("Banner Displayed");
        ScaleMonkAds.BannerFailedDisplayedEvent += Feedback("Banner Not Displayed");
        
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