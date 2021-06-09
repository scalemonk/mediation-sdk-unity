using System;

namespace ScaleMonk.Ads
{
    public interface IScaleMonkAdsSDK
    {
        /// <summary>
        /// Tells the ScaleMonk SDK whether the user has granted consent as prescribed by the GDPR laws and that data can be collected
        ///
        /// </summary>
        /// <param name="consent"> True if the user has granted consent, false otherwise
        /// </param>
        void SetHasGDPRConsent(bool consent);

        /// <summary>
        /// Tells the ScaleMonk SDK whether the application is targeted to children and should only show age-appropriate ads
        ///
        /// </summary>
        /// <param name="isChildDirected"> True if the app is child directed, false otherwise
        /// </param>
        void SetIsApplicationChildDirected(bool isChildDirected);

        /// <summary>
        /// Tells the ScaleMonk SDK that the user can't give consent for GDPR since they're underage
        ///
        /// </summary>
        /// <param name="consent"> True if the user is underage, false otherwise
        /// </param>
        void SetUserCantGiveGDPRConsent(bool cantGiveConsent);

        void AddAnalytics(IAnalytics analytics );

        /// <summary>
        /// Set a new id to track the user instead using the FIU
        ///
        /// </summary>
        /// <param name="customUserId"> UserId that replace the FIU
        /// </param>
        void SetCustomUserId(string customUserId);

        /// <summary>
        /// Displays an interstitial ad.
        ///
        /// If the display was successful, the event `InterstitialDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `InterstitialNotDisplayedEvent` will be called.
        void ShowInterstitial();

        /// <summary>
        /// Displays an interstitial ad.
        ///
        /// If the display was successful, the event `InterstitialDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `InterstitialNotDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        void ShowInterstitial(string tag);

        /// <summary>
        /// Displays a rewarded ad.
        ///
        /// If the display was successful, the event `RewardedDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `RewardedNotDisplayedEvent` will be called.
        /// </summary>
        void ShowRewarded();

        /// <summary>
        /// Displays a rewarded ad.
        ///
        /// If the display was successful, the event `RewardedDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `RewardedNotDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        void ShowRewarded(string tag);

        /// <summary>
        /// Displays a banner ad.
        ///
        /// If the display was successful, the event `BannerCompletedDisplayedEvent` will be called.
        /// Otherwise, the event `BannerFailedDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        /// <param name="bannerSize">The bannerSize of the Ad.</param>
        /// <param name="bannerPosition">The bannerPosition where the ad will be displayed.</param>
        void ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition);

        /// <summary>
        /// Displays a banner ad.
        ///
        /// If the display was successful, the event `BannerCompletedDisplayedEvent` will be called.
        /// Otherwise, the event `BannerFailedDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        /// <param name="bannerPosition">The bannerPosition where the ad will be displayed.</param>
        void ShowBanner(string tag, BannerPosition bannerPosition);

        /// <summary>
        /// Displays a banner ad.
        ///
        /// If the display was successful, the event `BannerCompletedDisplayedEvent` will be called.
        /// Otherwise, the event `BannerFailedDisplayedEvent` will be called.
        /// </summary>
        /// <param name="bannerPosition">The bannerPosition where the ad will be displayed.</param>
        void ShowBanner(BannerPosition bannerPosition);

        /// <summary>
        /// Displays a banner ad.
        ///
        /// If the display was successful, the event `BannerCompletedDisplayedEvent` will be called.
        /// Otherwise, the event `BannerFailedDisplayedEvent` will be called.
        /// </summary>
        /// <param name="bannerSize">The bannerSize of the Ad.</param>
        /// <param name="bannerPosition">The bannerPosition where the ad will be displayed.</param>
        void ShowBanner(BannerSize bannerSize, BannerPosition bannerPosition);

        /// <summary>
        /// Stops a banner ad.
        ///
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be removed from (like menu or store).</param>
        void StopBanner(string tag);

        /// <summary>
        /// Stops a banner ad.
        ///
        /// </summary>
        void StopBanner();

        void InitializeBinding();
        void CompletedRewardedDisplay(string tag);
        void StartedRewardedDisplay(string tag);
        void ClickedRewarded(string tag);
        void FailedRewardedDisplay(string tag);
        void CompletedInterstitialDisplay(string tag);
        void ClickedInterstitial(string tag);
        void FailedInterstitialDisplay(string tag);
        void InterstitialReady();
        void InterstitialNotReady();
        void RewardedReady();
        void RewardedNotReady();
        void FailedBannerDisplay(string tag);
        void CompletedBannerDisplay(string tag);
        void InitializationCompleted();
        void Initialize(Action callback);
    }
}