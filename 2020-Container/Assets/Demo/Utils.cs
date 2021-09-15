using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo
{
    static class Utils
    {
        public static string ToDebugString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return "{" + string.Join(",", dictionary.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }
        
        public static string ReadSegmentationTagsFromFile()
        {
            var segmentationTagsTextAsset = (TextAsset)Resources.Load("custom-segmentation-tags", typeof(TextAsset));

            return segmentationTagsTextAsset.text;
        }
    }
}