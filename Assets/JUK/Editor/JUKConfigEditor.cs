using System.Collections;
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

        [MenuItem("JUK/Debug Mode", priority = 1)]
        public static void DebugMode()
        {
            EditorPrefs.SetBool(nameof(DebugMode), !EditorPrefs.GetBool(nameof(DebugMode), true));
        }

        [MenuItem("JUK/Debug Mode", priority = 1, validate = true)]
        public static bool OpenEditorCheck()
        {
            Menu.SetChecked("JUK/Debug Mode", EditorPrefs.GetBool(nameof(DebugMode)));
            return true;
        }

        [InitializeOnLoadMethod]
        public static void RegisterBuildAction()
        {

        }

        public void OnGUI()
        {
            var jsprojPath = EditorGUILayout.TextField(nameof(JSConf.instance.JsprojPath),
              JSConf.instance.JsprojPath);
            if (string.IsNullOrWhiteSpace(jsprojPath))
                jsprojPath = Path.Combine(Directory.GetCurrentDirectory(), "Jsproj").Replace('\\', '/') + "/";
            JSConf.instance.JsprojPath = jsprojPath;

            var outputPath = EditorGUILayout.TextField(nameof(JSConf.instance.OutputPath),
             JSConf.instance.OutputPath);
            if (string.IsNullOrWhiteSpace(outputPath))
                outputPath = Path.Combine(Application.streamingAssetsPath, "dist").Replace('\\', '/') + "/";
            JSConf.instance.OutputPath = outputPath;

            EditorUtility.SetDirty(JSConf.instance);
        }
    }
}
