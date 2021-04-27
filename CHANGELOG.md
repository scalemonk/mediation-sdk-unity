# Changelog
All notable changes to this project will be documented in this file.

## [Unreleased]

### Added

- Added banner support for Android

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
