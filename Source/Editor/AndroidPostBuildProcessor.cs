#if UNITY_ANDROID
using System.IO;
using ScaleMonk.Ads;
using UnityEditor.Android;
using UnityEngine;

namespace ScaleMonk_Ads.Editor
{
    public class AndroidPostBuildProcessor : IPostGenerateGradleAndroidProject
    {
        public int callbackOrder
        {
            get { return 999; }
        }

        void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path)
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