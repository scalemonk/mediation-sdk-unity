using System;
using System.Collections.Generic;

namespace ScaleMonk.Ads
{
    public class ScaleMonkAdsSDK : IScaleMonkAdsSDK
    {
        private const string Label = "ScaleMonkAds";
        private const string DefaultTag = "DEFAULT_TAG";

        private readonly IAdsBinding _adsBinding;
        private static Action _initializationCallback;
        private bool _isInitialized;
        private bool _hasAnalyticsBinding;
        private bool _isBannerPresent;
        private readonly BannerSize _defaultBannerSize = BannerSize.Small;
        private readonly INativeBridgeService _nativeBridgeService;
        private readonly AnalyticsService _analyticsService;

        /// <summary>
        /// Creates a new Ads SDK
        /// </summary>
        /// <param name="adsBinding">Ads Binding implementation for the current platform (Android/iOS/Editor)</param>
        /// <param name="nativeBridgeService"></param>
        /// <param name="analyticsService"></param>
        public ScaleMonkAdsSDK(IAdsBinding adsBinding, INativeBridgeService nativeBridgeService,
            AnalyticsService analyticsService)
        {
            _adsBinding = adsBinding;
            _nativeBridgeService = nativeBridgeService;
            _analyticsService = analyticsService;
        }

        /// <summary>
        /// Informs the rewarded display was successful. Use this callback to restart the app state and give rewards to the user.
        /// </summary>
        public Action RewardedDisplayedEvent;

        /// <summary>
        /// Informs the rewarded display has started. Use this callback to pause any behavior you might need.
        /// </summary>
        public Action RewardedStartedEvent;

        /// <summary>
        /// Informs the rewarded display was not successful.
        ///
        /// To see the reason for the failed display, check the reason field in the `ads:display-failed` analytics event.
        /// </summary>
        public Action RewardedNotDisplayedEvent;

        /// <summary>
        /// Informs the rewarded ad was clicked.
        /// </summary>
        public Action RewardedClickedEvent;

        /// <summary>
        /// Informs the interstitial display was successful. Use this callback to restart the app state and give rewards to the user.
        /// </summary>
        public Action InterstitialDisplayedEvent;

        /// <summary>
        /// Informs the interstitial display was not successful.
        ///
        /// To see the reason for the failed display, check the reason field in the `ads:display-failed` analytics event.
        /// </summary>
        public Action InterstitialNotDisplayedEvent;

        /// <summary>
        /// Informs the interstitial ad was clicked.
        /// </summary>
        public Action InterstitialClickedEvent;

        /// <summary>
        /// Informs that an interstitial ad has been successfully cached and is ready to be shown.
        /// </summary>
        public Action InterstitialReadyEvent;

        /// <summary>
        /// Informs that an interstitial ad has not been successfully cached and is not ready to be shown.
        /// </summary>
        public Action InterstitialNotReadyEvent;

        /// <summary>
        /// Informs that a rewarded ad has been successfully cached and is ready to be shown.
        /// </summary>
        public Action RewardedReadyEvent;

        /// <summary>
        /// Informs that a rewarded ad has not been successfully cached and is not ready to be shown.
        /// </summary>
        public Action RewardedNotReadyEvent;

        /// <summary>
        /// Informs the banner display was not successful.
        ///
        /// To see the reason for the failed display, check the reason field in the `ads:display-failed` analytics event.
        /// </summary>
        public Action BannerFailedDisplayedEvent;

        /// <summary>
        /// Informs the banner display was successful.
        /// </summary>
        public Action BannerCompletedDisplayedEvent;

        /// <summary>
        /// Informs the sdk was initialized.
        /// </summary>
        public Action InitializationCompletedEvent;

