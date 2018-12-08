using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
namespace JUK
{
    using UnityEditor;
    public class JSConf : ScriptableSingleton<JSConf>
    {
        public string JsprojPath;
        public string OutputPath;
    }
}
#endif
