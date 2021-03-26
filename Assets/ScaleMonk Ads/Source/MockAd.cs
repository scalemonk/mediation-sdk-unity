using System.Collections;
using System.Collections.Generic;
using ScaleMonk.Ads;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MockAd : MonoBehaviour
{
    public Button closeButton;
    public Text title;
    public Mode mockAdMode;
    public string mockAdTag;
    private static string TITLE_TEXT = "AD WILL BE DISPLAYED HERE";

    public enum Mode
    {
        Interstitial,
        Rewarded
    }

    private ScaleMonkAds _scaleMonkAds;

    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(closeAd);
    }

    private void closeAd()
    {
        if (mockAdMode == Mode.Interstitial)
        {
            _scaleMonkAds.CompletedInterstitialDisplay(mockAdTag);
        }
        else
        {
            _scaleMonkAds.CompletedRewardedDisplay(mockAdTag);
        }

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            closeAd();
        }
    }

    public void SetMode(Mode mode)
    {
        mockAdMode = mode;
        if (mode == Mode.Interstitial)
        {
            title.text = "AN INTERSTITIAL " + TITLE_TEXT;
        }
        else
        {
            title.text = "A REWARDED " + TITLE_TEXT;
        }
    }

    public void SetTag(string tag)
    {
        mockAdTag = tag;
    }

    public void SetScalemonkAds(ScaleMonkAds adsInstance)
    {
        _scaleMonkAds = adsInstance;
    }
}