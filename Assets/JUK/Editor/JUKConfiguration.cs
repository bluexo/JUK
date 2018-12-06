using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JUK
{
    public class JUKConfiguration : ScriptableSingleton<JUKConfiguration>
    {
        public bool DebugMode;
        public bool IsStrict;
        public string JsprojPath;
        public string OutputPath;
    }
}
