package com.scalemonk.ads.unity.banner;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.util.TypedValue;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.RelativeLayout;

import com.scalemonk.ads.Banner;

import java.util.ArrayList;
import java.util.List;

public class BannerFactory {
    // we need this value to be sure that the container is big enough to place our Ad.
    private static final int OFFSET_IN_DP = 1;

    public static Banner createBanner(Context context, String position, int width, int height) {
        Activity activity = (Activity) context;
        FrameLayout rootLayout = activity.findViewById(android.R.id.content);

        // This relative layout will be used as a parent canvas where the banner can be positioned
        // in different places of the screen.
        RelativeLayout fullscreenLayout = new RelativeLayout(context);
        RelativeLayout.LayoutParams fullscreenParams =
                new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT,
                        ViewGroup.LayoutParams.MATCH_PARENT);
        fullscreenLayout.setLayoutParams(fullscreenParams);

        Banner banner = new Banner(context);
        banner.setPadding(0,0,0,0);

        RelativeLayout.LayoutParams bannerParams =
                new RelativeLayout.LayoutParams(dpToPx(activity, width + OFFSET_IN_DP), dpToPx(activity, height + OFFSET_IN_DP));

        bannerParams.setMargins(0, 0, 0, 0);
        for (int rule : layoutParamsFrom(position)) {
            bannerParams.addRule(rule);
        }

        banner.setLayoutParams(bannerParams);

        fullscreenLayout.addView(banner);
        rootLayout.addView(fullscreenLayout);

        return banner;
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
        return (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, dp, activity.getResources().getDisplayMetrics());
    }
}
