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
using System.Linq;
using UniFlux.Editor;

namespace UniFlux.Editor
{
    [EditorWindowTitle(title = "UniFlux")] 
    internal class UniFluxDebuggerWindow : EditorWindow
    {
        [NonSerialized] private bool _isInitialized;
        [SerializeField] private TreeViewState _treeViewState; // Serialized in the window layout file so it survives assembly reloading
        [SerializeField] private MultiColumnHeaderState _multiColumnHeaderState;

        private SearchField _searchField;
        private Vector2 _bindingStackTraceScrollPosition;

        private MultiColumnTreeView TreeView { get; set; }
        private Rect SearchBarRect => new Rect(20f, 10f, position.width - 40f, 20f);
        private Rect MultiColumnTreeViewRect => new Rect(20, 30, position.width - 40, position.height - 200);

        private void OnEnable()
        {
            true.Subscribe(ref UniFlux.Core.Internal.Flux.OnAddFluxType, OnAddFluxType);

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            false.Subscribe(ref UniFlux.Core.Internal.Flux.OnAddFluxType, OnAddFluxType);

            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private async void OnAddFluxType()
        {
            await Task.Yield();
            Refresh();
        }
        private async void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            await Task.Yield();
            Refresh();
        }

        private async void OnSceneUnloaded(Scene scene)
        {
            await Task.Yield();
            Refresh();
        }

        private async void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            await Task.Yield();
            Refresh();
        }

        private void InitIfNeeded()
        {
            if (!_isInitialized)
            {
                // Check if it already exists (deserialized from window layout file or scriptable object)
                if (_treeViewState == null)
                    _treeViewState = new TreeViewState();

                bool firstInit = _multiColumnHeaderState == null;
                var headerState = MultiColumnTreeView.CreateDefaultMultiColumnHeaderState();
                if (MultiColumnHeaderState.CanOverwriteSerializedFields(_multiColumnHeaderState, headerState))
                    MultiColumnHeaderState.OverwriteSerializedFields(_multiColumnHeaderState, headerState);
                _multiColumnHeaderState = headerState;

                var multiColumnHeader = new MultiColumnHeader(headerState)
                {
                    canSort = false,
                    height = MultiColumnHeader.DefaultGUI.minimumHeight
                };

                if (firstInit)
                    multiColumnHeader.ResizeToFit();

                var treeModel = new TreeModel<UniFluxTreeElement>(GetData());

                TreeView = default;
                TreeView = new MultiColumnTreeView(_treeViewState, multiColumnHeader, treeModel);
                TreeView.ExpandAll();

                _searchField = new SearchField();
                _searchField.downOrUpArrowKeyPressed += TreeView.SetFocusAndEnsureSelectedItem;

                _isInitialized = true;
            }
        }
        private IList<UniFluxTreeElement> GetData()
        {
            IEnumerable<UniFluxTreeElement> data;
            // Clears Root childs
            UniFluxDebuggerUtils.ClearRootChilds(UniFluxDebuggerUtils.Root);

            if(UnityScriptingDefineSymbols.IsDefined("UNIFLUX_DEBUG"))
            {
                if(Application.isPlaying)
                {
                    data = UniFluxDebuggerUtils.TreeElements_DEBUG;
                }
                else
                {
                    data = UniFluxDebuggerUtils.TreeElements_NO_DEBUG;
                }
            }
            else
            {
                data = UniFluxDebuggerUtils.TreeElements_NO_DEBUG;
            }
            //
            foreach (var treeElement in data)
            {
                UniFluxDebuggerUtils.SetRootChild(UniFluxDebuggerUtils.Root, treeElement);
            }
            //
            var list = new List<UniFluxTreeElement>();
            TreeElementUtility.TreeToList(UniFluxDebuggerUtils.Root, list);
            return list;
        }

        private void OnGUI()
        {
            Repaint();
            InitIfNeeded();
            //
            PresentDebuggerEnabled();
            //
            GUILayout.FlexibleSpace();
            PresentStatusBar();
        }