        /// <summary>
        /// Tells the ScaleMonk SDK whether the user has granted consent as prescribed by the GDPR laws and that data can be collected
        ///
        /// </summary>
        /// <param name="consent"> True if the user has granted consent, false otherwise
        /// </param>
        [Obsolete("Use \"void SetHasGDPRConsent(GdprConsent status)\" method instead.")]
        public void SetHasGDPRConsent(bool consent)
        {
            SetHasGDPRConsent(consent ? GdprConsent.Granted : GdprConsent.NotGranted);
        }

        /// <summary>
        /// Tells the ScaleMonk SDK whether the user has granted consent as prescribed by the GDPR laws and that data can be collected
        ///
        /// </summary>
        /// <param name="consent"> True if the user has granted consent, false otherwise
        /// </param>
        public void SetHasGDPRConsent(GdprConsent consent)
        {
            _adsBinding.SetHasGDPRConsent(consent);
        }

        /// <summary>
        /// Tells the ScaleMonk SDK whether the application is targeted to children and should only show age-appropriate ads
        ///
        /// </summary>
        /// <param name="isChildDirected"> True if the app is child directed, false otherwise
        /// </param>
        public void SetIsApplicationChildDirected(bool isChildDirected)
        {
            RunIfInitialized(() => { _adsBinding.SetIsApplicationChildDirected(isChildDirected); });
        }

        /// <summary>
        /// Tells the ScaleMonk SDK whether the application is targeted to children and should only show age-appropriate ads
        ///
        /// </summary>
        /// <param name="status"> Child directed status
        /// </param>
        public void SetIsApplicationChildDirected(CoppaStatus status)
        {
            _adsBinding.SetIsApplicationChildDirected(status);
        }

        /// <summary>
        /// Tells the ScaleMonk SDK that the user can't give consent for GDPR since they're underage
        ///
        /// </summary>
        /// <param name="consent"> True if the user is underage, false otherwise
        /// </param>
        public void SetUserCantGiveGDPRConsent(bool cantGiveConsent)
        {
            RunIfInitialized(() => { _adsBinding.SetUserCantGiveGDPRConsent(cantGiveConsent); });
        }

        public void SetCustomSegmentationTags(HashSet<string> tags)
        {
            _adsBinding.SetCustomSegmentationTags(tags);
        }

        public void AddAnalytics(IAnalytics analytics)
        {
            _analyticsService.AddAnalytics(analytics);
        }

        /// <summary>
        /// Set a new id to track the user instead using the FIU
        ///
        /// </summary>
        /// <param name="customUserId"> UserId that replace the FIU
        /// </param>
        public void SetCustomUserId(string customUserId)
        {
            _adsBinding.SetCustomUserId(customUserId);
        }

        /// <summary>
        /// Displays an interstitial ad.
        ///
        /// If the display was successful, the event `InterstitialDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `InterstitialNotDisplayedEvent` will be called.
        public void ShowInterstitial()
        {
            ShowInterstitial(DefaultTag);
        }

        /// <summary>
        /// Displays an interstitial ad.
        ///
        /// If the display was successful, the event `InterstitialDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `InterstitialNotDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        public void ShowInterstitial(string tag)
        {
            RunIfInitialized(() =>
            {
                AdsLogger.LogWithFormat("{0} | Show interstitial at tag {1}", Label, tag);
                _adsBinding.ShowInterstitial(tag);
            });
        }

        /// <summary>
        /// Returns true if there is an instance of interstitial ad ready to be shown. Otherwise, it returns false.
        ///
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        public bool IsInterstitialReadyToShow(string tag)
        {
            return _adsBinding.IsInterstitialReadyToShow(tag);
        }

        /// <summary>
        /// Displays a rewarded ad.
        ///
        /// If the display was successful, the event `RewardedDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `RewardedNotDisplayedEvent` will be called.
        /// </summary>
        public void ShowRewarded()
        {
            ShowRewarded(DefaultTag);
        }

        /// <summary>
        /// Displays a rewarded ad.
        ///
        /// If the display was successful, the event `RewardedDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `RewardedNotDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        public void ShowRewarded(string tag)
        {
            RunIfInitialized(() =>
            {
                AdsLogger.LogWithFormat("{0} | Show rewarded at tag {1}", Label, tag);
                _adsBinding.ShowRewarded(tag);
            });
        }

