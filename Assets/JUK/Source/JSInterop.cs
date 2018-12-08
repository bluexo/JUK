using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Jint;
using Jint.Native;
using Jint.Runtime;
using Jint.Runtime.Interop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JUK
{
    public static class JSInterop
    {
        public const string DIST_DIR = "dist";

        public static readonly Engine Engine = new Engine(ConfigureOptions);

        static JSInterop()
        {
            try
            {
#if UNITY_EDITOR
                var files = Directory.GetFiles(JSConf.instance.JsprojPath, "*.js", SearchOption.AllDirectories);
                Array.ForEach(files, f => Engine.Execute(File.ReadAllText(f)));
#endif
                Engine.SetValue(nameof(Debug.Log), new Action<UnityEngine.Object>(Debug.Log));
                Engine.SetValue("AddComponent", new Action<GameObject, string>(AddComp));
                Engine.SetValue(nameof(JSInteropComp), TypeReference.CreateTypeReference(Engine, typeof(JSInteropComp)));
            }
            catch (JavaScriptException exc)
            {
                Debug.LogError($"{exc},\n" +
                    $"{nameof(exc.LineNumber)} = {exc.LineNumber}, \n" +
                    $"{nameof(exc.Column)} = {exc.Column}), \n" +
                    $"{nameof(exc.CallStack)} = {exc.CallStack} \n");
            }
        }

        public static void ConfigureOptions(Options options)
        {
            options.AllowClr(typeof(Application).Assembly,
               typeof(Button).Assembly,
               typeof(Event).Assembly,
               typeof(PointerEventData).Assembly);
            options.Strict();
#if UNITY_EDITOR
            options.DebugMode();
            options.AllowDebuggerStatement();
#endif
        }

        public static void AddComp(GameObject go, string typeName)
        {
            var type = Type.GetType(typeName);
            go.AddComponent(type);
        }
    }
}
