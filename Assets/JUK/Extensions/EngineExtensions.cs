using System;
using System.Collections;
using System.Collections.Generic;
using Jint.Native;
using UnityEngine;

namespace JUK
{

    public static class EngineExtensions
    {
        public static Type Add(this GameObject gameObject, JsValue jsValue)
        {
            var type = jsValue.To<Type>();
            gameObject.AddComponent(type);
            return type;
        }
    }
}
