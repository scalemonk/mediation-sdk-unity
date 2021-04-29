//  AdsBindingInterstitialViewController.m
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#import "AdsBindingBannerViewController.h"

@interface AdsBindingBannerViewController ()

@end

@implementation AdsBindingBannerViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}


- (void)onBannerFail:(NSString *)tag {
    NSLog(@"Failed banner display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "FailedBannerDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onBannerCompleted:(NSString *)tag {
    NSLog(@"Completed banner display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "CompletedBannerDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}


@end
