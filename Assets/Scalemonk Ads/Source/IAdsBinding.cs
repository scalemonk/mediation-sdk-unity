namespace ScaleMonk.Ads
{
    public interface IAdsBinding
    {
        void Initialize(ScaleMonkAds adsInstance);
        void ShowInterstitial(string tag);
        void ShowVideo(string tag);
        bool IsInterstitialReadyToShow(string analyticsLocation);
        bool IsRewardedVideoReadyToShow(string analyticsLocation);
        bool AreVideosEnabled();
        bool AreInterstitialsEnabled();
    }
}