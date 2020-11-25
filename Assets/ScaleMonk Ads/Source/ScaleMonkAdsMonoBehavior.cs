using UnityEngine;

namespace ScaleMonk.Ads
{
    public class ScaleMonkAdsMonoBehavior : MonoBehaviour
    {
        const string _label = "AdsMonoBehaviour";

        static GameObject _gameObject;
        static ScaleMonkAds _adsInstance;

        public static void Initialize(ScaleMonkAds adsInstance)
        {
            _adsInstance = adsInstance;
            CreateGameObject();
        }

        static void CreateGameObject()
        {
            if (_gameObject != null)
            {
                Debug.Log("AdsMonoBehaviour game object already created.");
                return;
            }

            _gameObject = new GameObject("AdsMonoBehaviour");
            _gameObject.AddComponent<ScaleMonkAdsMonoBehavior>();
            DontDestroyOnLoad(_gameObject);
        }

        public void CompletedVideoDisplay(string location)
        {
            Debug.LogFormat("[{0}] Completed video display at location {1}", _label, location);
            _adsInstance.CompletedVideoDisplay(location);
        }

        public void StartedVideoDisplay(string location)
        {
            Debug.LogFormat("[{0}] Started video display at location {1}", _label, location);
            _adsInstance.StartedVideoDisplay(location);
        }

        public void ClickedVideo(string location)
        {
            Debug.LogFormat("[{0}] Clicked video at location {1}", _label, location);
            _adsInstance.ClickedVideo(location);
        }

        public void FailedVideoDisplay(string location)
        {
            Debug.LogFormat("[{0}] Failed video display at location {1}", _label, location);
            _adsInstance.FailedVideoDisplay(location);
        }

        public void CompletedInterstitialDisplay(string location)
        {
            Debug.LogFormat("[{0}] Completed interstitial display at location {1}", _label, location);
            _adsInstance.CompletedInterstitialDisplay(location);
        }

        public void ClickedInterstitial(string location)
        {
            Debug.LogFormat("[{0}] Clicked interstitial at location {1}", _label, location);
            _adsInstance.ClickedInterstitial(location);
        }

        public void FailedInterstitialDisplay(string location)
        {
            Debug.LogFormat("[{0}] Failed interstitial display at location {1}", _label, location);
            _adsInstance.FailedInterstitialDisplay(location);
        }
        
        public void InterstitialReady()
        {
            Debug.LogFormat("[{0}] Interstitial display to display", _label);
            _adsInstance.InterstitialReady();
        }
        public void VideoReady()
        {
            Debug.LogFormat("[{0}] Rewarded display to display", _label);
            _adsInstance.VideoReady();
        }
    
    }
}