namespace ScaleMonk.Ads
{
    public interface IAdsBinding
    {
        void Initialize(ScaleMonkAds adsInstance);
        void ShowInterstitial(string tag);
        void ShowRewarded(string tag);
        bool IsInterstitialReadyToShow(string analyticsLocation);
        bool IsRewardedVideoReadyToShow(string analyticsLocation);
        bool AreRewardedEnabled();
        bool AreInterstitialsEnabled();
        void TagGDPRConsent(bool consent);
        void TagUserAge(bool isUnderage);
    }
}