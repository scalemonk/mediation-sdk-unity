package com.scalemonk.ads.unity.banner;

import android.app.Activity;
import android.content.Context;
import android.widget.FrameLayout;

import com.scalemonk.ads.BannerContainer;

public class BannerContainerFactory {
    public static BannerContainer createBannerContainerFrom(Context context, String position) {
        BannerContainer bannerContainer = new BannerContainer(context);
        Activity activity = (Activity) context;
        FrameLayout rootLayout = activity.findViewById(android.R.id.content);
        rootLayout.addView(bannerContainer);

        return bannerContainer;
    }
}