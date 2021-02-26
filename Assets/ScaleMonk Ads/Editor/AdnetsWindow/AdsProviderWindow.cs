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
                            adnet.ios = true;
#if SCALEMONK_ANDROID
                            adnet.android = true;
#endif
                        }
                    }

                    if (GUILayout.Button("Deselect All"))
                    {
                        foreach (var adnet in adnetsConfigs)
                        {
                            adnet.ios = false;
                            adnet.android = false;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(5);

                if (adnetsConfigs != null)
                {
                    EditorGUILayout.LabelField("iOS", new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 12,
                        stretchHeight = true,
                        fixedHeight = 30,
                    });
                    GUILayout.Space(5);

                    foreach (var adnet in adnetsConfigs)
                    {
                        if (adnet.availableIos)
                        {
                            EditorGUILayout.BeginHorizontal();
                            adnet.ios = EditorGUILayout.Toggle(adnet.name, adnet.ios);

                            EditorGUILayout.EndHorizontal();

                            if (adnet.ios)
                            {
                                EditorGUILayout.BeginHorizontal();
                                GUILayout.Space(10);
                                adnet.iosVersion = EditorGUILayout.TextField("version", adnet.iosVersion);
                                EditorGUILayout.EndHorizontal();
                                
                                
                            }
                            
                            foreach (var config in adnet.configs)
                            {
                                if (config.platform == "ios")
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
                    }

                    GUILayout.Space(10);

#if SCALEMONK_ANDROID
                    EditorGUILayout.LabelField("Android", new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 12,
                        stretchHeight = true,
                        fixedHeight = 30,
                    });
                    GUILayout.Space(5);

                    foreach (var adnet in adnetsConfigs)
                    {
                        if (adnet.availableAndroid)
                        {
                            adnet.android = EditorGUILayout.Toggle(adnet.name, adnet.android);

                            if (adnet.android)
                            {
                                foreach (var config in adnet.configs)
                                {
                                    if (config.platform == "android")
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
                        }
                    }
#endif

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

        void OnLostFocus()
        {
            AdsProvidersHelper.SaveConfig(adnetsConfigs);
        }
    }
}