//  AdnetXml.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System.Collections.Generic;

namespace ScaleMonk.Ads
{
    public class AdnetXml
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool enabled { get; set; }
        public List<AdnetConfigXml> configs;

        public AdnetXml(string id, string name, bool enabled) :
            this(id, name, enabled, new List<AdnetConfigXml>()) {}

        public AdnetXml(string id, string name, bool enabled, List<AdnetConfigXml> configs)
        {
            this.id = id;
            this.name = name;
            this.enabled = enabled;
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
