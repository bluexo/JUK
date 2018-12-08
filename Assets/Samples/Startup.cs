using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Jint;
using UnityEngine;

namespace JUK
{
    public class Startup : MonoBehaviour
    {
        private void Awake()
        {
            JSInterop.Engine.GetValue(nameof(Startup)).Invoke();
        }
    }
}
