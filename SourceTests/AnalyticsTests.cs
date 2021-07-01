using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ScaleMonk.Ads
{
    public class AnalyticsTests
    {
        [Test]
        public void AnalyticsIsAddedBeforeInitializationAndEventIsSentWhenNativeBridgeSendsAnEvent()
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
            
            scaleMonkAds.AddAnalytics(analyticsMock);
            
            scaleMonkAds.Initialize(() =>
            {
                
            });

            // When an event is sent from the native bridge
            var customEvent = "{\"eventName\" : \"anEvent\", \"eventKeys\" : [\"aKey\"], \"eventValues\" : [\"aValue\"]}";
            analyticsService.SendEvent(customEvent);

            // Then analytics receives the expected Event 
            analyticsMock.Received(1).SendEvent("anEvent", ContainsEventParam("aKey", "aValue"));
        }
        
        [Test]
        public void AnalyticsIsAddedAfterInitializationAndEventIsSentWhenNativeBridgeSendsAnEvent()
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
        public void AddTwoAnalyticsDispachEventsToBothOfThem()
        {
            // Given an initialized SDK with an extra analytics attached to it
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsMock = Substitute.For<IAnalytics>();
            var analyticsMock2 = Substitute.For<IAnalytics>();

            AnalyticsService analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);
            adsBinding
                .When(binding => binding.Initialize(scaleMonkAds))
                .Do(binding => scaleMonkAds.InitializationCompleted());

            scaleMonkAds.Initialize(() =>
            {
                scaleMonkAds.AddAnalytics(analyticsMock);
                scaleMonkAds.AddAnalytics(analyticsMock2);
            });

            // When an event is sent from the native bridge
            var customEvent = "{\"eventName\" : \"anEvent\", \"eventKeys\" : [\"aKey\"], \"eventValues\" : [\"aValue\"]}";
            analyticsService.SendEvent(customEvent);

            // Then analytics receives the expected Event 
            analyticsMock.Received(1).SendEvent("anEvent", ContainsEventParam("aKey", "aValue"));
            analyticsMock2.Received(1).SendEvent("anEvent", ContainsEventParam("aKey", "aValue"));
        }
        

        private static Dictionary<string, string> ContainsEventParam(string key, string value)
        {
            return Arg.Is<Dictionary<string, string>>(parameters =>
                parameters.ContainsKey(key) && parameters[key] == value);
        }
    }
}