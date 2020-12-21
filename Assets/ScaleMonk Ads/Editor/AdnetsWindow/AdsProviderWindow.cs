//  AdsProviderWindow.cs
//
//  © 2020 ScaleMonk, Inc. All Rights Reserved.
// Licensed under the ScaleMonk SDK License Agreement
// https://www.scalemonk.com/legal/en-US/mediation-license-agreement/index.html 
//

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScaleMonk.Ads
{
    public class AdsProvidersWindow : EditorWindow
    {
        Vector2 scrollPos;
        static List<AdnetXml> adnetsConfigs;

        [MenuItem("ScaleMonk/Adnets Selection", false, 0)]
        static void Display()
        {
            GetWindow<AdsProvidersWindow>(typeof(SceneView));
        }

        void Awake()
        {
            titleContent = new GUIContent("Adnets");
        }

        void OnFocus()
        {
            adnetsConfigs = AdsProvidersHelper.ReadAdnetsConfigs();
        }

        void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width),
                GUILayout.Height(position.height));
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var labelStyle = new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 14,
                        stretchHeight = true,
                        fixedHeight = 30,
                    };

                    EditorGUILayout.LabelField("Adnets configurations", labelStyle);
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Select All"))
                    {
                        foreach (var adnet in adnetsConfigs)
                        {
                            adnet.enabled = true;
                        }
                    }

                    if (GUILayout.Button("Deselect All"))
                    {
                        foreach (var adnet in adnetsConfigs)
                        {
                            adnet.enabled = false;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);


                if (adnetsConfigs != null)
                {
                    foreach (var adnet in adnetsConfigs)
                    {
                        adnet.enabled = EditorGUILayout.Toggle(adnet.name, adnet.enabled);

                        if (adnet.enabled)
                        {
                            foreach (var config in adnet.configs)
                            {
                                EditorGUILayout.BeginHorizontal();
                                {
                                    GUILayout.Space(10);
                                    config.value = EditorGUILayout.TextField(config.name, config.value);
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                    }

                    GUILayout.Space(20);

                    if (GUILayout.Button("Save"))
                    {
                        Debug.Log("Saving config");
                        AdsProvidersHelper.SaveConfig(adnetsConfigs);
                    }
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
