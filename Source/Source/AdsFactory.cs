using ScaleMonk.Ads;

#if UNITY_IOS
using ScaleMonk.Ads.iOS;
#elif UNITY_ANDROID
using ScaleMonk.Ads.Android;

#endif

public static class AdsFactory
{
    private static AnalyticsService _analyticsService;
    private static DefaultNativeBridgeService _defaultNativeBridgeService;
    private static IAdsBinding _binding;

    public static IAdsBinding AdsBinding()
    {
        if (_binding == null)
        {
#if UNITY_EDITOR
            _binding = new AdsEditorBinding();
#elif UNITY_ANDROID
            _binding = new AdsAndroidBinding(new AndroidJavaBridge());
#elif UNITY_IOS
            _binding = new AdsiOSBinding();
#endif
        }
        return _binding;
    }

    public static INativeBridgeService NativeBridgeService()
    {
        return _defaultNativeBridgeService ?? (_defaultNativeBridgeService = new DefaultNativeBridgeService());
    }

    public static AnalyticsService AnalyticsService()
    {
        return _analyticsService ?? (_analyticsService = new AnalyticsService());
    }
}