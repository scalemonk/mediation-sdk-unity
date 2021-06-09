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
                // Given an external analytics added 
                scaleMonkAds.AddAnalytics(analyticsMock);
            });

            // When an event is sent form the native bridge
            var customEvent = "{\"eventName\" : \"anEvent\", \"eventKeys\" : [\"aKey\"], \"eventValues\" : [\"aValue\"]}";
            analyticsService.SendEvent(customEvent);

            // Then analytics receives the expected Event 
            analyticsMock.Received(1).SendEvent("anEvent", ContainsEventParam("aKey", "aValue"));
        }

        private static Dictionary<string, string> ContainsEventParam(string key, string value)
        {
            return Arg.Is<Dictionary<string, string>>(parameters =>
                parameters.ContainsKey(key) && parameters[key] == value);
        }
    }
}