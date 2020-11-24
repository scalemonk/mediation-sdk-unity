namespace ScaleMonk.Ads
{
    public class AdsEditorBinding : IAdsBinding
    {
        public void Initialize(ScaleMonkAds adsInstance)
        {
            throw new System.NotImplementedException();
        }

        public void ShowInterstitial(string tag)
        {
            throw new System.NotImplementedException();
        }

        public void ShowVideo(string tag)
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

        public bool AreVideosEnabled()
        {
            return true;
        }

        public bool AreInterstitialsEnabled()
        {
            return true;
        }
    }
}