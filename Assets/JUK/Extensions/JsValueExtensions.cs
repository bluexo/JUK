using System.Collections;
using System.Collections.Generic;
using Jint;
using Jint.Native;
using UnityEngine;

public static class JsValueExtensions
{
    public static T To<T>(this JsValue jsValue) where T : class
    {
        var obj = jsValue.ToObject();
        return obj as T;
    }

    public static Engine SetClrValue(this Engine engine, string name, object obj)
    {
        var jsv = JsValue.FromObject(engine, obj);
        return engine.SetValue(name, obj);
    }
}
