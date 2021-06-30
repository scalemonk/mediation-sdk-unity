//  ScaleMonkAdsiOSPostProcessor.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

#if UNITY_IOS
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
            ScaleMonkXml scaleMonkXml = AdsProvidersHelper.ReadAdnetsConfigs();

            if (string.IsNullOrEmpty(scaleMonkXml.ios))
                return;
            
            configureSwiftBuild(targetPath);
            configureAdnetsAndAppId(buildTarget, targetPath, scaleMonkXml);
            configureSKAdNetworks(buildTarget, targetPath);
        }


        private static void configureAdnetsAndAppId(BuildTarget buildTarget, string targetPath,
            ScaleMonkXml scaleMonkXml)
        {
            if (buildTarget == BuildTarget.iOS)
            {
                var plistPath = Path.Combine(targetPath, "Info.plist");
                var infoPlist = new PlistDocument();
                infoPlist.ReadFromFile(plistPath);
                
                infoPlist.root.SetString("ScaleMonkApplicationId", scaleMonkXml.ios);

                var adnetsConfigs = scaleMonkXml.adnets;
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
                            infoPlist.root.SetString(config.config, config.value.Trim());
                        }
                    }
                    
                    if (adnet.id == "Admob")
                    {
                        disableAdmobSwizzling(infoPlist);
                    }
                }

                infoPlist.WriteToFile(plistPath);
            }
        }
        
        static void disableAdmobSwizzling(PlistDocument plist)
        {
            plist.root.SetBoolean("GoogleUtilitiesAppDelegateProxyEnabled", false);
        }

        private static void configureSwiftBuild(string targetPath)
        {
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
            string podFilePath = Path.Combine(targetPath, "Podfile");
            string podFileContents = File.ReadAllText(podFilePath);
            string targetUnityiPhone = "\ntarget 'Unity-iPhone' do";
            string inheritSearchPaths = "\n inherit! :search_paths";

            if (podFileContents.Contains("Unity-iPhone"))
            {
                podFileContents =
                    podFileContents.Replace(targetUnityiPhone, targetUnityiPhone + inheritSearchPaths);
                File.WriteAllText(podFilePath, podFileContents);
            }
            else
            {
                File.AppendAllText(podFilePath, targetUnityiPhone + inheritSearchPaths + "\nend");
            }

            File.AppendAllText(podFilePath,
                "\npost_install do |installer|\n   installer.pods_project.targets.each do |target|\n     target.build_configurations.each do |config|\n       if ['RxSwift', 'Willow'].include? target.name\n         config.build_settings['BUILD_LIBRARY_FOR_DISTRIBUTION'] = 'YES'\n       end\n     end\n   end\n end");

            var iphoneGuid = proj.GetUnityMainTargetGuid();
            proj.SetBuildProperty(iphoneGuid, "SWIFT_OBJC_BRIDGING_HEADER",
                "Libraries/ScaleMonk Ads/Plugins/iOS/SMAds-Bridging-Header.h");

            podFileContents = podFileContents.Replace("use_frameworks!", "");
#else
            proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Libraries/ScaleMonk Ads/Plugins/iOS/SMAds-Bridging-Header.h");

            using (var sw = File.AppendText(targetPath + "/Podfile"))
            {
                sw.WriteLine("\npost_install do |installer|\n   installer.pods_project.targets.each do |target|\n     target.build_configurations.each do |config|\n       if ['RxSwift', 'Willow'].include? target.name\n         config.build_settings['BUILD_LIBRARY_FOR_DISTRIBUTION'] = 'YES'\n       end\n     end\n   end\n end");
            }
#endif
            
            
            
            proj.SetBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
            proj.SetBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

            File.WriteAllText(projPath, proj.WriteToString());
        }

        static readonly string[] skadnetworks =
        {
            "22mmun2rn5.skadnetwork",
            "238da6jt44.skadnetwork",
            "24t9a8vw3c.skadnetwork",
            "252b5q8x7y.skadnetwork",
            "275upjj5gd.skadnetwork",
            "2u9pt9hc89.skadnetwork",
            "3qy4746246.skadnetwork",
            "3rd42ekr43.skadnetwork",
            "3sh42y64q3.skadnetwork",
            "424m5254lk.skadnetwork",
            "4468km3ulz.skadnetwork",
            "44jx6755aq.skadnetwork",
            "44n7hlldy6.skadnetwork",
            "488r3q3dtq.skadnetwork",
            "4dzt52r2t5.skadnetwork",
            "4fzdc2evr5.skadnetwork",
            "4pfyvq9l8r.skadnetwork",
            "4w7y6s5ca2.skadnetwork",
            "523jb4fst2.skadnetwork",
            "52fl2v3hgk.skadnetwork",
            "578prtvx9j.skadnetwork",
            "5a6flpkh64.skadnetwork",
            "5l3tpt7t6e.skadnetwork",
            "5lm9lj6jb7.skadnetwork",
            "5tjdwbrq8w.skadnetwork",
            "6g9af3uyq4.skadnetwork",
            "6xzpu9s2p8.skadnetwork",
            "737z793b9f.skadnetwork",
            "7953jerfzd.skadnetwork",
            "7rz58n8ntl.skadnetwork",
            "7ug5zh24hu.skadnetwork",
            "8m87ys6875.skadnetwork",
            "8s468mfl3y.skadnetwork",
            "97r2b46745.skadnetwork",
            "9g2aggbj52.skadnetwork",
            "9nlqeag3gk.skadnetwork",
            "9rd848q2bz.skadnetwork",
            "9t245vhmpl.skadnetwork",
            "9yg77x724h.skadnetwork",
            "av6w8kgt66.skadnetwork",
            "bvpn9ufa9b.skadnetwork",
            "c6k4g5qg8m.skadnetwork",
            "cg4yq2srnc.skadnetwork",
            "cj5566h2ga.skadnetwork",
            "cstr6suwn9.skadnetwork",
            "dzg6xy7pwj.skadnetwork",
            "ecpz2srf59.skadnetwork",
            "eh6m2bh4zr.skadnetwork",
            "ejvt5qm6ak.skadnetwork",
            "f38h382jlk.skadnetwork",
            "f73kdq92p3.skadnetwork",
            "feyaarzu9v.skadnetwork",
            "g28c52eehv.skadnetwork",
            "ggvn48r87g.skadnetwork",
            "glqzh8vgby.skadnetwork",
            "gta9lk7p23.skadnetwork",
            "gvmwg8q7h5.skadnetwork",
            "hdw39hrw9y.skadnetwork",
            "hs6bdukanm.skadnetwork",
            "kbd757ywx3.skadnetwork",
            "klf5c3l5u5.skadnetwork",
            "lr83yxwka7.skadnetwork",
            "ludvb6z3bs.skadnetwork",
            "m8dbw4sv7c.skadnetwork",
            "mlmmfzh3r3.skadnetwork",
            "mls7yz5dvl.skadnetwork",
            "mtkv5xtk9e.skadnetwork",
            "n38lu8286q.skadnetwork",
            "n66cz3y3bx.skadnetwork",
            "n9x2a789qt.skadnetwork",
            "nu4557a4je.skadnetwork",
            "nzq8sh4pbs.skadnetwork",
            "p78axxw29g.skadnetwork",
            "ppxm28t8ap.skadnetwork",
            "prcb7njmu6.skadnetwork",
            "pu4na253f3.skadnetwork",
            "r26jy69rpl.skadnetwork",
            "rx5hdcabgc.skadnetwork",
            "s39g8k73mm.skadnetwork",
            "su67r6k2v3.skadnetwork",
            "t38b2kh725.skadnetwork",
            "tl55sbb4fm.skadnetwork",
            "u679fj5vs4.skadnetwork",
            "uw77j35x4d.skadnetwork",
            "v4nxqhlyqp.skadnetwork",
            "v72qych5uu.skadnetwork",
            "v79kvwwj4g.skadnetwork",
            "v9wttpbfk9.skadnetwork",
            "w9q455wk68.skadnetwork",
            "wg4vff78zm.skadnetwork",
            "wzmmz9fp6w.skadnetwork",
            "x44k69ngh6.skadnetwork",
            "xy9t38ct57.skadnetwork",
            "y45688jllp.skadnetwork",
            "yclnxrl5pm.skadnetwork",
            "ydx93a7ass.skadnetwork",
            "yrqqpx2mcb.skadnetwork",
            "z4gj7hsk7h.skadnetwork",
            "zmvfpc5aq8.skadnetwork",
            "2fnua5tdw4.skadnetwork",
            "n6fk4nfna4.skadnetwork",
            "e5fvkxwrpn.skadnetwork",
            "mp6xlyr22a.skadnetwork",
            "32z4fx6l9h.skadnetwork",
            "f7s53z58qe.skadnetwork"            
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
#endif