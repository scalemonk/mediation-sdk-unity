using NSubstitute;
using NUnit.Framework;

namespace ScaleMonk.Ads
{
    public class ScaleMonkAdsSDKTests
    {
        [Test]
        public void SdkCallsBindingSetIsApplicationChildDirectedWhenItsNotInitialized()
        {
            // Given an sdk instance
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsService = Substitute.For<AnalyticsService>();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            
            // When sdk is not initialized and SetIsApplicationChildDirected is called
            Assert.False(scaleMonkAds.IsInitialized());
            scaleMonkAds.SetIsApplicationChildDirected(CoppaStatus.ChildTreatmentTrue);
            
            // Then adBidding SetIsApplicationChildDirected is called
            adsBinding.Received(1).SetIsApplicationChildDirected(CoppaStatus.ChildTreatmentTrue);
        }
        
        [Test]
        public void SdkCallsBindingSetIsApplicationChildDirectedWhenItsInitialized()
        {
            // Given an sdk instance
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsService = Substitute.For<AnalyticsService>();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());
            
            scaleMonkAds.Initialize(() => {
                // When sdk is initialized and SetIsApplicationChildDirected is called
                Assert.True(scaleMonkAds.IsInitialized());
                scaleMonkAds.SetIsApplicationChildDirected(CoppaStatus.ChildTreatmentTrue);
            
                // Then adBidding SetIsApplicationChildDirected is called
                adsBinding.Received(1).SetIsApplicationChildDirected(CoppaStatus.ChildTreatmentTrue);
            });
        }
    }
}
