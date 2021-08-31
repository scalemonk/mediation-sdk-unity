package com.scalemonk.ads.unity.binding;

import android.app.Activity;
import android.content.Context;
import android.util.Log;

import com.scalemonk.ads.BannerContainer;
import com.scalemonk.ads.BannerEventListener;
import com.scalemonk.ads.InterstitialEventListener;
import com.scalemonk.ads.RewardedEventListener;
import com.scalemonk.ads.ScaleMonkAds;
import com.scalemonk.ads.ScaleMonkBanner;
import com.scalemonk.ads.unity.banner.BannerFactory;
import com.scalemonk.ads.GDPRConsent;
import com.scalemonk.libs.ads.core.domain.UserType;
import com.scalemonk.libs.ads.core.domain.regulations.CoppaStatus;
import com.scalemonk.libs.ads.core.domain.session.UserTypeProvider;
import com.unity3d.player.UnityPlayer;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.UUID;

import org.jetbrains.annotations.NotNull;
import androidx.annotation.Keep;

import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.FutureTask;
import java.util.concurrent.RunnableFuture;

@Keep
public class AdsBinding {
    public static String TAG = "AdsBinding";
    private final Activity activity;
    private final AdsBindingRewardedListener videoListener;
    private final AdsBindingInterstitialListener interstitialListener;
    private final AdsBindingBannerListener bannerListener;
    private ConcurrentHashMap<String, ScaleMonkBanner> banners = new ConcurrentHashMap<>();

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
        this.activity.runOnUiThread(task);

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
        this.activity.runOnUiThread(task);

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

    public String showBanner(final Context context,
                           final String position,
                           final String tag,
                           final int width,
                           final int height) {
        final String id = UUID.randomUUID().toString();
        this.activity.runOnUiThread(() -> {
                    ScaleMonkBanner banner = BannerFactory.createBanner(context, position, width, height);
                    banners.put(id, banner);
                    ScaleMonkAds.showBanner(context, banner, tag);
                }
        );
        return id;
    }

    public void stopBanner(final Context context, final String id) {
        this.activity.runOnUiThread(() -> {
            if (banners.containsKey(id)) {
                ScaleMonkBanner banner = banners.get(id);
                ScaleMonkAds.stopBanner(context, banner);
                BannerFactory.remove(context, banner);
                banners.remove(id);
            }
        });
    }
    
    public void stopBanner(final Context context) {
        for (String id : new ArrayList<String>(banners.keySet())) {
            stopBanner(context, id);
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

    /**
     * Changes the user's GDPR consent value.
     *
     * @deprecated Use ScaleMonkAds.setHasGDPRConsent(GdprConsent status) method instead
     * @param consent GDPR consent value provided by the user. The values are mapped like explained
     *                below:
     *                0 -> GDPRConsent.GRANTED
     *                1 -> GDPRConsent.NOT_GRANTED
     *                2 -> GDPRConsent.NOT_APPLICABLE
     *                3 -> GDPRConsent.NOT_SET
     */
    @Deprecated
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
    
    public void setCustomSegmentationTags(final String tags) {
        // Tags is a comma separated string
        HashSet<String> setOfSegmentationTags = new HashSet<>(Arrays.asList(tags.split(",")));
        ScaleMonkAds.updateCustomSegmentationTags(setOfSegmentationTags);
    }

    @Deprecated
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