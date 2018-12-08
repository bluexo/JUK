var Engine = importNamespace("UnityEngine");
var UI = importNamespace("UnityEngine.UI");
var JUK = importNamespace("JUK");

var Startup = function(){
    var go = new Engine.GameObject("startup");
    var transform = go.GetComponent(Engine.Transform);
    AddComponent(go,"JUK.JSInteropComp");
    Log(transform.name);
}