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
    public class AboutEditorWindow : EditorWindow
    {
        [MenuItem("ScaleMonk/About ScaleMonk ...")]
        static void Init()
        {
            AboutEditorWindow window = ScriptableObject.CreateInstance<AboutEditorWindow>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 215, 215);
            window.Show();
        }

        private Texture2D m_Logo = null;

        void OnEnable()
        {
            m_Logo = (Texture2D) Resources.Load("Sprites/SM-logo", typeof(Texture2D));
        }

        void Awake()
        {
            titleContent = new GUIContent("About ScaleMonk");
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(210));

            GUI.DrawTexture(new Rect(10, 10, 200, 150), m_Logo);
            GUILayout.Space(170);

            EditorGUILayout.BeginHorizontal(GUILayout.Width(210));
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Scalemonk Version " + Config.Version, new GUIStyle(GUI.skin.label)
            {
                fontSize = 13,
                fontStyle = FontStyle.Bold,
                stretchHeight = true,
                fixedHeight = 30
            });

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal(GUILayout.Width(200));
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Ok!")) this.Close();
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}