        /// <summary>
        /// Returns true if there is an instance of rewarded ad ready to be shown. Otherwise, it returns false.
        ///
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        public bool IsRewardedReadyToShow(string tag)
        {
            return _adsBinding.IsRewardedVideoReadyToShow(tag);
        }

        /// <summary>
        /// Displays a banner ad. Returns a <c>Banner</c> that you will use to stop the rotation later. <br/>
        ///
        /// If the display was successful, the event `BannerCompletedDisplayedEvent` will be called.
        /// Otherwise, the event `BannerFailedDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        /// <param name="bannerSize">The bannerSize of the Ad.</param>
        /// <param name="bannerPosition">The bannerPosition where the ad will be displayed.</param>
        public Banner ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition)
        {
            Banner banner = null;
            RunIfInitialized(() =>
            {
                AdsLogger.LogWithFormat("{0} | Show banner at tag {1}", Label, tag);
                string id = _adsBinding.ShowBanner(tag, bannerSize, bannerPosition);
                banner = new Banner(id);
            });
            return banner;
        }

        /// <summary>
        /// Displays a banner ad. Returns a <c>Banner</c> that you will use to stop the rotation later. <br/>
        ///
        /// If the display was successful, the event `BannerCompletedDisplayedEvent` will be called.
        /// Otherwise, the event `BannerFailedDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        /// <param name="bannerPosition">The bannerPosition where the ad will be displayed.</param>
        public Banner ShowBanner(string tag, BannerPosition bannerPosition)
        {
            return ShowBanner(tag, _defaultBannerSize, bannerPosition);
        }

        /// <summary>
        /// Displays a banner ad. Returns a <c>Banner</c> that you will use to stop the rotation later. <br/>
        ///
        /// If the display was successful, the event `BannerCompletedDisplayedEvent` will be called.
        /// Otherwise, the event `BannerFailedDisplayedEvent` will be called.
        /// </summary>
        /// <param name="bannerPosition">The bannerPosition where the ad will be displayed.</param>
        public Banner ShowBanner(BannerPosition bannerPosition)
        {
            return ShowBanner(DefaultTag, _defaultBannerSize, bannerPosition);
        }

        /// <summary>
        /// Displays a banner ad. Returns a <c>Banner</c> that you will use to stop the rotation later. <br/>
        ///
        /// If the display was successful, the event `BannerCompletedDisplayedEvent` will be called.
        /// Otherwise, the event `BannerFailedDisplayedEvent` will be called.
        /// </summary>
        /// <param name="bannerSize">The bannerSize of the Ad.</param>
        /// <param name="bannerPosition">The bannerPosition where the ad will be displayed.</param>
        public Banner ShowBanner(BannerSize bannerSize, BannerPosition bannerPosition)
        {
            return ShowBanner(DefaultTag, bannerSize, bannerPosition);
        }

        /// <summary>
        /// Stops a banner ad.
        ///
        /// </summary>
        /// <param name="banner">The <c>Banner</c> that was returned when calling <c>ShowBanner</c></param>
        public void StopBanner(Banner banner)
        {
            RunIfInitialized(() =>
            {
                if (banner != null)
                {
                    AdsLogger.LogWithFormat("{0} | Stop banner with id {1}", Label, banner.ID);
                    _adsBinding.StopBanner(banner.ID);
                }
                else
                {
                    AdsLogger.LogWithFormat("{0} | Stopping all banners", Label);
                    _adsBinding.StopBanner();
                }
            });
        }

        /// <summary>
        /// Stops a banner ad.
        ///
        /// </summary>
        public void StopBanner()
        {
            StopBanner(null);
        }

        /// <summary>
        /// Sets the UserType
        /// </summary>
        /// <param name="userType"></param>
        [Obsolete("Use \"void SetCustomSegmentationTags(HashSet<String> tags)\" method instead.")]
        public void SetUserType(UserType userType)
        {
            _adsBinding.SetUserType(userType);
        }

