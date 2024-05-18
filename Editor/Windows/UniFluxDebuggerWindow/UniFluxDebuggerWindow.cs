using UnityEditor.IMGUI.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private UniFluxTreeView TreeView { get; set; }
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
            IList<UniFluxTreeElement> GetData()
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
            if (!_isInitialized)
            {
                // Check if it already exists (deserialized from window layout file or scriptable object)
                if (_treeViewState == null)
                    _treeViewState = new TreeViewState();

                bool firstInit = _multiColumnHeaderState == null;
                var headerState = UniFluxTreeView.CreateDefaultMultiColumnHeaderState();
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
                TreeView = new UniFluxTreeView(_treeViewState, multiColumnHeader, treeModel);
                TreeView.ExpandAll();

                _searchField = new SearchField();
                _searchField.downOrUpArrowKeyPressed += TreeView.SetFocusAndEnsureSelectedItem;

                _isInitialized = true;
            }
        }
        private void OnGUI()
        {
            Repaint();
            InitIfNeeded();
            //
            PresentDebuggerEnabled();
            //
            GUILayout.FlexibleSpace();
            //
            PresentStatusBar();
        }

        private void Refresh(PlayModeStateChange _ = default)
        {
            _isInitialized = false;
            InitIfNeeded();
        }
        private void PresentDebuggerEnabled()
        {
            void SearchBar(Rect rect)
            {
                var _response_searchField_old = UniFluxDebuggerUtils.Seacher;
                var _response_searchField_new = _searchField.OnGUI(rect, UniFluxDebuggerUtils.Seacher);
                //
                UniFluxDebuggerUtils.Seacher = _response_searchField_new;
                //
                if(_response_searchField_new != _response_searchField_old)
                {
                    // Trick to Update
                    TreeView.searchString = _response_searchField_new;
                    TreeView.searchString = null;
                }

                GUILayoutUtility.GetRect(rect.width, rect.height);
            }
            void DoTreeView(Rect rect)
            {
                TreeView.OnGUI(rect);
                GUILayoutUtility.GetRect(rect.width, rect.height);
            }

            SearchBar(SearchBarRect);
            DoTreeView(MultiColumnTreeViewRect);

            GUILayout.Space(16);

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(16);

                using (new GUILayout.VerticalScope())
                {
                    _bindingStackTraceScrollPosition = GUILayout.BeginScrollView(_bindingStackTraceScrollPosition);

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
                if(UnityScriptingDefineSymbols.IsDefined("UNIFLUX_DEBUG"))
                {
                    // Nada..
                }
                else
                {
                    GUILayout.Label("Define 'UNIFLUX_DEBUG' to activate runtime debug mode");
                }
                GUILayout.FlexibleSpace();

                var icon_hierarchy = UniFluxDebuggerUtils.KeepHierarchyOnSearch ? EditorGUIUtility.IconContent("d_UnityEditor.SceneHierarchyWindow@2x") : EditorGUIUtility.IconContent("d_Search Icon");
                icon_hierarchy.tooltip = "Allow Hierarchy when searching";
                
                var icon_debug = EditorGUIUtility.IconContent(UnityScriptingDefineSymbols.IsDefined("UNIFLUX_DEBUG") ? Application.isPlaying ? "d_DebuggerAttached" : "d_DebuggerEnabled": "d_DebuggerDisabled");
                icon_debug.tooltip = "Allow Debug Mode (Runtime Debugging)";

                var icon_refresh = EditorGUIUtility.IconContent("d_TreeEditor.Refresh");
                icon_refresh.tooltip = "Forces Tree View to Refresh";

                if (GUILayout.Button(icon_hierarchy, Styles.StatusBarIcon, GUILayout.Width(25)))
                {
                    UniFluxDebuggerUtils.KeepHierarchyOnSearch = !UniFluxDebuggerUtils.KeepHierarchyOnSearch;
                    Refresh();
                }

                GUI.enabled = !Application.isPlaying;
                if (GUILayout.Button(icon_debug, Styles.StatusBarIcon, GUILayout.Width(25)))
                {
                    UnityScriptingDefineSymbols.Toggle("UNIFLUX_DEBUG", EditorUserBuildSettings.selectedBuildTargetGroup);
                    Refresh();
                }
                GUI.enabled = true;

                if (GUILayout.Button(icon_refresh, Styles.StatusBarIcon, GUILayout.Width(25)))
                {
                    Refresh();
                }
            }
        }
    }
}