package com.scalemonk.ads.unity.binding;

import android.util.Log;

import com.scalemonk.ads.BannerEventListener;
import com.unity3d.player.UnityPlayer;
import org.jetbrains.annotations.Nullable;

public class AdsBindingBannerEventListener implements BannerEventListener {
        @Override
        public void onBannerFail(@Nullable String tag) {
            Log.d(AdsBinding.TAG, "Failed baanner display at location " + tag);
            UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "FailedBannerDisplay", tag);
        }
        
        @Override
        public void onBannerCompleted(@Nullable String tag) {
            Log.i(AdsBinding.TAG, "Completed banner display at location " + tag);
            UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "CompletedBannerDisplay", tag);
        }
}