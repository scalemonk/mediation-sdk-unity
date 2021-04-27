package com.scalemonk.ads.unity.binding;

import android.util.Log;

import com.scalemonk.ads.RewardedEventListener;
import com.unity3d.player.UnityPlayer;
import org.jetbrains.annotations.Nullable;

public class AdsBindingRewardedListener implements RewardedEventListener {
    @Override
    public void onRewardedClick(@Nullable String tag) {
        Log.d(AdsBinding.TAG, "Clicked rewarded display at location " + tag);
        UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "ClickedRewarded", tag);
    }

    @Override
    public void onRewardedFail(@Nullable String tag) {
        Log.d(AdsBinding.TAG, "Failed rewarded display at location " + tag);
        UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "FailedRewardedDisplay", tag);
    }

    @Override
    public void onRewardedFinishWithNoReward(@Nullable String tag) {
        Log.i(AdsBinding.TAG, "Completed rewarded display with NO reward at location " + tag);
        UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "FailedRewardedDisplay", tag);
    }

    @Override
    public void onRewardedFinishWithReward(@Nullable String tag) {
        Log.d(AdsBinding.TAG, "Completed rewarded display at location " + tag);
        UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "CompletedRewardedDisplay", tag);
    }

    @Override
    public void onRewardedReady() {
        Log.i(AdsBinding.TAG, "Rewarded Ready");
        UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "RewardedReady", "");
    }

    @Override
    public void onRewardedViewStart(@Nullable String tag) {
        Log.i(AdsBinding.TAG, "Rewarded started view at location " + tag);
        UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "StartedRewardedDisplay", tag);
    }

    @Override
    public void onRewardedFailedToLoad() {
        Log.i(AdsBinding.TAG, "Rewarded failed to load");
        UnityPlayer.UnitySendMessage("ScaleMonkAdsMonoBehavior", "RewardedNotReady", "");
    }
}
