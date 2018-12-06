﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using Jint;
using UnityEditor;
using UnityEngine;

namespace JUK
{
    public class JUKConfigEditor : EditorWindow
    {
        [MenuItem("JUK/Configuration")]
        public static void OpenConfiguration()
        {
            var window = GetWindow<JUKConfigEditor>();
            window.titleContent = new GUIContent(nameof(JUK));
            window.Show(true);
            window.minSize = new Vector2(450, 400);
            window.maxSize = new Vector2(450, 400);
        }

        [InitializeOnLoadMethod]
        public static void RegisterBuildAction()
        {

        }

        public void OnGUI()
        {
            var jsprojPath = EditorGUILayout.TextField(nameof(JUKConfiguration.instance.JsprojPath),
              JUKConfiguration.instance.JsprojPath);
            if (string.IsNullOrWhiteSpace(jsprojPath))
                jsprojPath = Path.Combine(Directory.GetCurrentDirectory(), "Jsproj");
            JUKConfiguration.instance.JsprojPath = jsprojPath;

            var outputPath = EditorGUILayout.TextField(nameof(JUKConfiguration.instance.OutputPath),
             JUKConfiguration.instance.OutputPath);
            if (string.IsNullOrWhiteSpace(outputPath))
                outputPath = Path.Combine(Application.streamingAssetsPath, "dist");
            JUKConfiguration.instance.OutputPath = outputPath;

            var debugMode = EditorGUILayout.Toggle(nameof(JUKConfiguration.instance.DebugMode),
                JUKConfiguration.instance.DebugMode);
            JUKConfiguration.instance.DebugMode = debugMode;

            var strict = EditorGUILayout.Toggle(nameof(JUKConfiguration.instance.IsStrict),
                JUKConfiguration.instance.IsStrict);
            JUKConfiguration.instance.IsStrict = strict;
            EditorUtility.SetDirty(JUKConfiguration.instance);
        }
    }
}