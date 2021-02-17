//  ScaleMonkAdsiOSPostProcessor.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public class ScaleMonkAdsiOSPostProcessor
    {
        [PostProcessBuild(49)]
        public static void onPostProcessBuild(BuildTarget buildTarget, string targetPath)
        {
            configureSwiftBuild(targetPath);
            configureAdnets(buildTarget, targetPath);
            configureSKAdNetworks(buildTarget, targetPath);
        }


        private static void configureAdnets(BuildTarget buildTarget, string targetPath)
        {
#if UNITY_IOS
            if (buildTarget == BuildTarget.iOS)
            {
                var plistPath = Path.Combine(targetPath, "Info.plist");
                var infoPlist = new PlistDocument();
                infoPlist.ReadFromFile(plistPath);

                var adnetsConfigs = AdsProvidersHelper.ReadAdnetsConfigs();
                foreach (var adnet in adnetsConfigs)
                {
                    if (adnet.configs == null)
                    {
                        continue;
                    }

                    foreach (var config in adnet.configs)
                    {
                        if (config.platform != "ios")
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(config.value))
                        {
                            infoPlist.root.SetString(config.config, config.value);
                        }
                    }
                    
                    if (adnet.id == "Admob")
                    {
                        disableAdmobSwizzling(infoPlist);
                    }
                }

                infoPlist.WriteToFile(plistPath);
            }
#endif
        }
        
        static void disableAdmobSwizzling(PlistDocument plist)
        {
            plist.root.SetBoolean("GoogleUtilitiesAppDelegateProxyEnabled", false);
        }

        private static void configureSwiftBuild(string targetPath)
        {
#if UNITY_IOS
            var projPath = UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath(targetPath);
            var proj = new UnityEditor.iOS.Xcode.PBXProject();

            proj.ReadFromString(File.ReadAllText(projPath));

#if UNITY_2019_3_OR_NEWER
            var targetGuid = proj.GetUnityFrameworkTargetGuid();
#else
            var targetGuid = proj.TargetGuidByName ("Unity-iPhone");
#endif

            proj.SetBuildProperty(targetGuid, "GCC_ENABLE_OBJC_EXCEPTIONS", "YES");

            proj.SetBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");
#if UNITY_2019_3_OR_NEWER
            using (var sw = File.AppendText(targetPath + "/Podfile"))
            {
                sw.WriteLine("\ntarget 'Unity-iPhone' do\n  inherit! :search_paths\nend");

                sw.WriteLine("\n \npost_install do |installer|\n   installer.aggregate_targets.each do |aggregate_target|\n     aggregate_target.xcconfigs.each do |config_name, xcconfig|\n \n       remove([ 'FBSDKCoreKit'],\n              'libraries', config_name, xcconfig, aggregate_target)\n     end\n   end\n end\n # https://github.com/CocoaPods/CocoaPods/issues/7155\n def remove(dependencies, type, config_name, xcconfig, aggregate_target)\n   existing = xcconfig.other_linker_flags[type.to_sym]\n   modified = existing.subtract(dependencies)\n   xcconfig.other_linker_flags[type.to_sym] = modified\n   xcconfig_path = aggregate_target.xcconfig_path(config_name)\n   xcconfig.save_as(xcconfig_path)\nend");
            }

            var iphoneGuid = proj.GetUnityMainTargetGuid();
            proj.SetBuildProperty(iphoneGuid, "SWIFT_OBJC_BRIDGING_HEADER",
                "Libraries/ScaleMonk Ads/Plugins/iOS/Bridging-Header.h");
#else
            proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/ScaleMonk Ads/Plugins/iOS/Bridging-Header.h");
#endif
            
            using (var sw = File.AppendText(targetPath + "/Podfile"))
            {
                sw.WriteLine("\npost_install do |installer|\n   installer.pods_project.targets.each do |target|\n     target.build_configurations.each do |config|\n       if ['RxSwift', 'Willow'].include? target.name\n         config.build_settings['BUILD_LIBRARY_FOR_DISTRIBUTION'] = 'YES'\n       end\n     end\n   end\n end");
            }
            
            proj.SetBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
            proj.SetBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

            File.WriteAllText(projPath, proj.WriteToString());
#endif
        }

        static readonly string[] skadnetworks =
        {
            "ludvb6z3bs.skadnetwork",
            "prcb7njmu6.skadnetwork",
            "4fzdc2evr5.skadnetwork",
            "578prtvx9j.skadnetwork",
            "hs6bdukanm.skadnetwork",
            "9rd848q2bz.skadnetwork",
            "8s468mfl3y.skadnetwork",
            "ppxm28t8ap.skadnetwork",
            "t38b2kh725.skadnetwork",
            "5lm9lj6jb7.skadnetwork",
            "2u9pt9hc89.skadnetwork",
            "wg4vff78zm.skadnetwork",
            "yclnxrl5pm.skadnetwork",
            "24t9a8vw3c.skadnetwork",
            "424m5254lk.skadnetwork",
            "7ug5zh24hu.skadnetwork",
            "f38h382jlk.skadnetwork",
            "9t245vhmpl.skadnetwork",
            "wzmmz9fp6w.skadnetwork",
            "44jx6755aq.skadnetwork",
            "3rd42ekr43.skadnetwork",
            "v72qych5uu.skadnetwork",
            "glqzh8vgby.skadnetwork",
            "22mmun2rn5.skadnetwork",
            "ydx93a7ass.skadnetwork",
            "cstr6suwn9.skadnetwork",
            "f73kdq92p3.skadnetwork",
            "lr83yxwka7.skadnetwork",
            "mlmmfzh3r3.skadnetwork",
            "v79kvwwj4g.skadnetwork",
            "4468km3ulz.skadnetwork",
            "3sh42y64q3.skadnetwork",
            "488r3q3dtq.skadnetwork",
            "g28c52eehv.skadnetwork",
            "zmvfpc5aq8.skadnetwork",
            "av6w8kgt66.skadnetwork",
            "c6k4g5qg8m.skadnetwork",
            "44n7hlldy6.skadnetwork",
            "5a6flpkh64.skadnetwork",
            "tl55sbb4fm.skadnetwork",
            "3qy4746246.skadnetwork",
            "s39g8k73mm.skadnetwork",
            "4dzt52r2t5.skadnetwork",
            "bvpn9ufa9b.skadnetwork",
            "kbd757ywx3.skadnetwork",
            "238da6jt44.skadnetwork",
            "4pfyvq9l8r.skadnetwork",
            "m8dbw4sv7c.skadnetwork",
            "6xzpu9s2p8.skadnetwork",
            "v9wttpbfk9.skadnetwork",
            "n38lu8286q.skadnetwork",
            "w9q455wk68.skadnetwork",
            "su67r6k2v3.skadnetwork",
            "klf5c3l5u5.skadnetwork",
            "ecpz2srf59.skadnetwork",
            "gta9lk7p23.skadnetwork",
            "7953jerfzd.skadnetwork",
            "22mmun2rn5.skadnetwork",
            "238da6jt44.skadnetwork",
            "9g2aggbj52.skadnetwork",
            "uw77j35x4d.skadnetwork",
            "wg4vff78zm.skadnetwork",
            "cg4yq2srnc.skadnetwork",
            "ejvt5qm6ak.skadnetwork",
            "7rz58n8ntl.skadnetwork",
            "mls7yz5dvl.skadnetwork",
            "cj5566h2ga.skadnetwork",
            "97r2b46745.skadnetwork",
            "8m87ys6875.skadnetwork",
            "y45688jllp.skadnetwork",
            "hdw39hrw9y.skadnetwork",
            "dzg6xy7pwj.skadnetwork",
            "5l3tpt7t6e.skadnetwork",
            "252b5q8x7y.skadnetwork",
            "v4nxqhlyqp.skadnetwork",
            "n9x2a789qt.skadnetwork",
            "nu4557a4je.skadnetwork",
            "p78axxw29g.skadnetwork",
            "mtkv5xtk9e.skadnetwork",
            "ggvn48r87g.skadnetwork",
            "eh6m2bh4zr.skadnetwork",
            "737z793b9f.skadnetwork",
            "523jb4fst2.skadnetwork",
            "52fl2v3hgk.skadnetwork",
            "9yg77x724h.skadnetwork",
            "gvmwg8q7h5.skadnetwork",
            "n66cz3y3bx.skadnetwork",
            "nzq8sh4pbs.skadnetwork",
            "pu4na253f3.skadnetwork",
            "u679fj5vs4.skadnetwork",
            "xy9t38ct57.skadnetwork",
            "yrqqpx2mcb.skadnetwork",
            "z4gj7hsk7h.skadnetwork",
        };

        private static void configureSKAdNetworks(BuildTarget buildTarget, string targetPath)
        {
#if UNITY_IOS
            var plistPath = targetPath + "/Info.plist";
            var plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));
            var arr = plist.root.CreateArray("SKAdNetworkItems");
            foreach (var id in skadnetworks)
            {
                var dic = arr.AddDict();
                dic.SetString("SKAdNetworkIdentifier", id);
            }

            File.WriteAllText(plistPath, plist.WriteToString());
            UnityEngine.Debug.Log("Add SKAdNetworkItems PostProcessor done");
#endif
        }
    }
}