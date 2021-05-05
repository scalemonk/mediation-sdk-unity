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

    [smAds initialize: ^(BOOL success){
        NSLog(@"Initialization Completed");
        UnitySendMessage("AdsMonoBehaviour", "InitializationCompleted", [@"" cStringUsingEncoding:NSUTF8StringEncoding]);
    }];
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

static NSArray<NSLayoutConstraint*>* getConstraintsForSizing(int width, int height, UIView *bannerView) {
    NSLayoutConstraint *heightConstraint;
    NSLayoutConstraint *widthConstraint;
        
    if (@available(iOS 11, *)) {
        heightConstraint = [bannerView.heightAnchor constraintEqualToConstant:height];
        widthConstraint = [bannerView.widthAnchor constraintEqualToConstant:width];
    } else {
        heightConstraint = [NSLayoutConstraint constraintWithItem:bannerView
                                                        attribute:NSLayoutAttributeHeight
                                                        relatedBy:NSLayoutRelationEqual
                                                           toItem:nil
                                                        attribute:NSLayoutAttributeNotAnAttribute
                                                       multiplier:1
                                                         constant:height];

        widthConstraint = [NSLayoutConstraint constraintWithItem:bannerView
                                                       attribute:NSLayoutAttributeWidth
                                                       relatedBy:NSLayoutRelationEqual
                                                          toItem:nil
                                                       attribute:NSLayoutAttributeNotAnAttribute
                                                      multiplier:1
                                                        constant:width];
    }
        
    return @[heightConstraint, widthConstraint];
}

static NSArray<NSLayoutConstraint*>* getConstraintsForPosition(NSString *strPosition, UIView *bannerView, UIView *containerView) {
    NSLayoutConstraint *bottomConstraint;
    NSLayoutConstraint *topConstraint;
    NSLayoutConstraint *leftConstraint;
    NSLayoutConstraint *rightConstraint;
    NSLayoutConstraint *horizontalCenterConstraint;
    NSLayoutConstraint *verticalCenterConstraint;
    
    if (@available(iOS 11, *)) {
        UILayoutGuide *guide = containerView.safeAreaLayoutGuide;
        bottomConstraint = [bannerView.bottomAnchor constraintEqualToSystemSpacingBelowAnchor:guide.bottomAnchor multiplier:1];
        leftConstraint = [bannerView.leftAnchor constraintEqualToSystemSpacingAfterAnchor:guide.leftAnchor multiplier:1];
        rightConstraint = [bannerView.rightAnchor constraintEqualToSystemSpacingAfterAnchor:guide.rightAnchor multiplier:1];
        topConstraint = [bannerView.topAnchor constraintEqualToSystemSpacingBelowAnchor:guide.topAnchor multiplier:1];
        horizontalCenterConstraint = [bannerView.centerXAnchor constraintEqualToSystemSpacingAfterAnchor:guide.centerXAnchor multiplier:1];
        verticalCenterConstraint = [bannerView.centerYAnchor constraintEqualToSystemSpacingBelowAnchor:guide.centerYAnchor multiplier:1];
    } else {
        bottomConstraint = [NSLayoutConstraint constraintWithItem:bannerView
                                                        attribute:NSLayoutAttributeBottom
                                                        relatedBy:NSLayoutRelationEqual
                                                           toItem:containerView
                                                        attribute:NSLayoutAttributeBottom
                                                       multiplier:1
                                                         constant:0];
        
        leftConstraint = [NSLayoutConstraint constraintWithItem:bannerView
                                                      attribute:NSLayoutAttributeLeft
                                                      relatedBy:NSLayoutRelationEqual
                                                         toItem:containerView
                                                      attribute:NSLayoutAttributeLeft
                                                     multiplier:1
                                                       constant:0];
        
        rightConstraint = [NSLayoutConstraint constraintWithItem:bannerView
                                                       attribute:NSLayoutAttributeRight
                                                       relatedBy:NSLayoutRelationEqual
                                                          toItem:containerView
                                                       attribute:NSLayoutAttributeRight
                                                      multiplier:1
                                                        constant:0];
        
        topConstraint = [NSLayoutConstraint constraintWithItem:bannerView
                                                     attribute:NSLayoutAttributeTop
                                                     relatedBy:NSLayoutRelationEqual
                                                        toItem:containerView
                                                     attribute:NSLayoutAttributeTop
                                                    multiplier:1
                                                      constant:0];

        horizontalCenterConstraint = [NSLayoutConstraint constraintWithItem:bannerView
                                                                  attribute:NSLayoutAttributeCenterX
                                                                  relatedBy:NSLayoutRelationEqual
                                                                     toItem:containerView
                                                                  attribute:NSLayoutAttributeCenterX
                                                                 multiplier:1
                                                                   constant:0];
        
        verticalCenterConstraint = [NSLayoutConstraint constraintWithItem:bannerView
                                                                attribute:NSLayoutAttributeCenterY
                                                                relatedBy:NSLayoutRelationEqual
                                                                   toItem:containerView
                                                                attribute:NSLayoutAttributeCenterY
                                                               multiplier:1
                                                                 constant:0];
    }
    
    NSDictionary *constraints = @{
        @"bottom_center": @[bottomConstraint, horizontalCenterConstraint],
        @"bottom_left": @[bottomConstraint, leftConstraint],
        @"bottom_right": @[bottomConstraint, rightConstraint],
        @"top_center": @[topConstraint, horizontalCenterConstraint],
        @"top_left": @[topConstraint, leftConstraint],
        @"top_right": @[topConstraint, rightConstraint],
        @"centered": @[horizontalCenterConstraint, verticalCenterConstraint],
        @"center_left": @[verticalCenterConstraint, leftConstraint],
        @"center_right": @[verticalCenterConstraint, rightConstraint]
    };
    
    return constraints[strPosition];
}

void SMAdsShowBanner(char* tagChr, int width, int height, char* position) {
    NSString *tag = [NSString stringWithUTF8String: tagChr];
    UIViewController *viewController = UnityGetGLViewController();
    NSString *positionAsString = [NSString stringWithUTF8String:position];
    
    SMBannerView *bannerView = [[SMBannerView alloc] init];
    bannerView.viewController = viewController;
    
    [viewController.view addSubview:bannerView];
    
    [bannerView setTranslatesAutoresizingMaskIntoConstraints:NO];
    
    [viewController.view addConstraints:getConstraintsForPosition(positionAsString, bannerView, viewController.view)];
    [bannerView addConstraints:getConstraintsForSizing(width, height, bannerView)];
    
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
