//  AdnetXml.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System.Collections.Generic;

namespace ScaleMonk.Ads
{
    public class ScaleMonkXml
    {
        public ScaleMonkXml()
        {
            adnets = new List<AdnetXml>();
        }

        public string ios { get; set; }
        public string android { get; set; }
        public List<AdnetXml> adnets { get; set; }
    }
    
    public class AdnetXml
    {
        public string id { get; set; }
        public string name { get; set; }
        
        public bool ios { get; set; }
        public bool android { get; set; }
        public bool availableIos { get; set; }
        public bool availableAndroid { get; set; }
        
        public List<AdnetConfigXml> configs;
        
        public string iosVersion { get; set; }
        
        public string androidVersion { get; set; }

        public AdnetXml(string id, string name, bool ios, bool android, string iosVersion = null) :
            this(id, name, ios, android, new List<AdnetConfigXml>(), iosVersion) {}

        public AdnetXml(string id, string name, bool ios, bool android, List<AdnetConfigXml> configs, string iosVersion = null)
        {
            this.id = id;
            this.name = name;
            this.iosVersion = iosVersion;
            this.availableIos = ios;
            this.availableAndroid = android;
            this.android = false;
            this.ios = false;
            this.configs = configs;
        }
    }

    public class AdnetConfigXml
    {
        public string config { get; private set; }
        public string platform { get; private set; }
        public string name { get; private set; }
        public string value { get; set; }
        
        public AdnetConfigXml(string config, string platform, string name, string value)
        {
            this.config = config;
            this.platform = platform;
            this.name = name;
            this.value = value;
        }

        public string Hash()
        {
            return string.Format("{0}|{1}|{2}", config, platform, name);
        }
    }
}