//  ScaleMonkAds.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System;

namespace ScaleMonk.Ads
{
    public class ScaleMonkAds
    {
        static ScaleMonkAdsSDK _instance;

        /// <summary>
        /// Instance to use the Ads SDK.
        /// </summary>
        public static ScaleMonkAdsSDK SharedInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScaleMonkAdsSDK(AdsFactory.AdsBinding(), AdsFactory.NativeBridgeService(),
                        AdsFactory.AnalyticsService());
                }

                return _instance;
            }
        }

        
        /// <summary>
        /// Initialize ScaleMonk SDK
        /// 
        /// </summary>
        public static void Initialize()
        {
            SharedInstance.Initialize(() => { });
        }

        /// <summary>
        /// Initialize ScaleMonk SDK
        ///
        /// <param name="callback">The callback that will be called after the ScaleMonk SDK is initialized</param>
        /// </summary>
        public static void Initialize(Action callback)
        {
            SharedInstance.Initialize(callback);
        }
    }
}