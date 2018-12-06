using System;
using System.Collections;
using System.Collections.Generic;
using Jint;
using Jint.Native;
using Jint.Native.Object;
using Jint.Runtime;
using UnityEngine;
using UnityEngine.UI;

internal delegate void LogDelegate(string message, params object[] args);

public class Startup : MonoBehaviour
{
    private static readonly Engine Engine = new Engine(cfg =>
    {
        cfg.DebugMode();
        cfg.AllowDebuggerStatement(true);
        cfg.AllowClr(typeof(Startup).Assembly, typeof(Vector3).Assembly, typeof(Button).Assembly);
    });

    public Text text;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        var code = Resources.Load<TextAsset>("game");
        Engine.SetValue("Log", new Action<string>(Debug.Log));
        Engine.SetClrValue(nameof(Startup), this);
        Engine.SetClrValue(nameof(Logger), Debug.unityLogger);
        try
        {
            Engine.Execute(code.text);
        }
        catch (JavaScriptException exc)
        {
            Debug.LogError($"{exc}");
        }
    }

    public void CallByJs()
    {
        Debug.Log(nameof(CallByJs));
    }

    /// <summary>
    /// Update is called once per frame 
    /// </summary>
    private void Update()
    {
    }
}
