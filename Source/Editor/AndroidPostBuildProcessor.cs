#if UNITY_ANDROID
using System.IO;
using ScaleMonk.Ads;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

#if !UNITY_2017
using UnityEditor.Android;
#endif

namespace ScaleMonk_Ads.Editor
{
#if !UNITY_2017
    public class AndroidPostBuildProcessor : IPostGenerateGradleAndroidProject
#else
    public class AndroidPostBuildProcessor : IPostprocessBuild
#endif
    {
        public int callbackOrder
        {
            get { return 999; }
        }

#if UNITY_2017
        public void OnPostprocessBuild(BuildTarget target, string path)
        {
            GenerateGradleAndroidProject(path);
        }
#else
        void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
        {
            GenerateGradleAndroidProject(path);
        }
#endif

        private static void GenerateGradleAndroidProject(string path)
        {
            ScaleMonkXml scaleMonkXml = AdsProvidersHelper.ReadAdnetsConfigs();
            if (string.IsNullOrEmpty(scaleMonkXml.android))
                return;
            Debug.Log("OnPostGenerateGradleAndroidProject. Path: " + path);
            string gradlePropertiesFile = path + "/../gradle.properties";

            if (File.Exists(gradlePropertiesFile))
            {
                File.Delete(gradlePropertiesFile);
            }

            StreamWriter writer = File.CreateText(gradlePropertiesFile);
            writer.WriteLine("org.gradle.jvmargs=-Xmx4096M");
            writer.WriteLine("android.useAndroidX=true");
            writer.WriteLine("android.enableJetifier=true");
            writer.WriteLine("org.gradle.parallel=true");
#if UNITY_2019_1_OR_NEWER
            writer.WriteLine("unityStreamingAssets=.unity3d**STREAMING_ASSETS**");
#endif
            writer.Flush();
            writer.Close();
        }
    }
}
#endif