#import "AdsBindingInterstitialViewController.h"

@interface AdsBindingInterstitialViewController ()

@end

@implementation AdsBindingInterstitialViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}

- (void)onCrossPromoInterstitialCached {
    NSLog(@"inter delegate onCrossPromoInterstitialCached");
}

- (void)onInterstitialClick:(NSString *)tag {
    NSLog(@"Clicked interstitial at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "ClickedInterstitial",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onInterstitialView:(NSString *)tag {
    NSLog(@"Completed interstitial display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "CompletedInterstitialDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onInterstitialFail:(NSString *)tag {
    NSLog(@"Failed interstitial display at tag %@", tag);
    UnitySendMessage("AdsMonoBehaviour", "FailedInterstitialDisplay",
                     [tag cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onInterstitialReady {
    NSLog(@"Interstitial ad ready to be shown");
    UnitySendMessage("AdsMonoBehaviour", "InterstitialReady", [@"" cStringUsingEncoding:NSUTF8StringEncoding]);
}

- (void)onInterstitialNotReady {
    NSLog(@"Interstitial not ready to be shown");
    UnitySendMessage("AdsMonoBehaviour", "InterstitialNotReady", [@"" cStringUsingEncoding:NSUTF8StringEncoding]);
}

@end
