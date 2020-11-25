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

    public string tag = "DEBUG MENU";

    // Start is called before the first frame update
    void Start()
    {
        InitButton.onClick.AddListener(OnClickInit);
        ShowInterstitialButton.onClick.AddListener(OnClickShowInterstitial);
        ShowRewardedVideoButton.onClick.AddListener(OnClickShowRewarded);
    }

    private void OnClickShowRewarded()
    {
        ScaleMonkAds.SharedInstance.ShowVideo(tag);
    }

    private void OnClickShowInterstitial()
    {
        ScaleMonkAds.SharedInstance.ShowInterstitial(tag);
    }

    private void OnClickInit()
    {
        ScaleMonkAds.Initialize("sm-test-app-scalemonk-6407705726");
        ScaleMonkAds.InterstitialClickedEvent += Feedback("Interstitial Clicked");
        ScaleMonkAds.VideoClickedEvent += Feedback("Video Clicked");
        ScaleMonkAds.InterstitialDisplayedEvent += Feedback("Interstitial Displayed");
        ScaleMonkAds.VideoDisplayedEvent += Feedback("Video Displayed");
        ScaleMonkAds.VideoStartedEvent += Feedback("Video Started");
        ScaleMonkAds.VideoNotDisplayedEvent += Feedback("Video Not Displayed");
        ScaleMonkAds.InterstitialNotDisplayedEvent += Feedback("Interstitial Not Displayed");
        ScaleMonkAds.InterstitialReadyEvent += Feedback("Interstitial Ready");
        ScaleMonkAds.VideoReadyEvent += Feedback("Rewarded Video Ready");
    }

    private Action Feedback(string start)
    {
        return () => LogField.text = start + " at " + tag;
    }

    // Update is called once per frame
    void Update()
    {
    }
}