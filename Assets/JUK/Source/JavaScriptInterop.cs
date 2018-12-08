using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Jint;
using Jint.Native;
using Jint.Runtime;
using MessagePack;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace JUK
{
    public class JavaScriptInterop : MonoBehaviour
    {
        public readonly static Engine JsEngine = new Engine(option =>
        {
            option.AllowClr(typeof(Application).Assembly,
                typeof(Button).Assembly,
                typeof(Event).Assembly,
                typeof(PointerEventData).Assembly,
                typeof(Stopwatch).Assembly);
            option.Strict();
            //option.DebugMode();
            //option.AllowDebuggerStatement();
        });

        // Use this for initialization
        private void Awake()
        {
            try
            {
#if UNITY_EDITOR
                var files = Directory.GetFiles(Application.streamingAssetsPath + "/dist/", "*.txt", SearchOption.AllDirectories);
                Array.ForEach(files, f => JsEngine.Execute(File.ReadAllText(f)));
#else

#endif
            }
            catch (JavaScriptException exc)
            {
                Debug.LogError($"{exc},\n" +
                    $"{nameof(exc.LineNumber)} = {exc.LineNumber}, \n" +
                    $"{nameof(exc.Column)} = {exc.Column}), \n" +
                    $"{nameof(exc.CallStack)} = {exc.CallStack} \n");
            }
        }

        public Text text;

        /// <summary>
        /// Use this for initialization
        /// </summary>
        private void Start()
        {
            var code = Resources.Load<TextAsset>("game");
            JsEngine.SetValue("Log", new Action<string>(Debug.Log));
            JsEngine.SetClrValue("Startup", this);
            try
            {
                JsEngine.Execute(code.text);
            }
            catch (JavaScriptException exc)
            {
                Debug.LogError($"{exc},\n" +
                    $"{nameof(exc.LineNumber)} = {exc.LineNumber}, \n" +
                    $"{nameof(exc.Column)} = {exc.Column}), \n" +
                    $"{nameof(exc.CallStack)} = {exc.CallStack} \n");
            }
        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}
