package com.scalemonk.ads.unity.banner;

import android.app.Activity;
import android.content.Context;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.RelativeLayout;

import com.scalemonk.ads.BannerContainer;

import java.util.ArrayList;
import java.util.List;

public class BannerContainerFactory {
    public static final int containerWidthInDp = 400;
    public static final int containerHeightInDp = 100;

    public static BannerContainer createBannerContainer(Context context, String position) {
        Activity activity = (Activity) context;
        FrameLayout rootLayout = activity.findViewById(android.R.id.content);

        // This relative layout will be used as a parent canvas where the banner can be positioned
        // in different places of the screen.
        RelativeLayout fullscreenLayout = new RelativeLayout(context);
        RelativeLayout.LayoutParams fullscreenParams =
                new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT,
                        ViewGroup.LayoutParams.MATCH_PARENT);
        fullscreenLayout.setLayoutParams(fullscreenParams);

        BannerContainer bannerContainer = new BannerContainer(context);
        RelativeLayout.LayoutParams bannerContainerParams =
                new RelativeLayout.LayoutParams(dpToPx(activity, containerWidthInDp), dpToPx(activity, containerHeightInDp));

        for (int rule : layoutParamsFrom(position)) {
            bannerContainerParams.addRule(rule);
        }

        bannerContainer.setLayoutParams(bannerContainerParams);

        fullscreenLayout.addView(bannerContainer);
        rootLayout.addView(fullscreenLayout);

        return bannerContainer;
    }

    private static List<Integer> layoutParamsFrom(String position) {
        List<Integer> rules = new ArrayList<>();

        switch (position) {
            case "bottom_center":
                rules.add(RelativeLayout.ALIGN_PARENT_BOTTOM);
                rules.add(RelativeLayout.CENTER_HORIZONTAL);
                break;
            case "bottom_left":
                rules.add(RelativeLayout.ALIGN_PARENT_BOTTOM);
                rules.add(RelativeLayout.ALIGN_LEFT);
                break;
            case "bottom_right":
                rules.add(RelativeLayout.ALIGN_PARENT_BOTTOM);
                rules.add(RelativeLayout.ALIGN_RIGHT);
                break;
            case "centered":
                rules.add(RelativeLayout.CENTER_IN_PARENT);
                rules.add(RelativeLayout.CENTER_HORIZONTAL);
                break;
            case "center_left":
                rules.add(RelativeLayout.CENTER_IN_PARENT);
                rules.add(RelativeLayout.ALIGN_LEFT);
                break;
            case "center_right":
                rules.add(RelativeLayout.CENTER_IN_PARENT);
                rules.add(RelativeLayout.ALIGN_RIGHT);
                break;
            case "top_center":
                rules.add(RelativeLayout.ALIGN_PARENT_TOP);
                rules.add(RelativeLayout.CENTER_HORIZONTAL);
                break;
            case "top_left":
                rules.add(RelativeLayout.ALIGN_PARENT_TOP);
                rules.add(RelativeLayout.ALIGN_LEFT);
                break;
            case "top_right":
                rules.add(RelativeLayout.ALIGN_PARENT_TOP);
                rules.add(RelativeLayout.ALIGN_RIGHT);
                break;
            default:
                System.out.println("Invalid position");
                break;
        }

        return rules;
    }

    private static int dpToPx(Activity activity, int dp) {
        float density = activity.getResources()
                .getDisplayMetrics()
                .density;
        return Math.round((float) dp * density);
    }
}