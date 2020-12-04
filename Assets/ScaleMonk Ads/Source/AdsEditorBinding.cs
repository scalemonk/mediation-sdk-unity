namespace ScaleMonk.Ads
{
    public class AdsEditorBinding : IAdsBinding
    {
        public void Initialize(ScaleMonkAds adsInstance)
        {
        }

        public void ShowInterstitial(string tag)
        {
        }

        public void ShowRewarded(string tag)
        {
        }

        public bool IsInterstitialReadyToShow(string analyticsLocation)
        {
            return true;
        }

        public bool IsRewardedVideoReadyToShow(string analyticsLocation)
        {
            return true;
        }

        public bool AreRewardedEnabled()
        {
            return true;
        }

        public bool AreInterstitialsEnabled()
        {
            return true;
        }

        public void TagGDPRConsent(bool consent)
        {
        }

        public void TagUserAge(bool isUnderage)
        {
        }
    }
}