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
    public Text LogField;

    public string menuTag = "DEBUG MENU";

    // Start is called before the first frame update
    void Start()
    {
        InitButton.onClick.AddListener(OnClickInit);
        ShowInterstitialButton.onClick.AddListener(OnClickShowInterstitial);
        ShowRewardedVideoButton.onClick.AddListener(OnClickShowRewarded);
    }

    private void OnClickShowRewarded()
    {
        ScaleMonkAds.SharedInstance.ShowRewarded(menuTag);
    }

    private void OnClickShowInterstitial()
    {
        ScaleMonkAds.SharedInstance.ShowInterstitial(menuTag);
    }

    private void OnClickInit()
    {
        ScaleMonkAds.Initialize("sm-test-app-scalemonk-6407705726");
        ScaleMonkAds.InterstitialClickedEvent += Feedback("Interstitial Clicked");
        ScaleMonkAds.RewardedClickedEvent += Feedback("Video Clicked");
        ScaleMonkAds.InterstitialDisplayedEvent += Feedback("Interstitial Displayed");
        ScaleMonkAds.RewardedDisplayedEvent += Feedback("Video Displayed");
        ScaleMonkAds.RewardedStartedEvent += Feedback("Video Started");
        ScaleMonkAds.RewardedNotDisplayedEvent += Feedback("Video Not Displayed");
        ScaleMonkAds.InterstitialNotDisplayedEvent += Feedback("Interstitial Not Displayed");
        ScaleMonkAds.InterstitialReadyEvent += Feedback("Interstitial Ready");
        ScaleMonkAds.RewardedReadyEvent += Feedback("Rewarded Video Ready");
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