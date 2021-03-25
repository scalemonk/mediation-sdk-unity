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
        static ScaleMonkXml scaleMonkConfig;

        [MenuItem("ScaleMonk/Application Configuration", false, 0)]
        static void Display()
        {
            GetWindow<AdsProvidersWindow>(typeof(SceneView));
        }

        void Awake()
        {
            titleContent = new GUIContent("Scalemonk Configuration");
        }

        void OnFocus()
        {
            scaleMonkConfig = AdsProvidersHelper.ReadAdnetsConfigs();
        }

        void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width),
                GUILayout.Height(position.height));
            {
                EditorGUILayout.BeginHorizontal(GUILayout.Width(500));
                var labelStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 14,
                    stretchHeight = true,
                    fixedHeight = 30,
                };

                EditorGUILayout.BeginVertical();
                EditorGUILayout.LabelField("ScaleMonk application configurations", labelStyle);
                GUILayout.Space(15);
                scaleMonkConfig.ios = EditorGUILayout.TextField("iOS application ID", scaleMonkConfig.ios);


                GUILayout.Space(10);
                scaleMonkConfig.android = EditorGUILayout.TextField("Android application ID", scaleMonkConfig.android);
                GUILayout.Space(20);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Adnets configurations", labelStyle);
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Select All"))
                {
                    foreach (var adnet in scaleMonkConfig.adnets)
                    {
                        adnet.ios = true;
                        adnet.android = true;
                    }
                }

                if (GUILayout.Button("Deselect All"))
                {
                    foreach (var adnet in scaleMonkConfig.adnets)
                    {
                        adnet.ios = false;
                        adnet.android = false;
                    }
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(5);

                if (scaleMonkConfig != null)
                {
                    EditorGUILayout.LabelField("iOS", new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 12,
                        stretchHeight = true,
                        fixedHeight = 30,
                    });
                    GUILayout.Space(5);

                    foreach (var adnet in scaleMonkConfig.adnets)
                    {
                        if (adnet.availableIos)
                        {
                            EditorGUILayout.BeginHorizontal();
                            adnet.ios = EditorGUILayout.Toggle(adnet.name, adnet.ios);

                            EditorGUILayout.EndHorizontal();

                            if (adnet.ios)
                            {
                                EditorGUILayout.BeginHorizontal(GUILayout.Width(500));
                                GUILayout.Space(10);
                                adnet.iosVersion = EditorGUILayout.TextField("version", adnet.iosVersion);
                                if (GUILayout.Button("Check available versions"))
                                {
                                    Help.BrowseURL(
                                        "https://github.com/scalemonk/ios-podspecs-framework/tree/master/Specs/ScaleMonkAds-" +
                                        adnet.id);
                                }

                                EditorGUILayout.EndHorizontal();
                            }

                            foreach (var config in adnet.configs)
                            {
                                if (config.platform == "ios")
                                {
                                    EditorGUILayout.BeginHorizontal(GUILayout.Width(500));
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

                    EditorGUILayout.LabelField("Android", new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 12,
                        stretchHeight = true,
                        fixedHeight = 30,
                    });
                    GUILayout.Space(5);

                    var anyAndroidNet = false;
                    foreach (var adnet in scaleMonkConfig.adnets)
                    {
                        if (adnet.availableAndroid)
                        {
                            anyAndroidNet = true;
                            EditorGUILayout.BeginHorizontal();
                            adnet.android = EditorGUILayout.Toggle(adnet.name, adnet.android);

                            EditorGUILayout.EndHorizontal();

                            if (adnet.android)
                            {
                                EditorGUILayout.BeginHorizontal(GUILayout.Width(500));
                                GUILayout.Space(10);
                                adnet.androidVersion = EditorGUILayout.TextField("version", adnet.androidVersion);
                                if (GUILayout.Button("Check available versions"))
                                {
                                    Help.BrowseURL(
                                        "https://scalemonk.jfrog.io/artifactory/scalemonk-gradle-prod/com/scalemonk/libs/ads-" +
                                        adnet.id.ToLower());
                                }

                                EditorGUILayout.EndHorizontal();
                            }

                            foreach (var config in adnet.configs)
                            {
                                if (config.platform == "android")
                                {
                                    EditorGUILayout.BeginHorizontal(GUILayout.Width(500));
                                    {
                                        GUILayout.Space(10);
                                        config.value = EditorGUILayout.TextField(config.name, config.value);
                                    }
                                    EditorGUILayout.EndHorizontal();
                                }
                            }
                        }
                    }

                    if (!anyAndroidNet)
                    {
                        EditorGUILayout.LabelField("Coming Soon!", new GUIStyle(GUI.skin.label)
                        {
                            fontSize = 16,
                            stretchHeight = true,
                            fixedHeight = 30,
                        });
                    }

                    GUILayout.Space(10);

                    GUILayout.Space(20);

                    EditorGUILayout.BeginHorizontal(GUILayout.Width(500));
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Save", GUILayout.Width(300)))
                    {
                        Debug.Log("Saving config");
                        AdsProvidersHelper.SaveConfig(scaleMonkConfig);
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
        }

        void OnLostFocus()
        {
            AdsProvidersHelper.SaveConfig(scaleMonkConfig);
        }
    }
}