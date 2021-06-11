using ScaleMonk.Ads;
#if UNITY_IOS
using ScaleMonk.Ads.iOS;
#elif UNITY_ANDROID
using ScaleMonk.Ads.Android;
#endif

public static class AdsFactory
{
    public static IAdsBinding AdsBinding()
    {
        IAdsBinding binding = null;

#if UNITY_EDITOR
        binding = new AdsEditorBinding();
#elif UNITY_ANDROID
        binding = new AdsAndroidBinding(new AndroidJavaBridge());
#elif UNITY_IOS
            binding = new AdsiOSBinding();
#endif
        return binding;
    }

    public static INativeBridgeService NativeBridgeService()
    {
        return new DefaultNativeBridgeService();
    }

    public static AnalyticsService AnalyticsService()
    {
        return new AnalyticsService();
    }
}