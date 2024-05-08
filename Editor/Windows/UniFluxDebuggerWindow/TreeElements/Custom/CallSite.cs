using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniFlux.Editor
{
    [Serializable]
    internal class CallSite
    {
        public string ClassName;
        public string FunctionName;
        public string Path;
        public int Line;
    }
}