# Changelog
All notable changes to this project will be documented in this file.

## [Unreleased]

## [1.25.1]
### Added
- Fixed typo in Analytics binding param

## [1.25.0]
### Added
- Update Android ScaleMonk version to 9.1.0

## [1.24.0]
### Added
- Update iOS ScaleMonk version to 6.1.0
- Added `IsBannerPresent` method

### Changed
- Analytics use native implementation now

## [1.23.0] - 2021-11-16
### Added
- Update iOS ScaleMonk version to 6.0.0

## [1.22.0] - 2021-11-09
### Changed
- Moved mock ads to editor folder
- Update Android Scalemonk version to 9.0.0

## [1.21.0] - 2021-10-28
### Added
- Added supoprt for Yandex in iOS
- Update iOS ScaleMonk version to 5.0.0

## [1.20.0] - 2021-10-06
### Added
- Added support for Maio in iOS
- Update iOS ScaleMonk version to 4.0.0

## [1.19.0] - 2021-10-06
### Added
- Added support for MyTarget in Android

### Changed
- Updated Android ScaleMonk version to 8.0.0

## [1.18.0] - 2021-09-27
### Changed
- Updated Android ScaleMonk version to 7.0.0

## [1.17.0] - 2021-09-24
### Added
- The binding for setCustomSegmentationTags

### Changed
- Updated iOS ScaleMonk version to 3.3.0

## [1.16.0] - 2021-09-15
### Changed
- Updated Android ScaleMonk version to 6.2.0

## [1.15.0] - 2021-09-08
### Changed
- Updated iOS ScaleMonk version to `3.2.0`

## [1.14.1] - 2021-09-07
### Fixed
- Fixed how we compare adnet id with strings.
- Set GoogleUtilitiesAppDelegateProxyEnabled to false when AdMob is included and enabled for iOS.

## [1.14.0] - 2021-08-25
### Changed
- Updated Android ScaleMonk version to 6.1.0

## [1.13.0] - 2021-08-11
### Changed
- Updated iOS ScaleMonk version to 3.1.0
- Fix paying user not being set after initialized in iOS

## [1.12.1] - 2021-08-19
### Changed
- Updated iOS ScaleMonk version to 3.0.1

## [1.12.0] - 2021-08-10
### Changed
- Updated iOS ScaleMonk version to 3.0.0
- Updated Android ScaleMonk version to 6.0.0

### Removed
- RTB option was removed from the ScaleMonk adapter selection for iOS. Included by default in iOS lib >= 2.3.0

## [1.11.0] - 2021-07-27
### Changed
- Updated Android ScaleMonk version to 5.0.0
- Added Super Banners for Android.

## [1.10.0] - 2021-07-20
### Changed
- Updated iOS ScaleMonk version to 2.2.0
- Use new GDPR consent handling compatible with v4.0.0 version of Scalemonk SDK.

## [1.9.0] - 2021-07-12
### Added
- Allow configure IAnalytics before and after the initialization
- Added `IsInterstitialReadyToShow` and `IsRewardedReadyToShow` methods

### Fixed
- Fix export package. Exclude exporter on export package process

### Changed
- Updated iOS ScaleMonk version to 2.1.0

### Changed
- Adds binding for `setIsApplicationChildDirected` with coppa enum on android

### Changed
- Adds binding for `setIsApplicationChildDirectedStatus` with coppa enum on ios

## [1.8.0] - 2021-06-15

### Changed
- Updated Android ScaleMonk version to 3.1.0

## [1.7.0] - 2021-06-15

### Changed
- Updated Android ScaleMonk version to 3.0.0

## [1.6.0] - 2021-05-27

### Added

- Added minify support for Android
- Added a new API to accept analytics other than the default one.

### Changed
- Updated Android ScaleMonk version to 2.0.0

## [1.5.0] - 2021-05-07

### Added

- Added banner support for iOS

## [1.4.0] - 2021-05-07

### Added

- Added banner support for Android

### Changed

- SDK Initialization method now receives a callback as parameter
- Updated iOS ScaleMonk version to 1.2.1
- Updated Android ScaleMonk version to 1.2.0 

## [1.3.1] - 2021-04-30

### Fixed

- Fixed callbacks events in Android not notifying

## [1.3.0] - 2021-04-27

### Changed

- Updated iOS ScaleMonk version to 1.2.0
- Updated Android ScaleMonk version to 1.1.0

### Fixed

- Fixed Android bindings

## [1.2.0] - 2021-04-06

### Added

- Added missing repositories

### Changed

- Repositories are now added if the adnet is selected
- Bindings are defined only for the selected platforms

### Fixed

- MockAd implementation

## [1.1.0] - 2021-03-25

### Added

- Added android building
- Added directives to postprocessor so the mediation can compile even if user doesn't have the unity modules
- Added an ad screen to see while it's running with the editor (available for 2018.4 and above). Press Q to close the ad
- If no android id is specified, dependencies are not added

## [1.0.0] - 2021-03-22

### Added

- Enabled adnet selection
- Added more adnets to select
- Can now check the versions online

### Changed

- Modified adnet XML schema

## [0.5.1] - 2021-03-19

### Fixed

- Fixed some exporting issues on iOS

## [0.5.0] - 2021-03-19

### Changed

- Update to iOS version `1.1.0`

## [0.4.2] - 2021-02-19

### Fixed

- Fixed podfile generation in Unity 2019

## [0.4.1] - 2021-02-10

### Added

- Added `isInterstitialReadyToShowWithTag` and `isRewardedReadyToShowWithTag` methods

### Changed

- Updated to iOS version 0.4.1

### Fixed

- Fixed AdMob swizzling

## [0.1.1.4] - 2021-02-10

### Added

- First version of ScaleMonkAds Unity.
- Enables display of Interstitial and Rewarded Video ads.
- Integrates with AdMob, Applovin, Facebook, IronSource, UnityAds and Vungle SDKs.
