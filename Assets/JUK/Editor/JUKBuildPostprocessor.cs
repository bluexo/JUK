using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace JUK
{
    public class JUKBuildPostprocessor
    {
        [MenuItem("JUK/Copy To Output")]
        public static void CopyToOutput()
        {
            var path = JUKConfiguration.instance.JsprojPath;
            if (string.IsNullOrWhiteSpace(path)) return;
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            for (var i = 0; i < files.Length; i++)
            {
                var file = new FileInfo(files[i]);
                var dest = JUKConfiguration.instance.OutputPath;
                if (!Directory.Exists(dest))
                    Directory.CreateDirectory(dest);
                file.CopyTo($"{dest}/{file.Name}.txt", true);
            }
            AssetDatabase.Refresh();
        }

        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) => CopyToOutput();
    }
}
