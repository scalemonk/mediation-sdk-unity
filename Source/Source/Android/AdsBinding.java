package com.scalemonk.ads.unity.binding;

import android.app.Activity;
import android.content.Context;
import android.util.Log;

import com.scalemonk.ads.BannerContainer;
import com.scalemonk.ads.BannerEventListener;
import com.scalemonk.ads.InterstitialEventListener;
import com.scalemonk.ads.RewardedEventListener;
import com.scalemonk.ads.ScaleMonkAds;
import com.scalemonk.ads.GDPRConsent;
import com.scalemonk.ads.unity.banner.BannerContainerFactory;
import com.scalemonk.libs.ads.core.domain.UserType;
import com.scalemonk.libs.ads.core.domain.regulations.CoppaStatus;
import com.scalemonk.libs.ads.core.domain.session.UserTypeProvider;
import com.unity3d.player.UnityPlayer;

import org.jetbrains.annotations.NotNull;
import androidx.annotation.Keep;

import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.FutureTask;
import java.util.concurrent.RunnableFuture;

import static com.unity3d.services.core.misc.Utilities.runOnUiThread;

@Keep
public class AdsBinding {
    public static String TAG = "AdsBinding";
    private final Activity activity;
    private final AdsBindingRewardedListener videoListener;
    private final AdsBindingInterstitialListener interstitialListener;
    private final AdsBindingBannerListener bannerListener;
    private ConcurrentHashMap<String, BannerContainer> banners = new ConcurrentHashMap<>();

    public AdsBinding(final Activity activity) {
        this.activity = activity;
        this.videoListener = new AdsBindingRewardedListener();
        this.interstitialListener = new AdsBindingInterstitialListener();
        this.bannerListener = new AdsBindingBannerListener();
        ScaleMonkAds.addAnalytics(new AdsBindingAnalyticsListener());
    }

    public void initialize() {
        this.activity.runOnUiThread(() -> {
            setupAds(interstitialListener, videoListener, bannerListener);
        });
    }

    public boolean isInterstitialReadyToShow(String tag) {
        Log.d(TAG, "Checking availability of interstitials at: " + tag + ".");

        boolean[] result = new boolean[1];

        RunnableFuture<Void> task = new FutureTask<>(() -> result[0] = ScaleMonkAds.isInterstitialReadyToShow(tag), null);
        runOnUiThread(task);

        try {
            task.get(); // this will block until Runnable completes
        } catch (Exception e) {
            result[0] = false;
        }

        return result[0];
    }

    public boolean isRewardedReadyToShow(String tag) {
        Log.d(TAG, "Checking availability of videos on: " + tag + ".");

        boolean[] result = new boolean[1];

        RunnableFuture<Void> task = new FutureTask<>(() -> result[0] = ScaleMonkAds.isRewardedReadyToShow(tag), null);
        runOnUiThread(task);

        try {
            task.get(); // this will block until Runnable completes
        } catch (Exception e) {
            result[0] = false;
        }

        return result[0];
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
                    // Do not show two banners on the same tag.
                    if (banners.containsKey(tag)) return;

                    BannerContainer currentBannerContainer = BannerContainerFactory.createBannerContainer(context, position, width, height);
                    banners.put(tag, currentBannerContainer);

                    ScaleMonkAds.showBanner(context, currentBannerContainer, tag);
                }
        );
    }

    public void stopBanner(final Context context, final String tag) {
        this.activity.runOnUiThread(() -> {
            if (banners.containsKey(tag)) {
                BannerContainer currentBannerContainer = banners.get(tag);
                ScaleMonkAds.stopBanner(context, currentBannerContainer, tag);
                BannerContainerFactory.remove(context, currentBannerContainer);
                banners.remove(tag);
            }
        });
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

    [Obsolete("Use \"void SetHasGDPRConsent(GdprConsent status)\" method instead.")]
    public void setHasGDPRConsent(final boolean consent) {
        ScaleMonkAds.setHasGDPRConsent(consent);
    }
    
    public void setHasGDPRConsent(final int consent) {
        switch (consent) {
            case 0:
                ScaleMonkAds.setHasGDPRConsent(GDPRConsent.GRANTED);
                break;
            case 1:
                ScaleMonkAds.setHasGDPRConsent(GDPRConsent.NOT_GRANTED);
                break;
            case 2:
                ScaleMonkAds.setHasGDPRConsent(GDPRConsent.NOT_APPLICABLE);
                break;
            default:
                ScaleMonkAds.setHasGDPRConsent(GDPRConsent.NOT_SET);
                break;
        }
    }

    public void setIsApplicationChildDirected(final boolean isChildDirected) {
        ScaleMonkAds.setIsApplicationChildDirected(isChildDirected);
    }

    public void setIsApplicationChildDirected(final int consent) {
        switch (consent) {
            case 0:
                ScaleMonkAds.setIsApplicationChildDirected(CoppaStatus.UNKNOWN);
                break;
            case 1:
                ScaleMonkAds.setIsApplicationChildDirected(CoppaStatus.CHILD_TREATMENT_FALSE);
                break;
            case 2:
                ScaleMonkAds.setIsApplicationChildDirected(CoppaStatus.CHILD_TREATMENT_TRUE);
                break;
            case 3:
                ScaleMonkAds.setIsApplicationChildDirected(CoppaStatus.NOT_APPLICABLE);
                break;
        }
    }

    public void setUserCantGiveGDPRConsent(final boolean cantGiveConsent) {
        ScaleMonkAds.setUserCantGiveGDPRConsent(cantGiveConsent);
    }

    public void setCustomUserId(final String customUserId) {
        ScaleMonkAds.updateCustomUserId(customUserId);
    }

    public void setUserType(final String userType) {
        ScaleMonkAds.updateUserTypeProvider(new UnityUserTypeProvider(getUserTypeFromString(userType)));
    }

    private UserType getUserTypeFromString(String userTypeAsString) {
        switch (userTypeAsString) {
            case "paying_user":
                return UserType.PAYING_USER_USER_TYPE;
            case "non_paying_user":
                return UserType.NON_PAYING_USER_USER_TYPE;
            default:
                return UserType.INVALID_USER_TYPE;
        }
    }
}

class UnityUserTypeProvider implements UserTypeProvider {
    protected UserType userType = UserType.NON_PAYING_USER_USER_TYPE;

    public UnityUserTypeProvider(UserType userType) {
        this.userType = userType;
    }

    @NotNull
    @Override
    public UserType get() {
        return userType;
    }
}