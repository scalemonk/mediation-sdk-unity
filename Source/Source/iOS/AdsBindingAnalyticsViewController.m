//  AdsBindingBannerViewController.m
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#import "AdsBindingAnalyticsViewController.h"

@interface AdsBindingAnalyticsViewController ()

@end

@implementation AdsBindingAnalyticsViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}

- (void)sendEvent:(NSString *)eventName
            withParams:(NSDictionary<NSString *, NSObject *> *)eventParams {
    NSLog(@"Event %@ received on iOS binding", eventName);
    NSString * eventAsString = createEventAsString(eventName, eventParams);
    
    UnitySendMessage("AdsMonoBehaviour", "SendEvent", strdup([eventAsString UTF8String]));
}

static NSString *createEventAsString(NSString *eventName, NSDictionary<NSString *,NSObject *> *eventParams) {
    NSError *error;
    NSArray *eventKeys = eventParams.allKeys;
    NSArray *eventValues = eventParams.allValues;
    
    NSDictionary *dict = [[NSDictionary alloc] initWithObjectsAndKeys:eventName, @"eventName", eventKeys, @"eventKeys",
                          eventValues, @"eventValues", nil];
        
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dict options:NSJSONWritingPrettyPrinted error:&error];
    if (error != nil) {
        NSLog(@"Cannot serialize event into json %@", error);
        return @"";
    }
    
    NSString* jsonDictionary = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
    return jsonDictionary;
}

@end
