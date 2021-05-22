package com.scalemonk.ads.unity.binding;

import android.app.Activity;
import android.content.Context;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonArray;
import com.google.gson.JsonObject;
import com.scalemonk.libs.ads.core.domain.Analytics;
import com.unity3d.player.UnityPlayer;

import org.jetbrains.annotations.NotNull;

import java.util.ArrayList;
import java.util.Map;

public class AdsBindingAnalyticsListener implements Analytics {
    private Gson gson;

    public AdsBindingAnalyticsListener() {
        gson = new GsonBuilder().create();
    }

    @Override
    public void sendEvent(@NotNull String eventName, @NotNull Map<String, ?> eventParams) {
        JsonObject eventAsJson = new JsonObject();
        ArrayList<String> eventKeys = new ArrayList<>();
        ArrayList<Object> eventValues = new ArrayList<>();

        for (Map.Entry<String, ?> entry : eventParams.entrySet()) {
            eventKeys.add(entry.getKey());
            eventValues.add(entry.getValue());
        }
        eventAsJson.addProperty("eventName", eventName);
        eventAsJson.add("eventKeys", gson.toJsonTree(eventKeys).getAsJsonArray());
        eventAsJson.add("eventValues", gson.toJsonTree(eventValues).getAsJsonArray());

        UnityPlayer.UnitySendMessage("AdsMonoBehaviour", "SendEvent", eventAsJson.toString());
    }
}