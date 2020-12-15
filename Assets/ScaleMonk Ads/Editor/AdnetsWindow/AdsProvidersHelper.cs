using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public class AdsProvidersHelper
    {
        const string iosAdsVersion = "0.1.0";

        public static string GetAdnetsXmlPath()
        {
            return Path.Combine(Application.dataPath, "../Packages/adnets.xml");
        }

        public static string GetAdnetsXmlSchemaPath()
        {
            return Path.Combine(GetLibPath(), "Editor/AdnetsSchema.xml");
        }

        public static string GetDependenciesPath()
        {
            return Path.Combine(GetLibPath(), "Editor/ScaleMonkAdsDependencies.xml");
        }

        static List<AdnetXml> ReadAdnetsFromPath(List<AdnetXml> adnetsBase, string path, bool local = false)
        {
            var adnetsDict = new Dictionary<string, AdnetXml>();
            foreach (var adnetBase in adnetsBase)
            {
                adnetsDict[adnetBase.id] = adnetBase;
            }

            var doc = new XmlDocument();
            doc.Load(path);

            var root = doc.DocumentElement;

            if (root == null)
            {
                return new List<AdnetXml>();
            }

            var nodes = root.SelectNodes("adnet");
            foreach (XmlNode node in nodes)
            {
                var id = node.Attributes["id"].Value;
                var name = node.Attributes["name"].Value;
                var enabled = local && bool.Parse(node.Attributes["enabled"].Value);

                var configs = node.SelectNodes("adnetConfig");
                var adnetConfigs = new List<AdnetConfigXml>();
                foreach (XmlNode config in configs)
                {
                    var configConfig = config.Attributes["config"].Value;
                    var configPlatform = config.Attributes["platform"].Value;
                    var configName = config.Attributes["name"].Value;
                    var configValue = local ? config.Attributes["value"].Value : string.Empty;
                    adnetConfigs.Add(new AdnetConfigXml(configConfig, configPlatform, configName, configValue));
                }

                AdnetXml currentAdnet;
                adnetsDict.TryGetValue(id, out currentAdnet);

                if (currentAdnet == null)
                {
                    if (local)
                    {
                        // ignore local dependencies not found in AdnetsSchema.xml
                        continue;
                    }

                    // add new adnet
                    adnetsDict[id] = new AdnetXml(id, name, enabled, adnetConfigs);
                }
                else
                {
                    // increment existing adnet
                    currentAdnet.id = id;
                    currentAdnet.name = name;
                    currentAdnet.enabled = enabled;

                    var newConfigs = new List<AdnetConfigXml>();

                    foreach (var newConfig in adnetConfigs)
                    {
                        var curConfig = currentAdnet.configs.Find(c => c.Hash() == newConfig.Hash());
                        if (curConfig != null)
                        {
                            curConfig.value = newConfig.value;
                            newConfigs.Add(curConfig);
                        }
                    }

                    currentAdnet.configs = newConfigs;
                    adnetsDict[id] = currentAdnet;
                }
            }

            return adnetsDict.Values.ToList();
        }

        public static List<AdnetXml> ReadAdnetsConfigs()
        {
            var schemaPath = GetAdnetsXmlSchemaPath();
            var localPath = GetAdnetsXmlPath();
            var adnets = ReadAdnetsFromPath(new List<AdnetXml>(), schemaPath);

            if (File.Exists(localPath))
            {
                adnets = ReadAdnetsFromPath(adnets, localPath, true);
            }

            return adnets;
        }

        public static void SaveConfig(List<AdnetXml> adnets)
        {
            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = doc.DocumentElement;

            doc.InsertBefore(xmlDeclaration, root);

            var adnetsElement = doc.CreateElement("adnets");
            foreach (var adnet in adnets)
            {

                var adnetElement = doc.CreateElement("adnet");
                adnetElement.SetAttribute("id", adnet.id);
                adnetElement.SetAttribute("name", adnet.name);
                adnetElement.SetAttribute("enabled", adnet.enabled.ToString());

                if (adnet.configs != null && adnet.configs.Count > 0)
                {
                    foreach (var config in adnet.configs)
                    {
                        var configElement = doc.CreateElement("adnetConfig");
                        if (adnet.enabled && string.IsNullOrEmpty(config.value))
                        {
                            Debug.LogErrorFormat("Adnet {0} missing config {1} ({2})", adnet.name, config.name, config.config);
                            return;
                        }

                        configElement.SetAttribute("config", config.config);
                        configElement.SetAttribute("platform", config.platform);
                        configElement.SetAttribute("name", config.name);
                        configElement.SetAttribute("value", config.value);

                        adnetElement.AppendChild(configElement);
                    }
                }

                adnetsElement.AppendChild(adnetElement);
            }

            doc.AppendChild(adnetsElement);

            var path = GetAdnetsXmlPath();
            Debug.Log("Saving config to " + path);
            doc.Save(path);

            UpdateNativeDependencies(adnets);
        }

        static void UpdateNativeDependencies(List<AdnetXml> adnets)
        {
            UpdateIOSDependencies(adnets);
        }

        static void UpdateIOSDependencies(List<AdnetXml> adnets)
        {
            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = doc.DocumentElement;

            doc.InsertBefore(xmlDeclaration, root);

            var dependenciesElement = doc.CreateElement("dependencies");
            var iosPodsElement = doc.CreateElement("iosPods");

            var adsPod = doc.CreateElement("iosPod");
            adsPod.SetAttribute("name", "ScaleMonkAds");
            adsPod.SetAttribute("version", iosAdsVersion);

            adsPod.AppendChild(CreateSourcesElement(doc));

            iosPodsElement.AppendChild(adsPod);

            foreach (var adnet in adnets)
            {
                if (!adnet.enabled)
                {
                    continue;
                }

                var adnetPod = doc.CreateElement("iosPod");
                adnetPod.SetAttribute("name", string.Format("ScaleMonkAds/Provider-{0}", adnet.id));
                adnetPod.AppendChild(CreateSourcesElement(doc));

                // TODO: set configs to info.plist

                iosPodsElement.AppendChild(adnetPod);
            }

            dependenciesElement.AppendChild(iosPodsElement);
            doc.AppendChild(dependenciesElement);

            var path = GetDependenciesPath();
            Debug.Log("Saving iOS pods config to " + path);

            // Make file available to write to
            var pathFileAttributes = File.GetAttributes(path);
            File.SetAttributes(path, FileAttributes.Normal);

            // Update file
            doc.Save(path);

            // Revert path file attributes after writing
            File.SetAttributes(path, pathFileAttributes);
        }

        static XmlNode CreateSourcesElement(XmlDocument doc)
        {
            var sourcesElement = doc.CreateElement("sources");
            var iosPodspecSource = doc.CreateElement("source");

            iosPodspecSource.InnerText = "git@github.com:scalemonk/ios-podspecs.git";

            sourcesElement.AppendChild(iosPodspecSource);

            return sourcesElement;
        }

        static string GetLibPath()
        {
            var localPath = Path.Combine(Application.dataPath, "ScaleMonk Ads/");
            var installedPath = Path.Combine(Application.dataPath, "ScaleMonk Ads/");

            if (Directory.Exists(installedPath))
            {
                return installedPath;
            }

            return localPath;
        }
    }
}
