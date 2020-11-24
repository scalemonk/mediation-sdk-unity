#import "SMAds.h"
#import "AdsBindingVideoDelegateViewController.h"
#import "AdsBindingInterstitialViewController.h"

static UIViewController *videoDelegateViewController;
static UIViewController *interstitialDelegateViewController;
static SMAds *smAds;

void SMAdsInitialize(char* applicationId) {
    smAds = [[SMAds alloc] initWith:[NSString stringWithUTF8String: applicationId]];

    [smAds initialize: ^(BOOL success){}];
    videoDelegateViewController = [[AdsBindingVideoDelegateViewController alloc] init];
    interstitialDelegateViewController = [[AdsBindingInterstitialViewController alloc] init];
    
    [smAds addInterstitialListener:(id<SMInterstitialAdEventListener>) interstitialDelegateViewController];
    [smAds addVideoListener:(id<SMRewardedVideoAdEventListener>) videoDelegateViewController];
}

void SMAdsShowInterstitial(char* tagChr) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    [smAds showInterstitialAdWithViewController:UnityGetGLViewController() andTag:tag];
}

void SMAdsShowVideo(char* tagChr) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    [smAds showRewardedVideoAdWithViewController:UnityGetGLViewController()
                                               andTag:tag];
}

bool SMIsRewardedVideoReadyToShow(char* tagChr) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
//    return [adsWrapper isRewardedVideoReadyToShowWithTag:tag];
    return true;
}

bool SMIsInterstitialReadyToShow(char* tagChr) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    return true;
//    return [adsWrapper isInterstitialReadyToShowWithTag:tag];
}

bool SMAreInterstitialsEnabled() {
    return true;
//    return [adsWrapper areInterstitialsEnabled];
}

bool SMAreVideosEnabled() {
    return true;
//    return [adsWrapper areVideosEnabled];
}



