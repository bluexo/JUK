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
            option.DiscardGlobal();
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
                for (var i = 0; i < files.Length; i++)
                {
                    var txt = File.ReadAllText(files[i]);
                    JsEngine.Execute(txt);
                }
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

        [MessagePackObject]
        public class Nested
        {
            [Key(0)]
            public int a { get; set; }
            [Key(1)]
            public string b { get; set; }
            [Key(2)]
            public bool c { get; set; }
        }

        [MessagePackObject]
        public class SampleData
        {
            [Key(0)]
            public int number { get; set; }
            [Key(1)]
            public double number2 { get; set; }
            [Key(2)]
            public string text { get; set; }
            [Key(3)]
            public bool flag { get; set; }
            [Key(4)]
            public List<int> list { get; set; }
            [Key(5)]
            public Nested obj { get; set; }
        }

        public void DeserializeByJs(byte[] buffer)
        {
            var obj = MessagePackSerializer.Deserialize<SampleData>(buffer);
            Debug.Log($"{obj.text},{obj.flag},{obj.list.Count}");
            var sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            for (var i = 0; i < 10000; i++)
            {
                var newBuffer = MessagePackSerializer.Deserialize<SampleData>(buffer);
            }
            sw.Stop();
            Debug.Log("-----CS");
            Debug.Log($"{sw.ElapsedMilliseconds}");
            Debug.Log("CS-----");

            var test = JsEngine.GetValue("test");
            sw.Reset();
            sw.Start();
            Debug.Log("-----JS");
            test.Invoke();
            Debug.Log(sw.ElapsedMilliseconds);
            Debug.Log("JS-----");
            sw.Stop();
        }


        // Update is called once per frame
        private void Update()
        {

        }
    }
}
