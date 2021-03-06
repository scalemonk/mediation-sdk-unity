//  AdsBindingVideoDelegateViewController.m
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#import "AdsBindingRewardedDelegateViewController.h"

@interface AdsBindingRewardedDelegateViewController ()

@end

@implementation AdsBindingRewardedDelegateViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}

- (void)onRewardedClick:(NSString *)tag {
    NSLog(@"Clicked rewarded at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "ClickedRewarded",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onRewardedFinishWithReward:(NSString *)tag {
    NSLog(@"Completed rewarded display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "CompletedRewardedDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onRewardedFinishWithNoReward:(NSString *)tag {
    NSLog(@"Failed rewarded display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "FailedRewardedDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onRewardedFail:(NSString *)tag {
    NSLog(@"Failed rewarded display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "FailedRewardedDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onRewardedViewStart:(NSString *)tag {
    NSLog(@"Started rewarded display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "StartedRewardedDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onRewardedReady {
    NSLog(@"Rewarded ready to be shown");
    UnitySendMessage("AdsMonoBehaviour", "RewardedReady", [@"" cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onRewardedNotReady {
    NSLog(@"Rewarded not ready to be shown");
    UnitySendMessage("AdsMonoBehaviour", "RewardedNotReady", [@"" cStringUsingEncoding:NSUTF8StringEncoding]);
}

@end
