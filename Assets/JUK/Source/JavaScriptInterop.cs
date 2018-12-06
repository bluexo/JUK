using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Jint;
using Jint.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JUK
{
    public class JavaScriptInterop : MonoBehaviour
    {
        public readonly static Engine JsEngine = new Engine(option =>
        {
            option.AllowClr(typeof(Application).Assembly,
                typeof(Button).Assembly,
                typeof(Event).Assembly,
                typeof(PointerEventData).Assembly);
            option.Strict();
            option.DebugMode();
            option.AllowDebuggerStatement();
        });

        // Use this for initialization
        private void Awake()
        {
            try
            {
#if UNITY_EDITOR
                var files = Directory.GetFiles(Application.streamingAssetsPath + "/dist/", "*.txt", SearchOption.AllDirectories);
                for (var i = 0; i < files.Length; i++)
                {
                    var txt = File.ReadAllText(files[i]);
                    JsEngine.Execute(txt);
                }
#endif
            }
            catch (JavaScriptException exc)
            {
                Debug.LogError($"{exc},\n" +
                    $"LineNumber={exc.LineNumber} ,\n" +
                    $"Location={exc.Location.Source} \n");
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
            JsEngine.SetClrValue(nameof(Startup), this);
            JsEngine.SetClrValue(nameof(Logger), Debug.unityLogger);
            try
            {
                JsEngine.Execute(code.text);
            }
            catch (JavaScriptException exc)
            {
                Debug.LogError($"{exc},\n" +
                    $"LineNumber={exc.LineNumber} ,\n" +
                    $"Location={exc.Location.Source} \n");
            }
        }

        public void CallByJs()
        {
            Debug.Log(nameof(CallByJs));
        }


        // Update is called once per frame
        private void Update()
        {

        }
    }
}
