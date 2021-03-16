package com.scalemonk.ads.unity.binding;

import android.app.Activity;
import android.content.Context;
import android.util.Log;
import com.scalemonk.ads.InterstitialEventListener;
import com.scalemonk.ads.RewardedEventListener;
import com.scalemonk.ads.ScaleMonkAds;

public class AdsBinding {
    public static String TAG = "AdsBinding";
    private final Activity activity;
    private final AdsBindingRewardedListener videoListener;
    private final AdsBindingInterstitialListener interstitialListener;

    public AdsBinding(final Activity activity) {
        this.activity = activity;
        this.videoListener = new AdsBindingRewardedListener();
        this.interstitialListener = new AdsBindingInterstitialListener();

        this.activity.runOnUiThread(() -> {
            setupAds(interstitialListener, videoListener);
        });
    }

    public boolean isInterstitialReadyToShow(String tag) {
        Log.d(TAG, "Checking availability of interstitials at: " + tag + ".");
        return ScalemonkAds.isInterstitialReadyToShow(tag);
    }

    public boolean isRewardedReadyToShow(String tag) {
        if (getAdsController() == null) return false;
        Log.d(TAG, "Checking availability of videos on: " + tag + ".");
        return ScalemonkAds.isRewardedReadyToShow(tag);
    }

    public boolean areVideosEnabled() {
        return ScalemonkAds.areVideosEnabled();
    }

    public boolean areInterstitialsEnabled() {
        return ScalemonkAds.areInterstitialsEnabled();
    }

    public void showInterstitial(final Context context, final String tag) {
        this.activity.runOnUiThread(() -> Scalemonk.showInterstitial(context, tag));
    }

    public void showRewarded(final Context context, final String tag) {
        this.activity.runOnUiThread(() -> adsController.showRewarded(context, tag));
    }

    private void setupAds(
            InterstitialEventListener interstitialListener,
            RewardedEventListener rewardedListener
    ) {
        ScaleMonkAds.initialize(context, Runnable {
                    this.bannerListener?.let { ScaleMonkAds.addBannerListener(it) }
                    this.interstitialListener?.let { ScaleMonkAds.addInterstitialListener(it) }
                    this.rewardedListener?.let { ScaleMonkAds.addRewardedListener(it) }
                    Log.i(TAG, "Ads SDK Initialized")
        })
    }
    
    public void SetHasGDPRConsent(final boolean consent) {
        ScaleMonkAds.setHasGDPRConsent(consent);
    }
    
    public void setIsApplicationChildDirected(final boolean isChildDirected) {
        ScaleMonkAds.isChildDirected(isChildDirected);
    }
    
    public void setUserCantGiveGDPRConsent(final boolean cantGiveConsent) {
        ScaleMonkAds.setUserCantGiveGDPRConsent(cantGiveConsent);
    }
}
