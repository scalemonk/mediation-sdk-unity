//  AdsBinding.m
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html
//

#import <ScaleMonkAds/SMAds.h>
#import "AdsBindingRewardedDelegateViewController.h"
#import "AdsBindingInterstitialViewController.h"
#import "AdsBindingBannerViewController.h"

static UIViewController *videoDelegateViewController;
static UIViewController *interstitialDelegateViewController;
static UIViewController *bannerDelegateViewController;
static SMAds *smAds;

void SMAdsInitialize() {
    smAds = [[SMAds alloc] init];

    [smAds initialize: ^(BOOL success){}];
    videoDelegateViewController = [[AdsBindingRewardedDelegateViewController alloc] init];
    interstitialDelegateViewController = [[AdsBindingInterstitialViewController alloc] init];
    bannerDelegateViewController = [[AdsBindingBannerViewController alloc] init];
    
    [smAds addInterstitialListener:(id<SMInterstitialAdEventListener>) interstitialDelegateViewController];
    [smAds addRewardedListener:(id<SMRewardedAdEventListener>) videoDelegateViewController];
    [smAds addBannerListener:(id<SMBannerAdEventListener>) bannerDelegateViewController];
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

static CGPoint GetPosition(int bannerHeight, int bannerWidth, NSString *strPosition, UIView *containerView) {
    CGFloat topPadding = 0;
    CGFloat bottomPadding = 0;
    CGFloat leftPadding = 0;
    CGFloat rightPadding = 0;
    
    CGFloat x;
    CGFloat y;
    
    if (@available(iOS 11.0, *)) {
        topPadding = UIApplication.sharedApplication.windows.firstObject.safeAreaInsets.top;
        bottomPadding = UIApplication.sharedApplication.windows.firstObject.safeAreaInsets.bottom;
        leftPadding = UIApplication.sharedApplication.windows.firstObject.safeAreaInsets.left;
        rightPadding = UIApplication.sharedApplication.windows.firstObject.safeAreaInsets.right;
    }
    
    NSArray *positions = @[@"bottom_center",
                           @"bottom_left",
                           @"bottom_right",
                           @"top_center",
                           @"top_left",
                           @"top_right",
                           @"centered",
                           @"center_left",
                           @"center_right"
    ];
    
    switch ([positions indexOfObject:strPosition]) {
        case 0: // bottom_center
            x = containerView.center.x - (bannerWidth / 2);
            y = containerView.bounds.size.height - bannerHeight - bottomPadding;
            break;
        case 1: // bottom_left
            x = 0 + leftPadding;
            y = containerView.bounds.size.height - bannerHeight - bottomPadding;
            break;
        case 2: // bottom_right
            x = containerView.bounds.size.width - bannerWidth - rightPadding;
            y = containerView.bounds.size.height - bannerHeight - bottomPadding;
            break;
        case 3: //top_center
            x = containerView.center.x - (bannerWidth / 2);
            y = 0 - topPadding;
            break;
        case 4: // top_left
            x = 0 + leftPadding;
            y = 0 - topPadding;
            break;
        case 5: // top_right
            x = containerView.bounds.size.width - bannerWidth - rightPadding;
            y = 0 - topPadding;
            break;
        case 6: // centered
            x = containerView.center.x - (bannerWidth / 2);
            y = containerView.center.y - (bannerHeight / 2);
            break;
        case 7: // center_left
            x = 0 + leftPadding;
            y = containerView.center.y - (bannerHeight / 2);
            break;
        case 8: // center_right
            x = containerView.bounds.size.width - bannerWidth - rightPadding;
            y = containerView.center.y - (bannerHeight / 2);
            break;
        default: // To bottom_center
            x = containerView.center.x - (bannerWidth / 2);
            y = containerView.bounds.size.height - bannerHeight - bottomPadding;
            break;
    }
    
    return CGPointMake(x, y);
}

void SMAdsShowBanner(char* tagChr, int width, int height, char* position) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    UIViewController *viewController = UnityGetGLViewController();
    NSString *positionAsString = [NSString stringWithUTF8String:position];

    CGPoint positionInAxis = GetPosition(height, width, positionAsString, viewController.view);
    
    SMBannerView *bannerView = [[SMBannerView alloc] init];
    bannerView.frame = CGRectMake(positionInAxis.x, positionInAxis.y, width, height);
    
    bannerView.viewController = viewController;
    [viewController.view addSubview:bannerView];
    
    [smAds showBannerAdWithViewController: viewController
                                       bannerView:bannerView
                                           andTag:tag];
}

void SMAdsStopBanner(char* tagChr) {
    [smAds stopLoadingBanners];
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
