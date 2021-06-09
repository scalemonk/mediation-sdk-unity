namespace ScaleMonk.Ads
{
    internal class DefaultNativeBridgeService : INativeBridgeService
    {
        public void Initialize(ScaleMonkAdsSDK scaleMonkAdsSDK)
        {
            ScaleMonkAdsMonoBehavior.Initialize(scaleMonkAdsSDK);
        }
    }
}