namespace ScaleMonk.Ads.Android
{
    public class AdsAndroidBinding : IAdsBinding
    {
        public void Initialize(ScaleMonkAds adsInstance)
        {
            // TODO: Not yet implemented
        }

        public void ShowInterstitial(string tag)
        {
            // TODO: Not yet implemented
        }

        public void ShowRewarded(string tag)
        {
            // TODO: Not yet implemented
        }

        public bool IsInterstitialReadyToShow(string analyticsLocation)
        {
            // TODO: Not yet implemented
            return false;
        }

        public bool IsRewardedVideoReadyToShow(string analyticsLocation)
        {
            // TODO: Not yet implemented
            return false;
        }

        public bool AreRewardedEnabled()
        {
            // TODO: Not yet implemented
            return false;
        }

        public bool AreInterstitialsEnabled()
        {
            // TODO: Not yet implemented
            return false;
        }

        public void SetHasGDPRConsent(bool consent)
        {
            // TODO: Not yet implemented

        }

        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
            // TODO: Not yet implemented

        }

        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            // TODO: Not yet implemented
        }
    }
}