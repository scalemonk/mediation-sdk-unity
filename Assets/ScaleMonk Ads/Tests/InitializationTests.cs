using System;
using NSubstitute;
using NUnit.Framework;

namespace ScaleMonk.Ads
{
    public class InitializationTests
    {
        [Test]
        public void SDKCanBeInitialized()
        {
            // Given an sdk instance
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();

            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());

            // When we call the initialization of the SDK
            scaleMonkAds.Initialize(() => { });
            
            // Then the SDK is initialized
            adsBinding.Received(1).Initialize(scaleMonkAds);
        }
        
        [Test]
        public void SDKCanBeInitializedOnlyOnce()
        {
            // Given an sdk instance
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();

            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());

            // When we call the initialization of the SDK two times
            scaleMonkAds.Initialize(() => { });
            scaleMonkAds.Initialize(() => { });

            // Then it's initialized just once
            adsBinding.Received(1).Initialize(scaleMonkAds);
        }

        [Test]
        public void InitializationCompletedEventIsCalledWhenSDKIsInitialized()
        {
            // Given an sdk instance
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var onInitialized = Substitute.For<Action>();
            
            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            scaleMonkAds.InitializationCompletedEvent += onInitialized;
            
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());

            // When we call the initialization of the SDK
            scaleMonkAds.Initialize(() => { });

            // Then InitializationCompletedEvent is received
            onInitialized.Received(1);


        }
    }
}