using System;
using System.Collections.Generic;
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

        [SetUp]
        public void GivenAnAdsAndroidBinding()
        {
            _mockAndroidJavaBridge = Substitute.For<IBridge>();
            _androidBinding = new AdsAndroidBinding(_mockAndroidJavaBridge);
        }

        [Test]
        public void ShowBannerAndroidBridgeTest()
        {
            // When Show banner is called with tag "aTag"
            _androidBinding.ShowBanner("aTag", BannerSize.Full, BannerPosition.TopCenter);

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallStringNativeMethodWithActivity(
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
            // When StopBanner is call at "aBannerId"
            _androidBinding.StopBanner("aBannerId");

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethodWithActivity(
                "stopBanner",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "aBannerId")
            );
        }

        [Test]
        public void ShowInterstitialAndroidBridgeTest()
        {
            // When Show interstitial is called with tag "aTag"
            _androidBinding.ShowInterstitial("aTag");

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethodWithActivity(
                "showInterstitial",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "aTag")
            );
        }

        [Test]
        public void ShowRewardedAdAndroidBridgeTest()
        {
            // When Show rewarded is called with tag "aTag"
            _androidBinding.ShowRewarded("aTag");

            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethodWithActivity(
                "showRewarded",
                Arg.Is<object[]>(parameters =>
                    parameters[0].ToString() == "aTag")
            );
        }

        [Test]
        public void SetCustomUserIdAndroidBridgeTest()
        {
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
            // When SetIsApplicationChildDirected to "CoppaStatus.CHILD_TREATMENT_TRUE"
            _androidBinding.SetIsApplicationChildDirected(CoppaStatus.ChildTreatmentTrue);
            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethod(
                "setIsApplicationChildDirected",
                Arg.Is<object[]>(parameters =>
                    (int) parameters[0] == 2)
            );
        }

        [Test]
        public void SetNonChildCoppaAndroidBridgeTest()
        {
            // When SetIsApplicationChildDirected to "CoppaStatus.CHILD_TREATMENT_TRUE"
            _androidBinding.SetIsApplicationChildDirected(CoppaStatus.ChildTreatmentFalse);
            // Then the java bridge is called with the right parameters
            _mockAndroidJavaBridge.Received(1).CallNativeMethod(
                "setIsApplicationChildDirected",
                Arg.Is<object[]>(parameters =>
                    (int) parameters[0] == 1)
            );
        }

        [Test]
        public void SetHasGdprConsent_GivenSomeValue_CallsCorrectNativeMethodWithGdprIntValue()
        {
            // Given
            const GdprConsent gdprConsent = GdprConsent.Granted;

            // When
            _androidBinding.SetHasGDPRConsent(gdprConsent);

            // Then
            _mockAndroidJavaBridge.Received(1).CallNativeMethod("setHasGDPRConsent", (int) gdprConsent);
        }

        // TODO Remove when IAdsBinding.SetHasGDPRConsent(boolean) method is removed
        [Test]
        public void SetHasGdprConsent_GivenTrue_CallsNativeMethodPassingGrantedAsValue()
        {
            // When
            _androidBinding.SetHasGDPRConsent(true);

            // Then
            _mockAndroidJavaBridge.Received(1).CallNativeMethod("setHasGDPRConsent", (int)GdprConsent.Granted);
        }

        // TODO Remove when IAdsBinding.SetHasGDPRConsent(boolean) method is removed
        [Test]
        public void SetHasGdprConsent_GivenFalse_CallsNativeMethodPassingNotGrantedAsValue()
        {
            // When
            _androidBinding.SetHasGDPRConsent(false);

            // Then
            _mockAndroidJavaBridge.Received(1).CallNativeMethod("setHasGDPRConsent", (int)GdprConsent.NotGranted);
        }

        [Test]
        public void SetCustomSegmentationTags_GivenEmptyTags_CallsNativeMethodPassingEmptySet()
        {
            HashSet<String> emptySegmentationTags = new HashSet<string>();
            
            // When
            _androidBinding.SetCustomSegmentationTags(emptySegmentationTags);
            
            // Then
            _mockAndroidJavaBridge.Received(1).CallNativeMethod("setCustomSegmentationTags", "");
        }

        [Test]
        public void SetCustomSegmentationTags_GivenSomeTags_CallsNativeMethodPassingASetWithTags()
        {
            HashSet<String> segmentationTags = new HashSet<string> {"paying_user", "non_paying_user"};
            
            // When
            _androidBinding.SetCustomSegmentationTags(segmentationTags);
            
            // Then
            _mockAndroidJavaBridge.Received(1).CallNativeMethod("setCustomSegmentationTags", "paying_user,non_paying_user");
        }
    }
}