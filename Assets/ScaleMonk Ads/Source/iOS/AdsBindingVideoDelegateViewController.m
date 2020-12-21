//  AdsBindingVideoDelegateViewController.m
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#import "AdsBindingVideoDelegateViewController.h"

@interface AdsBindingVideoDelegateViewController ()

@end

@implementation AdsBindingVideoDelegateViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}

- (void)onVideoAdClick:(NSString *)tag {
    NSLog(@"Clicked video at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "ClickedVideo",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onVideoAdFinishWithReward:(NSString *)tag {
    NSLog(@"Completed video display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "CompletedVideoDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onVideoAdFinishWithNoReward:(NSString *)tag {
    NSLog(@"Failed video display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "FailedVideoDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onVideoAdFail:(NSString *)tag {
    NSLog(@"Failed video display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "FailedVideoDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onVideoAdViewStart:(NSString *)tag {
    NSLog(@"Started video display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "StartedVideoDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onVideoAdReady {
    NSLog(@"Video ad ready to be shown");
    UnitySendMessage("AdsMonoBehaviour", "VideoReady", [@"" cStringUsingEncoding:NSUTF8StringEncoding]);
}

@end
