package com.scalemonk.ads.unity.binding;

import android.app.Activity;
import android.content.Context;
import android.util.Log;
import androidx.annotation.Keep;

import com.scalemonk.ads.BannerContainer;
import com.scalemonk.ads.InterstitialEventListener;
import com.scalemonk.ads.RewardedEventListener;
import com.scalemonk.ads.BannerEventListener;
import com.scalemonk.ads.ScaleMonkAds;
import com.scalemonk.ads.unity.banner.BannerContainerFactory;
import com.unity3d.player.UnityPlayer;

@Keep
public class AdsBinding {
    public static String TAG = "AdsBinding";
    private final Activity activity;
    private final AdsBindingRewardedListener videoListener;
    private final AdsBindingInterstitialListener interstitialListener;
    private final AdsBindingBannerListener bannerListener;
    private AdsBindingAnalyticsListener analyticsListener;
    private BannerContainer currentBannerContainer;

    public AdsBinding(final Activity activity) {
        this.activity = activity;
        this.videoListener = new AdsBindingRewardedListener();
        this.interstitialListener = new AdsBindingInterstitialListener();
        this.bannerListener = new AdsBindingBannerListener();

        this.activity.runOnUiThread(() -> {
            setupAds(interstitialListener, videoListener, bannerListener);
        });
    }

    public boolean isInterstitialReadyToShow(String tag) {
        Log.d(TAG, "Checking availability of interstitials at: " + tag + ".");
        return ScaleMonkAds.isInterstitialReadyToShow(tag);
    }

    public boolean isRewardedReadyToShow(String tag) {
        Log.d(TAG, "Checking availability of videos on: " + tag + ".");
        return ScaleMonkAds.isRewardedReadyToShow(tag);
    }

//    public boolean areVideosEnabled() {
//        return ScaleMonkAds.areVideosEnabled();
//    }

//    public boolean areInterstitialsEnabled() {
//        return ScaleMonkAds.areInterstitialsEnabled();
//    }

    public void showInterstitial(final Context context, final String tag) {
        this.activity.runOnUiThread(() -> ScaleMonkAds.showInterstitial(context, tag));
    }

    public void showRewarded(final Context context, final String tag) {
        this.activity.runOnUiThread(() -> ScaleMonkAds.showRewarded(context, tag));
    }
    
    public void showBanner(final Context context, 
                            final String position, 
                            final String tag, 
                            final int width,
                            final int height) {
        
        this.activity.runOnUiThread(() -> {
                    currentBannerContainer = BannerContainerFactory.createBannerContainer(context, position, width, height);
                    ScaleMonkAds.showBanner(context, currentBannerContainer, tag);
                }
        );
    }
    
    public void stopBanner(final Context context, final String tag) {
        if (currentBannerContainer != null) {
            this.activity.runOnUiThread(() -> ScaleMonkAds.stopBanner(context, currentBannerContainer, tag));
        }
    }

    private void setupAds(
            InterstitialEventListener interstitialListener,
            RewardedEventListener rewardedListener,
            BannerEventListener bannerListener
    ) {
        ScaleMonkAds.initialize(activity, () -> {
                    ScaleMonkAds.addInterstitialListener(interstitialListener);
                    ScaleMonkAds.addRewardedListener(rewardedListener);
                    ScaleMonkAds.addBannerListener(bannerListener);
                    
                    Log.i(TAG, "Ads SDK Initialized");
                    
                    UnityPlayer.UnitySendMessage("AdsMonoBehaviour", "InitializationCompleted", "");
        });
    }
    
    public void setHasGDPRConsent(final boolean consent) {
        ScaleMonkAds.setHasGDPRConsent(consent);
    }
    
    public void setIsApplicationChildDirected(final boolean isChildDirected) {
        ScaleMonkAds.setIsApplicationChildDirected(isChildDirected);
    }
    
    public void setUserCantGiveGDPRConsent(final boolean cantGiveConsent) {
        ScaleMonkAds.setUserCantGiveGDPRConsent(cantGiveConsent);
    }
    
    public void addAnalytics() {
        analyticsListener = new AdsBindingAnalyticsListener();
        ScaleMonkAds.addAnalytics(analyticsListener);
    }
    
    public void setCustomUserId(final String customUserId) {
        ScaleMonkAds.updateCustomUserId(customUserId);
    }
}