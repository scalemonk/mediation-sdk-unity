using NSubstitute;
using NUnit.Framework;

namespace ScaleMonk.Ads
{
    public class GdprTests
    {
        [Test]
        public void ScalemonkSDKCallsInternalGdprMembersCorrectly()
        {
            // Given an initialized SDK
            var adsBinding = Substitute.For<IAdsBinding>();
            var nativeBridgeService = Substitute.For<INativeBridgeService>();
            var analyticsService = Substitute.For<AnalyticsService>();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, nativeBridgeService, analyticsService);

            const GdprConsent gdprConsent = GdprConsent.Granted;

            // When given a Gdpr value
            scaleMonkAds.SetHasGDPRConsent(gdprConsent);

            // Then it passes it to its Native bridge
            adsBinding.Received(1).SetHasGDPRConsent(gdprConsent);
        }
    }
}