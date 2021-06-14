using NSubstitute;
using NUnit.Framework;

namespace ScaleMonk.Ads
{
    public class CustomUserIdTest
    {
        [Test]
        public void SetCustomUserIdWhenAdsIsNotInitialized()
        {
            // Given an IAdsBinding not initialized
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);

            // When set custom user Id 
            scaleMonkAds.SetCustomUserId("userId");

            // Then the custom user Id is not set
            adsBinding.Received(1).SetCustomUserId(Arg.Any<string>());
        }

        [Test]
        public void SetCustomUserIdWhenAdsIsInitializedSetsTheRightUserId()
        {
            // Given an IAdsBinding not initialized
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();

            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());

            scaleMonkAds.Initialize(() =>
            {
                // When set custom user Id 
                scaleMonkAds.SetCustomUserId("userId");
            });

            // Then the custom user Id is not set
            adsBinding.Received(1).SetCustomUserId("userId");
        }
    }
}