        private void Refresh(PlayModeStateChange _ = default)
        {
            _isInitialized = false;
            InitIfNeeded();
        }

        private void SearchBar(Rect rect)
        {
            TreeView.searchString = _searchField.OnGUI(rect, TreeView.searchString);
            GUILayoutUtility.GetRect(rect.width, rect.height);
        }

        private void DoTreeView(Rect rect)
        {
            TreeView.OnGUI(rect);
            GUILayoutUtility.GetRect(rect.width, rect.height);
        }
        private void PresentDebuggerEnabled()
        {
            SearchBar(SearchBarRect);
            DoTreeView(MultiColumnTreeViewRect);

            GUILayout.Space(16);

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(16);

                using (new GUILayout.VerticalScope())
                {
                    _bindingStackTraceScrollPosition = GUILayout.BeginScrollView(_bindingStackTraceScrollPosition);

                    PresentCallSite();

                    GUILayout.EndScrollView();
                    GUILayout.Space(16);
                }

                GUILayout.Space(16);
            }
        }

        private void PresentStatusBar()
        {
            using (new EditorGUILayout.HorizontalScope(Styles.AppToolbar))
            {
                if(Application.isPlaying)
                {
                }
                else
                {
                    GUILayout.Label("This only works in Play mode");
                }

                if(UnityScriptingDefineSymbols.IsDefined("UNIFLUX_DEBUG"))
                {

                }
                else
                {
                    GUILayout.Label("Define 'UNIFLUX_DEBUG'");
                }
                GUILayout.FlexibleSpace();

                var icon_debug = EditorGUIUtility.IconContent(
                    UnityScriptingDefineSymbols.IsDefined("UNIFLUX_DEBUG")
                        ? "d_DebuggerEnabled"
                        : "d_DebuggerDisabled"
                );
                var refreshIcon = EditorGUIUtility.IconContent("d_TreeEditor.Refresh");
                refreshIcon.tooltip = "Forces Tree View to Refresh";
        

                GUI.enabled = !Application.isPlaying;
                if (GUILayout.Button(icon_debug, Styles.StatusBarIcon, GUILayout.Width(25)))
                {
                    UnityScriptingDefineSymbols.Toggle("UNIFLUX_DEBUG", EditorUserBuildSettings.selectedBuildTargetGroup);
                }
                GUI.enabled = true;


                if (GUILayout.Button(refreshIcon, Styles.StatusBarIcon, GUILayout.Width(25)))
                {
                    Refresh();
                }
            }
        }

        private void PresentCallSite()
        {
            var selection = TreeView.GetSelection();

            if (selection == null || selection.Count == 0)
            {
                return;
            }

            var item = TreeView.Find(selection.Single());

            if (item == null || item.Callsite == null)
            {
                return;
            }

            foreach (var callSite in item.Callsite)
            {
                PresentStackFrame(callSite.ClassName, callSite.FunctionName, callSite.Path, callSite.Line);
            }
        }

        private static void PresentStackFrame(string className, string functionName, string path, int line)
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label($"{className}:{functionName}()  →", Styles.StackTrace);

                if (PresentLinkButton($"{path}:{line}"))
                {
                    UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(path, line);
                }
            }
        }

        private static bool PresentLinkButton(string label, params GUILayoutOption[] options)
        {
            var position = GUILayoutUtility.GetRect(new GUIContent(label), Styles.Hyperlink, options);
            position.y -= 3;
            Handles.color = Styles.Hyperlink.normal.textColor;
            Handles.DrawLine(new Vector3(position.xMin + (float)EditorStyles.linkLabel.padding.left, position.yMax), new Vector3(position.xMax - (float)EditorStyles.linkLabel.padding.right, position.yMax));
            Handles.color = Color.white;
            EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);
            return GUI.Button(position, label, Styles.Hyperlink);
        }
    }
}