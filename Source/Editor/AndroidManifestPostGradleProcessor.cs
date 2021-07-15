#if UNITY_ANDROID
using System.IO;
using System.Text;
using System.Xml;
using ScaleMonk.Ads;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

#if !UNITY_2017
using UnityEditor.Android;
#endif

namespace ScaleMonk_Ads.Editor
{
#if UNITY_2017
    public class AndroidManifestPostGradleProcessor : IPostprocessBuild
#else
    public class AndroidManifestPostGradleProcessor : IPostGenerateGradleAndroidProject
#endif
    {
        public readonly string AppIdKey = "com.scalemonk.libs.ads.applicationId";

#if UNITY_2017
        public void OnPostprocessBuild(BuildTarget target, string path)
        {
            GenerateAndroidManifest(path);
        }
#else
        public void OnPostGenerateGradleAndroidProject(string basePath)
        {
            GenerateAndroidManifest(basePath);
        }
#endif

        private void GenerateAndroidManifest(string basePath)
        {
            // If needed, add condition checks on whether you need to run the modification routine.
            // For example, specific configuration/app options enabled
            ScaleMonkXml scaleMonkXml = AdsProvidersHelper.ReadAdnetsConfigs();
            if (string.IsNullOrEmpty(scaleMonkXml.android))
                return;

            var androidManifest = new AndroidManifest(GetManifestPath(basePath));
            // androidManifest.SetApplicationTheme(ThemeName);
            Debug.Log("OnPostGenerateGradleAndroidProject. App id: " + scaleMonkXml.android);
            androidManifest.AddMetadataElement(AppIdKey, scaleMonkXml.android.Trim());
            // Add your XML manipulation routines

            foreach (var adnet in scaleMonkXml.adnets)
            {
                if (adnet.configs == null || !adnet.android)
                {
                    continue;
                }

                foreach (var config in adnet.configs)
                {
                    if (config.platform != "android")
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(config.value))
                    {
                        androidManifest.AddMetadataElement(config.config, config.value.Trim());
                    }
                }
            }

            androidManifest.Save();
        }

        public int callbackOrder
        {
            get { return 1; }
        }

        private string _manifestFilePath;

        private string GetManifestPath(string basePath)
        {
            if (string.IsNullOrEmpty(_manifestFilePath))
            {
                var pathBuilder = new StringBuilder(basePath);
                pathBuilder.Append(Path.DirectorySeparatorChar).Append("src");
                pathBuilder.Append(Path.DirectorySeparatorChar).Append("main");
                pathBuilder.Append(Path.DirectorySeparatorChar).Append("AndroidManifest.xml");
                _manifestFilePath = pathBuilder.ToString();
            }

            return _manifestFilePath;
        }
    }


    internal class AndroidXmlDocument : XmlDocument
    {
        private string m_Path;
        protected XmlNamespaceManager nsMgr;
        public readonly string AndroidXmlNamespace = "http://schemas.android.com/apk/res/android";

        public AndroidXmlDocument(string path)
        {
            m_Path = path;
            using (var reader = new XmlTextReader(m_Path))
            {
                reader.Read();
                Load(reader);
            }

            nsMgr = new XmlNamespaceManager(NameTable);
            nsMgr.AddNamespace("android", AndroidXmlNamespace);
        }

        public string Save()
        {
            return SaveAs(m_Path);
        }

        public string SaveAs(string path)
        {
            using (var writer = new XmlTextWriter(path, new UTF8Encoding(false)))
            {
                writer.Formatting = Formatting.Indented;
                Save(writer);
            }

            return path;
        }
    }


    internal class AndroidManifest : AndroidXmlDocument
    {
        private readonly XmlElement ApplicationElement;

        public AndroidManifest(string path) : base(path)
        {
            ApplicationElement = SelectSingleNode("/manifest/application") as XmlElement;
        }

        private XmlAttribute CreateAndroidAttribute(string key, string value)
        {
            XmlAttribute attr = CreateAttribute("android", key, AndroidXmlNamespace);
            attr.Value = value;
            return attr;
        }

        private XmlElement CreateAndroidElement(string key)
        {
            XmlElement elem = CreateElement(key, "");
            return elem;
        }

        internal void AddMetadataElement(string key, string value)
        {
            XmlElement metadataElem = CreateAndroidElement("meta-data");
            ApplicationElement.AppendChild(metadataElem);
            metadataElem.Attributes.Append(CreateAndroidAttribute("name", key));
            metadataElem.Attributes.Append(CreateAndroidAttribute("value", value));
        }
    }
}
#endif