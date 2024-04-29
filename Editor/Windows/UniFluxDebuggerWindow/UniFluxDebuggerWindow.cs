using UnityEditor.IMGUI.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UniFlux.Editor
{
    [EditorWindowTitle(title = "UniFlux")] 
    public class UniFluxDebuggerWindow : EditorWindow
    {
        private Vector2 _bindingStackTraceScrollPosition;
        private MultiColumnTreeView TreeView { get; set; }
        private Rect MultiColumnTreeViewRect => new Rect(20, 30, position.width - 40, position.height - 200);

        private void OnGUI()
        {
            Refresh();
        }
        private void Refresh()
        {
            Repaint();
            Render();
        }
        private void Render()
        {
            Render_Tree();
            GUILayout.FlexibleSpace();
            Render_StatusBar();
        }
        private void Render_Tree()
        {
            void DoTreeView(Rect rect)
            {
                TreeView.OnGUI(rect);
                GUILayoutUtility.GetRect(rect.width, rect.height);
            }
            // SearchBar(SearchBarRect);
            DoTreeView(MultiColumnTreeViewRect);

            GUILayout.Space(16);

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(16);

                using (new GUILayout.VerticalScope())
                {
                    _bindingStackTraceScrollPosition = GUILayout.BeginScrollView(_bindingStackTraceScrollPosition);

                    // PresentCallSite();

                    GUILayout.EndScrollView();
                    GUILayout.Space(16);
                }

                GUILayout.Space(16);
            }
        }
        private void Render_StatusBar()
        {
            GUILayouts.RefreshStatusBar(Refresh);
        }
        private void SearchBar(Rect rect)
        {
            // TreeView.searchString = _searchField.OnGUI(rect, TreeView.searchString);
            GUILayoutUtility.GetRect(rect.width, rect.height);
        }
    }
}