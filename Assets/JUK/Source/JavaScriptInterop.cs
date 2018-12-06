using System.Collections;
using System.Collections.Generic;
using Jint;
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
        private void Start()
        {
#if UNITY_EDITOR
#endif
        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}