        public bool IsInitialized()
        {
            return _isInitialized;
        }

        private void InitializeBinding()
        {
            _adsBinding.Initialize(this);
        }
        
        public bool IsBannerPresent()
        {
            return _isBannerPresent;
        }

        static void CallAction(Action action)
        {
            if (action != null)
            {
                action();
            }
        }

        #region Ads Native Binding Callbacks

        public void CompletedRewardedDisplay(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Rewarded displayed at tag {1}", Label, tag);
            CallAction(RewardedDisplayedEvent);
        }

        public void StartedRewardedDisplay(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Started rewarded at tag {1}", Label, tag);
            CallAction(RewardedStartedEvent);
        }

        public void ClickedRewarded(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Clicked rewarded at tag {1}", Label, tag);
            CallAction(RewardedClickedEvent);
        }

        public void FailedRewardedDisplay(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Rewarded not displayed at tag {1}", Label, tag);
            CallAction(RewardedNotDisplayedEvent);
        }

        public void CompletedInterstitialDisplay(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Interstitial displayed at tag {1}", Label, tag);
            CallAction(InterstitialDisplayedEvent);
        }

        public void ClickedInterstitial(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Clicked interstitial at tag {1}", Label, tag);
            CallAction(InterstitialClickedEvent);
        }

        public void FailedInterstitialDisplay(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Interstitial not displayed at tag {1}", Label, tag);
            CallAction(InterstitialNotDisplayedEvent);
        }

        public void InterstitialReady()
        {
            AdsLogger.LogWithFormat("{0} | Interstitial ready to be displayed", Label);
            CallAction(InterstitialReadyEvent);
        }

        public void InterstitialNotReady()
        {
            AdsLogger.LogWithFormat("{0} | Interstitial not ready to be displayed", Label);
            CallAction(InterstitialNotReadyEvent);
        }

        public void RewardedReady()
        {
            AdsLogger.LogWithFormat("{0} | Rewarded ad ready to be displayed", Label);
            CallAction(RewardedReadyEvent);
        }

        public void RewardedNotReady()
        {
            AdsLogger.LogWithFormat("{0} | Rewarded ad not ready to be displayed", Label);
            CallAction(RewardedNotReadyEvent);
        }

        public void FailedBannerDisplay(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Banner not displayed at tag {1}", Label, tag);
            _isBannerPresent = false;
            CallAction(BannerFailedDisplayedEvent);
        }

        public void CompletedBannerDisplay(string tag)
        {
            AdsLogger.LogWithFormat("{0} | Banner displayed at tag {1}", Label, tag);
            _isBannerPresent = true;
            CallAction(BannerCompletedDisplayedEvent);
        }

        public void InitializationCompleted()
        {
            AdsLogger.LogWithFormat("{0} | SDK Initialization Completed", Label);
            _isInitialized = true;
            CallAction(_initializationCallback);
            CallAction(InitializationCompletedEvent);
        }

        #endregion

        private void RunIfInitialized(Action action)
        {
            if (!_isInitialized)
            {
                AdsLogger.LogInfo("ScaleMonk SDK must be initialized. Make sure to call ScaleMonkAds.Initialize()");
                return;
            }

            action();
        }

        public void Initialize(Action callback)
        {
            if (_isInitialized)
            {
                AdsLogger.LogWarning("{0} | Ads SDK already initialized", Label);
                return;
            }

            AdsLogger.LogWithFormat("{0} | Initializing Ads SDK", Label);

            _initializationCallback = callback;

            _nativeBridgeService.Initialize(this);
            InitializeBinding();
        }

        public void SendEvent(string analyticsEvent)
        {
            _analyticsService.SendEvent(analyticsEvent);
        }
    }

    public class Banner
    {
        internal Banner(string id)
        {
            ID = id;
        }
        public string ID { get; private set; }
    }
}