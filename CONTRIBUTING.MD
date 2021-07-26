## Project version
This project has been built with Unity 2018.4. If you open it with a different Unity version, be careful about the `.meta` files. You should not push them unless you are in the same version.

## Run Tests
Go to `Window > General > Test Runner` and hit `Run All`

## Run Tests with code coverage
Enable code coverage from `Window > Windows > Code Coverage`. Go to `Window > General > Test Runner` and hit `Run All`

## Release

### Update the native libraries
Go to: Assets/ScaleMonk Ads/Editor/AdnetsWindow/AdsProvidersHelper.cs

```
   const string iosAdsVersion = "X.X.X";
   const string androidAdsVersion = "Y.Y.Y";
```
### Export
Go to Asset -> Export package

A pop up will appear with all files checked. 

You have to unselect:
- Plugins folder
- Scenes folder
- ScaleMonkDebugMenu.cs
- ScaleMonkAdsDependencies.xml (only available if you previously Saved the proyect) 

Click on Export

Save the unitypackage with the file format 'scalemonk-unity-${package.version}'. A file with name 'scalemonk-unity-x.x.x.unitypackage' will be created. 

Go to https://github.com/scalemonk/mediation-sdk-unity/releases

Draft a new release
- Tag version: ${package.version}
- Release tile: ${package.version}
- Description: ${changelog.version.entry}
- Attach the file 'scalemonk-unity-x.x.x.unitypackage'
- Click "Publish release"
