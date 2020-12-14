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

        public void SetHasGDPRConsent(bool consent)
        {
        }

        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
        }

        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            
        }
    }
}