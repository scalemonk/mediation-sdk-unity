//  AdsProvidersHelper.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public class AdsProvidersHelper
    {
        const string iosAdsVersion = "1.0.0-alpha.5";

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
            XmlNodeList xmlNodeList = null;

            if (local)
            {
                var localDoc = new XmlDocument();
                localDoc.Load(GetAdnetsXmlPath());
                var localRoot = localDoc.DocumentElement;
                xmlNodeList = localRoot != null ? localRoot.SelectNodes("adnet") : null;
            }

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
                var ios = bool.Parse(node.Attributes["ios"] != null ? node.Attributes["ios"].Value ?? "false" : "false");
                var android = bool.Parse(node.Attributes["android"] != null ? node.Attributes["android"].Value ?? "false" : "false");

                var configs = node.SelectNodes("adnetConfig");
                var adnetConfigs = new List<AdnetConfigXml>();
                foreach (XmlNode config in configs)
                {
                    var configConfig = config.Attributes["config"].Value;
                    var configPlatform = config.Attributes["platform"].Value;
                    var configName = config.Attributes["name"].Value;
                    var configValue = config.Attributes["value"] != null ? config.Attributes["value"].Value ?? string.Empty : string.Empty;
                    adnetConfigs.Add(new AdnetConfigXml(configConfig, configPlatform, configName, configValue));
                }

                AdnetXml currentAdnet = new AdnetXml(id, name, ios, android);
                var localNode = xmlNodeList != null ? xmlNodeList.Cast<XmlNode>().FirstOrDefault(n => (n.Attributes["id"] != null ? n.Attributes["id"].Value : null) == id) : null;

                if (localNode != null)
                {
                    currentAdnet.android = bool.Parse(localNode.Attributes["android"] != null ? localNode.Attributes["android"].Value ?? "false" : "false");
                    currentAdnet.ios = bool.Parse(localNode.Attributes["ios"] != null ? localNode.Attributes["ios"].Value ?? "false" : "false");
                    currentAdnet.iosVersion = localNode.Attributes["iosVersion"] != null ? localNode.Attributes["iosVersion"].Value : string.Empty;
                    currentAdnet.androidVersion = localNode.Attributes["androidVersion"] != null ? localNode.Attributes["androidVersion"].Value : string.Empty;
                }

                var newConfigs = new List<AdnetConfigXml>();

                XmlNodeList savedConfigs = null;
                if (localNode != null)
                {
                    savedConfigs = localNode.SelectNodes("adnetConfig");
                }

                foreach (var newConfig in adnetConfigs)
                {
                    var curConfig = savedConfigs.Cast<XmlNode>().FirstOrDefault(c =>
                        (c.Attributes["config"] != null ? c.Attributes["config"].Value : null) == newConfig.config &&
                        (c.Attributes["platform"] != null ? c.Attributes["platform"].Value : null) == newConfig.platform);
                    if (curConfig != null)
                    {
                        newConfig.value = curConfig.Attributes["value"].Value;
                    }

                    newConfigs.Add(newConfig);
                }

                currentAdnet.configs = newConfigs;
                adnetsDict[id] = currentAdnet;
            }

            return adnetsDict.Values.ToList();
        }

        public static List<AdnetXml> ReadAdnetsConfigs()
        {
            var schemaPath = GetAdnetsXmlSchemaPath();
            var localPath = GetAdnetsXmlPath();
            var adnets = ReadAdnetsFromPath(new List<AdnetXml>(), schemaPath, File.Exists(localPath));
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
                adnetElement.SetAttribute("ios", adnet.ios.ToString());
                adnetElement.SetAttribute("iosVersion", adnet.iosVersion);
                adnetElement.SetAttribute("androidVerrsion", adnet.androidVersion);
                adnetElement.SetAttribute("android", adnet.android.ToString());

                if (adnet.configs != null && adnet.configs.Count > 0)
                {
                    foreach (var config in adnet.configs)
                    {
                        var configElement = doc.CreateElement("adnetConfig");
                        if (((adnet.availableIos && config.platform == "ios")
                             || (adnet.availableAndroid && config.platform == "android"))
                            && string.IsNullOrEmpty(config.value))
                        {
                            Debug.LogErrorFormat("Adnet {0} missing config {1} ({2})", adnet.name, config.name,
                                config.config);
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
            var doc = new XmlDocument();
            var xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var root = doc.DocumentElement;

            doc.InsertBefore(xmlDeclaration, root);
            var dependenciesElement = doc.CreateElement("dependencies");

            // UpdateAndroidDependencies(adnets, doc, dependenciesElement);
            UpdateIOSDependencies(adnets, doc, dependenciesElement);

            doc.AppendChild(dependenciesElement);

            var path = GetDependenciesPath();

            Debug.Log("Saving Android and Ios packages config to " + path);

            // Make file available to write to
            var pathFileAttributes = File.GetAttributes(path);
            File.SetAttributes(path, FileAttributes.Normal);

            // Update file
            doc.Save(path);

            // Revert path file attributes after writing
            File.SetAttributes(path, pathFileAttributes);
        }
        
        static void UpdateIOSDependencies(List<AdnetXml> adnets, XmlDocument doc, XmlElement dependenciesElement)
        {
            var iosPodsElement = doc.CreateElement("iosPods");

            var adsPod = doc.CreateElement("iosPod");
            adsPod.SetAttribute("name", "ScaleMonkAds");
            adsPod.SetAttribute("version", iosAdsVersion);

            adsPod.AppendChild(CreateSourcesElement(doc));

            iosPodsElement.AppendChild(adsPod);

            bool addedRenderer = false;
            bool addedMopub = false;
            foreach (var adnet in adnets)
            {
                if (!adnet.ios)
                {
                    continue;
                }

                var adnetPod = doc.CreateElement("iosPod");
                adnetPod.SetAttribute("name", string.Format("ScaleMonkAds-{0}", adnet.id));
                if (!string.IsNullOrEmpty(adnet.iosVersion))
                {
                    adnetPod.SetAttribute("version", adnet.iosVersion);    
                }
                adnetPod.AppendChild(CreateSourcesElement(doc));
                
                // TODO: set configs to info.plist

                iosPodsElement.AppendChild(adnetPod);
            }

            dependenciesElement.AppendChild(iosPodsElement);
        }

        static XmlNode CreateSourcesElement(XmlDocument doc)
        {
            var sourcesElement = doc.CreateElement("sources");
            var iosPodspecSource = doc.CreateElement("source");

            iosPodspecSource.InnerText = "https://github.com/scalemonk/ios-podspecs-framework";

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