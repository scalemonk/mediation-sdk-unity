package com.scalemonk.ads.unity.binding;

import android.util.Log;
import androidx.annotation.Keep;

import com.scalemonk.ads.InterstitialEventListener;
import com.unity3d.player.UnityPlayer;
import org.jetbrains.annotations.Nullable;

@Keep
public class AdsBindingInterstitialListener implements InterstitialEventListener {
    @Override
    public void onInterstitialView(@Nullable String tag) {
        Log.d(AdsBinding.TAG, "Completed interstitial display at location " + tag);
        UnityPlayer.UnitySendMessage("AdsMonoBehaviour", "CompletedInterstitialDisplay", tag);
    }

    @Override
    public void onInterstitialClick(@Nullable String tag) {
        Log.d(AdsBinding.TAG, "Clicked interstitial at location " + tag);
        UnityPlayer.UnitySendMessage("AdsMonoBehaviour", "ClickedInterstitial", tag);
    }

    @Override
    public void onInterstitialFail(@Nullable String tag) {
        Log.d(AdsBinding.TAG, "Failed interstitial at location " + tag);
        UnityPlayer.UnitySendMessage("AdsMonoBehaviour", "FailedInterstitialDisplay", tag);
    }

    @Override
    public void onInterstitialReady() {
        Log.i(AdsBinding.TAG, "Interstitial Ready");
        UnityPlayer.UnitySendMessage("AdsMonoBehaviour", "InterstitialReady", "");

    }

    @Override
    public void onInterstitialViewStart(@Nullable String tag) {
        Log.i(AdsBinding.TAG, "Interstitial started view at location " + tag);
        // There is no method to notify interstitial start
    }
}
