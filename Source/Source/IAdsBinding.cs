//  IAdsBinding.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System;
using System.Collections.Generic;

namespace ScaleMonk.Ads
{
    public interface IAdsBinding
    {
        void Initialize(ScaleMonkAdsSDK adsInstance);
        
        void ShowInterstitial(string tag);
        
        void ShowRewarded(string tag);
      
        string ShowBanner(string tag, BannerSize bannerSize, BannerPosition bannerPosition);
      
        void StopBanner(string id);
      
        void StopBanner();
        bool IsInterstitialReadyToShow(string tag);
        
        bool IsRewardedVideoReadyToShow(string tag);
        
        bool AreRewardedEnabled();
        
        bool AreInterstitialsEnabled();
        
        [Obsolete("Use \"void SetHasGDPRConsent(GdprConsent status)\" method instead.")]
        void SetHasGDPRConsent(bool consent);
        
        void SetHasGDPRConsent(GdprConsent consent);
        
        [Obsolete("Use \"void SetIsApplicationChildDirected(CoppaStatus status)\" method instead.")]
        
        void SetIsApplicationChildDirected(bool isChildDirected);
        
        void SetIsApplicationChildDirected(CoppaStatus status);
        
        void SetUserCantGiveGDPRConsent(bool cantGiveConsent);
        
        void SetCustomUserId(string customUserId);

        void SetCustomSegmentationTags(HashSet<String> tags);
        
        [Obsolete("Use \"void SetCustomSegmentationTags(HashSet<String> tags)\" method instead.")]
        void SetUserType(UserType userType);
    }
}