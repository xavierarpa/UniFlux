using UnityEditor;
using UnityEngine;

namespace UniFlux.Editor
{
    public static class Styles
    {
        public readonly static GUIStyle LabelHorizontallyCentered = new GUIStyle("Label")
        {
            alignment = TextAnchor.MiddleCenter
        };
        public readonly static GUIStyle StackTrace = new GUIStyle("CN Message")
        {
            wordWrap = false
        };
        public readonly static GUIStyle StatusBarIcon = new GUIStyle("StatusBarIcon");
        public readonly static GUIStyle AppToolbar = new GUIStyle("AppToolbar");
        public readonly static GUIStyle Hyperlink = new GUIStyle(EditorStyles.linkLabel)
        {
            wordWrap = false
        };
    }
}