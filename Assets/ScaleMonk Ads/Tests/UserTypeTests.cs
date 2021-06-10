using NSubstitute;
using NUnit.Framework;

namespace ScaleMonk.Ads
{
    public class UserTypeTests
    {
        [Test]
        public void SetUserTypeWhenAdsIsNotInitialized()
        {
            // Given an IAdsBinding not initialized
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);

            // When set custom user Id 
            scaleMonkAds.SetUserType(UserType.PAYING_USER);

            // Then the custom user Id is not set
            adsBinding.Received().SetUserType(UserType.PAYING_USER);
        }
        
        [Test]
        public void SetUserTypeNonPayingWhenAdsIsNotInitialized()
        {
            // Given an IAdsBinding not initialized
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);

            // When set custom user Id 
            scaleMonkAds.SetUserType(UserType.NON_PAYING_USER);

            // Then the custom user Id is not set
            adsBinding.Received().SetUserType(UserType.NON_PAYING_USER);
        }
        
        [Test]
        public void SetUserTypeWhenAdsIsInitializedCallsSetUserType()
        {
            // Given an IAdsBinding not initialized
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);

            scaleMonkAds.Initialize(() => { });
            
            // When set custom user Id 
            scaleMonkAds.SetUserType(UserType.PAYING_USER);

            // Then the custom user Id is not set
            adsBinding.Received().SetUserType(UserType.PAYING_USER);
        }
        
        [Test]
        public void SetUserTypeNonPayingWhenAdsIsInitializedCallsSetUserType()
        {
            // Given an IAdsBinding not initialized
            var adsBinding = Substitute.For<IAdsBinding>();
            var monoBehaviourService = Substitute.For<INativeBridgeService>();
            var analyticsService = new AnalyticsService();
            var scaleMonkAds = new ScaleMonkAdsSDK(adsBinding, monoBehaviourService, analyticsService);

            scaleMonkAds.Initialize(() => { });
            
            // When set custom user Id 
            scaleMonkAds.SetUserType(UserType.NON_PAYING_USER);

            // Then the custom user Id is not set
            adsBinding.Received().SetUserType(UserType.NON_PAYING_USER);
        }
    }
}