package com.scalemonk.ads.unity.binding;

import android.util.Log;
import androidx.annotation.Keep;

import com.scalemonk.ads.BannerEventListener;
import com.unity3d.player.UnityPlayer;
import org.jetbrains.annotations.Nullable;

@Keep
public class AdsBindingBannerListener implements BannerEventListener {
        @Override
        public void onBannerFail(@Nullable String tag) {
            Log.d(AdsBinding.TAG, "Failed banner display at location " + tag);
            UnityPlayer.UnitySendMessage("AdsMonoBehaviour", "FailedBannerDisplay", tag);
        }
        
        @Override
        public void onBannerCompleted(@Nullable String tag) {
            Log.i(AdsBinding.TAG, "Completed banner display at location " + tag);
            UnityPlayer.UnitySendMessage("AdsMonoBehaviour", "CompletedBannerDisplay", tag);
        }
}