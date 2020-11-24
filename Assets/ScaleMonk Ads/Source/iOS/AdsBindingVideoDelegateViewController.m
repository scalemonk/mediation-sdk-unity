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
@end
