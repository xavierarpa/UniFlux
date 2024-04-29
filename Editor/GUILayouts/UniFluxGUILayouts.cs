using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniFlux.Editor
{
    public static class GUILayouts
    {
        public static void RefreshStatusBar(Action callback)
        {
            using (new EditorGUILayout.HorizontalScope(Styles.AppToolbar))
            {
                GUILayout.FlexibleSpace();
                var refreshIcon = EditorGUIUtility.IconContent("d_TreeEditor.Refresh");
                refreshIcon.tooltip = "Forces Tree View to Refresh";
                
                if (GUILayout.Button(refreshIcon, Styles.StatusBarIcon, GUILayout.Width(25)))
                {
                    callback?.Invoke();
                }
            }
        }   
    }
}
