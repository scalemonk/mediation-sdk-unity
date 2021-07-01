using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public static class ExportScaleMonk
    {
        public static void Export()
        {
            var exportedPackageAssetList = new List<string>();
            //Find all shaders that have "Surface" in their names and add them to the list
            Debug.Log("Generating Unity Version " + Config.Version);
            foreach (var guid in AssetDatabase.FindAssets("",
                new[] {"Assets/Scalemonk Ads", "Assets/ExternalDependencyManager"}))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                
                if (!(path.Contains("ExportScaleMonk") || path.Contains("ExportPackage")))
                {
                    Debug.Log("Including file: " + path);
                    exportedPackageAssetList.Add(path);
                }
                else
                {
                    Debug.Log("Ignoring file: " + path);
                }
            }

            //Export Shaders and Prefabs with their dependencies into a .unitypackage
            AssetDatabase.ExportPackage(exportedPackageAssetList.ToArray(),
                "ScaleMonk-" + Config.Version + ".unitypackage",
                ExportPackageOptions.Default);
        }
    }
}