//  AdsBindingBannerViewController.m
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#import "AdsBindingAnalyticsViewController.h"
#import "TFGAnalytics.h"

@interface AdsBindingAnalyticsViewController ()

@end

@implementation AdsBindingAnalyticsViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}

- (void)sendEvent:(NSString *)eventName
            withParams:(NSDictionary<NSString *, NSObject *> *)eventParams {
    @try {
        NSLog(@"Event %@ received on iOS binding", eventName);

        NSArray *eventKeys = eventParams.allKeys;
        NSMutableArray* eventValues = [[NSMutableArray alloc] init];

        for(id key in eventParams)
            [eventValues addObject: [NSString stringWithFormat: @"%@", [eventParams objectForKey: key]]];
            
        [[TFGAnalytics sharedInstance] sendEvent:eventName params:  [[NSMutableDictionary alloc] initWithObjects:eventValues forKeys:eventKeys]];
    }
    @catch(id exception){
        NSLog(@"Cannot serialize event %@ with params %@ error is %@", eventName, eventParams, exception);
    }
}

@end
