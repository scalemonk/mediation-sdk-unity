using System.Linq;
using NSubstitute;
using NUnit.Framework;
using ScaleMonk.Ads.Android;

namespace ScaleMonk.Ads
{
    public class AndroidBindingTests
    {
        private IBridge _mockAndroidJavaBridge;
        private AdsAndroidBinding _androidBinding;
        
        private void GivenAnAdsAndroidBinding()
        {
            _mockAndroidJavaBridge = Substitute.For<IBridge>();
            _androidBinding = new AdsAndroidBinding(_mockAndroidJavaBridge);
        }
        
        [Test]
        public void ShowBannerAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();

            // When Show banner is called with tag "aTag"
            _androidBinding.ShowBanner("aTag", BannerSize.Full, BannerPosition.TopCenter);

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethodWithActivity(
                "showBanner",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "top_center" &&
                    parameters[1].ToString() == "aTag" &&
                    parameters[2].ToString() == "468" &&
                    parameters[3].ToString() == "60")
            );
        }

        [Test]
        public void StopBannerAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();

            // When StopBanner is call at "aTag"
            _androidBinding.StopBanner("aTag");

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethodWithActivity(
                "stopBanner",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "aTag" )
            );
        }
        
        [Test]
        public void ShowInterstitialAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();

            // When Show interstitial is called with tag "aTag"
            _androidBinding.ShowInterstitial("aTag");

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethodWithActivity(
                "showInterstitial",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "aTag" )
            );
        }
        
        [Test]
        public void ShowRewardedAdAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();

            // When Show rewarded is called with tag "aTag"
            _androidBinding.ShowRewarded("aTag");

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethodWithActivity(
                "showRewarded",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "aTag" )
            );
        }
        
        [Test]
        public void SetCustomUserIdAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();

            // When SetCustomUserId is called with tag "aTag"
            _androidBinding.SetCustomUserId("anUserId");

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethod(
                "setCustomUserId",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "anUserId")
            );
        }
        
        [Test]
        public void SetUserTypeAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();
            
            _androidBinding.SetUserType(UserType.PAYING_USER);

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethod(
                "setUserType",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "paying_user")
            );
        }
        
        [Test]
        public void SetUserTypeNonPayingUserAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();
            
            _androidBinding.SetUserType(UserType.NON_PAYING_USER);

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethod(
                "setUserType",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "non_paying_user")
            );
        }
        
        [Test]
        public void SetChildCoppaAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();

            // When SetIsApplicationChildDirected to "CoppaStatus.CHILD_TREATMENT_TRUE"
            _androidBinding.SetIsApplicationChildDirected(CoppaStatus.CHILD_TREATMENT_TRUE);
            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethod(
                "setIsApplicationChildDirected",
                Arg.Is<object[]>(parameters =>
                    (int)parameters[0] == 2)
            );
        }
        
        [Test]
        public void SetNonChildCoppaAndroidBridgeTest()
        {
            GivenAnAdsAndroidBinding();

            // When SetIsApplicationChildDirected to "CoppaStatus.CHILD_TREATMENT_TRUE"
            _androidBinding.SetIsApplicationChildDirected(CoppaStatus.CHILD_TREATMENT_FALSE);
            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethod(
                "setIsApplicationChildDirected",
                Arg.Is<object[]>(parameters =>
                    (int)parameters[0] == 1)
            );
        }
    }
}