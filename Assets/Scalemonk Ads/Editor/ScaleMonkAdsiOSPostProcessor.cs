using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Scalemonk_Ads.Editor
{
    public class ScaleMonkAdsiOSPostProcessor
    {
        [PostProcessBuild]
        public static void onPostProcessBuild(BuildTarget buildTarget, string targetPath)
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
            
            
            proj.SetBuildProperty(targetGuid, "SWIFT_VERSION", "5.0");
#if !UNITY_2019_3_OR_NEWER
            //TODO check this
            proj.SetBuildProperty(targetGuid, "SWIFT_OBJC_BRIDGING_HEADER", "Assets/Scalemonk Ads/Plugins/iOS/Bridging-Header.h");
#endif
            proj.SetBuildProperty(targetGuid, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
            proj.SetBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");
#endif
            
            File.WriteAllText (projPath, proj.WriteToString ());
        }
    }
}