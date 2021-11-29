package com.scalemonk.ads.unity.binding;

import android.app.Activity;
import android.content.Context;
import androidx.annotation.Keep;

import com.scalemonk.libs.ads.core.domain.Analytics;
import com.tfg.libs.analytics.AnalyticsManager;
import com.unity3d.player.UnityPlayer;

import org.jetbrains.annotations.NotNull;

import java.util.HashMap;
import java.util.Map;

@Keep
public class AdsBindingAnalyticsListener implements Analytics {
    
    @Override
    public void sendEvent(@NotNull String eventName, @NotNull Map<String, ?> eventParams) {
        Map<String, String> map = new HashMap<>();
        for (Map.Entry<String, ?> entry : eventParams.entrySet()) {
            map.put(entry.getKey(), entry.getValue().toString());
        }

        AnalyticsManager.getInstance().sendEvent(eventName, map);
    }
}