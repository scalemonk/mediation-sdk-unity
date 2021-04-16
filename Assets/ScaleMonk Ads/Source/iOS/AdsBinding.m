//  AdsBinding.m
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html
//

#import <ScaleMonkAds/SMAds.h>
#import "AdsBindingRewardedDelegateViewController.h"
#import "AdsBindingInterstitialViewController.h"

static UIViewController *videoDelegateViewController;
static UIViewController *interstitialDelegateViewController;
static SMAds *smAds;

void SMAdsInitialize() {
    smAds = [[SMAds alloc] init];

    [smAds initialize: ^(BOOL success){}];
    videoDelegateViewController = [[AdsBindingRewardedDelegateViewController alloc] init];
    interstitialDelegateViewController = [[AdsBindingInterstitialViewController alloc] init];
    
    [smAds addInterstitialListener:(id<SMInterstitialAdEventListener>) interstitialDelegateViewController];
    [smAds addRewardedListener:(id<SMRewardedAdEventListener>) videoDelegateViewController];
}

void SMAdsShowInterstitial(char* tagChr) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    [smAds showInterstitialAdWithViewController:UnityGetGLViewController() andTag:tag];
}

void SMAdsShowRewarded(char* tagChr) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    [smAds showRewardedAdWithViewController:UnityGetGLViewController()
                                               andTag:tag];
}

void SMSetApplicationChildDirected(bool isChildDirected){
    [smAds setIsApplicationChildDirected: isChildDirected];
}

void SMSetUserCantGiveGDPRConsent(bool isUnderage){
    [smAds setUserCantGiveGDPRConsentWithStatus: isUnderage];
}
       
void SMSetHasGDPRConsent(bool consent){
    [smAds setHasGDPRConsentWithStatus:consent];
}

bool SMIsRewardedReadyToShow(char* tagChr) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    return [smAds isRewardedReadyToShowWithTag:tag];
}

bool SMIsInterstitialReadyToShow(char* tagChr) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    return [smAds isInterstitialReadyToShowWithTag:tag];
}

bool SMAreInterstitialsEnabled() {
    return [smAds areInterstitialsEnabled];
}

bool SMAreRewardedEnabled() {
    return [smAds areRewardedEnabled];
}
