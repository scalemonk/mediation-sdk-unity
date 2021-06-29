using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ScaleMonk.Ads
{
    public class AnalyticsTests
    {
        [Test]
        public void AnalyticsEventIsSentWhenNativeBridgeSendsAnEvent()
        {
            // Given an initialized SDK with an extra analytics attached to it
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsMock = Substitute.For<IAnalytics>();

            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());

            scaleMonkAds.Initialize(() =>
            {
                scaleMonkAds.AddAnalytics(analyticsMock);
            });

            // When an event is sent from the native bridge
            var customEvent = "{\"eventName\" : \"anEvent\", \"eventKeys\" : [\"aKey\"], \"eventValues\" : [\"aValue\"]}";
            analyticsService.SendEvent(customEvent);

            // Then analytics receives the expected Event 
            analyticsMock.Received(1).SendEvent("anEvent", ContainsEventParam("aKey", "aValue"));
        }

        [Test]
        public void AnalyticsCannotBeAddedIfSDKIsNotInitialized()
        {
            // Given a not initialized SDK
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsMock = Substitute.For<IAnalytics>();

            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            
            // When we add an analytics to it
            scaleMonkAds.AddAnalytics(analyticsMock);
            
            // Then the analytics binding is never created
            adsBinding.DidNotReceive().CreateAnalyticsBinding();
        }

        [Test]
        public void AnalyticsCanBeAddedToAInitializedSDK()
        {
            // Given an initialized SDK
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsMock = Substitute.For<IAnalytics>();

            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());

            scaleMonkAds.Initialize(() =>
            {
                // When an external analytics is added 
                scaleMonkAds.AddAnalytics(analyticsMock);
            });

            // Then analytics binding is created
            adsBinding.Received(1).CreateAnalyticsBinding();
        }
        
        [Test]
        public void AddingMoreThanOneExternalAnalyticsJustCreatesOneAnalyticsBinding()
        {
            // Given an initialized SDK
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsMock = Substitute.For<IAnalytics>();

            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());

            scaleMonkAds.Initialize(() =>
            {
                // When adding more than one analytics
                scaleMonkAds.AddAnalytics(analyticsMock);
                scaleMonkAds.AddAnalytics(analyticsMock);
            });

            // Then just one analytics binding is created
            adsBinding.Received(1).CreateAnalyticsBinding();
        }

        private static Dictionary<string, string> ContainsEventParam(string key, string value)
        {
            return Arg.Is<Dictionary<string, string>>(parameters =>
                parameters.ContainsKey(key) && parameters[key] == value);
        }
    }
}