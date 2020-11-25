using System;
using ScaleMonk.Ads.iOS;
using ScaleMonk.Ads.Android;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public class ScaleMonkAds
    {
        
        static ScaleMonkAds _instance;
        readonly IAdsBinding _adsBinding;
        public string ApplicationId;

        /// <summary>
        /// Instance to use the Ads SDK.
        /// </summary>
        public static ScaleMonkAds SharedInstance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("You should call Ads.Initialize first");
                }

                return _instance;
            }
        }
        
        /// <summary>
        /// Initializes the SDK.
        ///
        /// <param name="applicationId">The identifier for the application that will be using the Mediation SDK</param>
        /// </summary>
        public static void Initialize(string applicationId)
        {
            if (_instance != null)
            {
                Debug.LogWarning("Ads SDK already initialized");
                return;
            }

            Debug.Log("Initializing Ads SDK");

            _instance = new ScaleMonkAds(applicationId);
            ScaleMonkAdsMonoBehavior.Initialize(_instance);
            _instance.InitializeInternal();
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
            Debug.LogFormat("Show interstitial tag={0}", tag);
            _adsBinding.ShowInterstitial(tag);
        }

        /// <summary>
        /// Displays a rewarded video ad.
        ///
        /// If the display was successful, the event `VideoDisplayedEvent` will be called when the ad closes.
        /// Otherwise, the event `VideoNotDisplayedEvent` will be called.
        /// </summary>
        /// <param name="tag">The game tag from where the ad will be displayed (like menu or store).</param>
        public void ShowVideo(string tag)
        {
            Debug.LogFormat("Show video tag={0}", tag);
            _adsBinding.ShowVideo(tag);
        }
        
        /// <summary>
        /// Creates a new Ads SDK
        /// </summary>
        /// <param name="applicationId">The application identifier to be used by CAdS and Exchange to identify the game</param>
        ScaleMonkAds(string applicationId)
        {
            ApplicationId = applicationId;
            _adsBinding = GetAdsBinding();
        }
        
        void InitializeInternal()
        {
            _adsBinding.Initialize(this);
        }
        
        static void CallAction(Action action)
        {
            if (action != null)
            {
                action();
            }
        }

        
        /// <summary>
        /// Informs the video display was successful. Use this callback to restart the app state and give rewards to the user.
        /// </summary>
        public static Action VideoDisplayedEvent;

        /// <summary>
        /// Informs the video display has started. Use this callback to pause any behavior you might need.
        /// </summary>
        public static Action VideoStartedEvent;

        /// <summary>
        /// Informs the video display was not successful.
        ///
        /// To see the reason for the failed display, check the reason field in the `ads:display-failed` analytics event.
        /// </summary>
        public static Action VideoNotDisplayedEvent;

        /// <summary>
        /// Informs the video ad was clicked.
        /// </summary>
        public static Action VideoClickedEvent;

        /// <summary>
        /// Informs the interstitial display was successful. Use this callback to restart the app state and give rewards to the user.
        /// </summary>
        public static Action InterstitialDisplayedEvent;

        /// <summary>
        /// Informs the interstitial display was not successful.
        ///
        /// To see the reason for the failed display, check the reason field in the `ads:display-failed` analytics event.
        /// </summary>
        public static Action InterstitialNotDisplayedEvent;

        /// <summary>
        /// Informs the interstitial ad was clicked.
        /// </summary>
        public static Action InterstitialClickedEvent;
        
        /// <summary>
        /// Informs that an interstitial ad has been successfully cached and is ready to be shown.
        /// </summary>
        public static Action InterstitialReadyEvent;
        
        /// <summary>
        /// Informs that a rewarded video ad has been successfully cached and is ready to be shown.
        /// </summary>
        public static Action VideoReadyEvent;
        
        #region Ads Native Binding Callbacks
        public void CompletedVideoDisplay(string tag)
        {
            Debug.LogFormat("[Ads] Video displayed at tag {0}", tag);
            CallAction(VideoDisplayedEvent);
        }

        public void StartedVideoDisplay(string tag)
        {
            Debug.LogFormat("[Ads] Started video at tag {0}", tag);
            CallAction(VideoStartedEvent);
        }

        public void ClickedVideo(string tag)
        {
            Debug.LogFormat("[Ads] Clicked video at tag {0}", tag);
            CallAction(VideoClickedEvent);
        }

        public void FailedVideoDisplay(string tag)
        {
            Debug.LogFormat("[Ads] Video not displayed at tag {0}", tag);
            CallAction(VideoNotDisplayedEvent);
        }

        public void CompletedInterstitialDisplay(string tag)
        {
            Debug.LogFormat("[Ads] Interstitial displayed at tag {0}", tag);
            CallAction(InterstitialDisplayedEvent);
        }

        public void ClickedInterstitial(string tag)
        {
            Debug.LogFormat("[Ads] Clicked interstitial at tag {0}", tag);
            CallAction(InterstitialClickedEvent);
        }

        public void FailedInterstitialDisplay(string tag)
        {
            Debug.LogFormat("[Ads] Interstitial not displayed at tag {0}", tag);
            CallAction(InterstitialNotDisplayedEvent);
        }
        
        public void InterstitialReady()
        {
            Debug.LogFormat("[Ads] Interstitial ready to be displayed");
            CallAction(InterstitialReadyEvent);
        }
         public void VideoReady()
        {
            Debug.LogFormat("[Ads] Rewarded Video ad ready to be displayed");
            CallAction(VideoReadyEvent);
        }
  
        #endregion

        
        static IAdsBinding GetAdsBinding()
        {
            IAdsBinding binding = null;

#if UNITY_ANDROID
            binding = new AdsAndroidBinding();
#elif UNITY_IOS
            binding = new AdsiOSBinding();
#elif UNITY_EDITOR
            binding = new AdsEditorBinding();
#endif
            return binding;
        }
    }
}