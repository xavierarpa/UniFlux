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
    public class UniFluxDebuggerWindow : EditorWindow
    {
        private const string ContainerIcon = "PreMatCylinder"; // d_PrefabModel On Icon, "PreMatCylinder"
        private const string ResolverIcon = "d_NetworkAnimator Icon"; // "d_eyeDropper.Large", "AnimatorStateTransition Icon", "RelativeJoint2D Icon"
        private const string InstanceIcon = "d_Prefab Icon"; // "d_Prefab Icon", "d_Prefab On Icon"

        [NonSerialized] private bool _isInitialized;
        [SerializeField] private TreeViewState _treeViewState; // Serialized in the window layout file so it survives assembly reloading
        [SerializeField] private MultiColumnHeaderState _multiColumnHeaderState;

        private int _id = -1;
        private SearchField _searchField;
        private Vector2 _bindingStackTraceScrollPosition;

        private MultiColumnTreeView TreeView { get; set; }
        private Rect SearchBarRect => new Rect(20f, 10f, position.width - 40f, 20f);
        private Rect MultiColumnTreeViewRect => new Rect(20, 30, position.width - 40, position.height - 200);

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
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

                var treeModel = new TreeModel<MyTreeElement>(GetData());

                TreeView = default;
                TreeView = new MultiColumnTreeView(_treeViewState, multiColumnHeader, treeModel);
                TreeView.ExpandAll();

                _searchField = new SearchField();
                _searchField.downOrUpArrowKeyPressed += TreeView.SetFocusAndEnsureSelectedItem;

                _isInitialized = true;
            }
        }
        private IList<MyTreeElement> GetData()
        {
            var root = new MyTreeElement("Root", -1, ++_id, ContainerIcon, () => string.Empty, Array.Empty<string>(), string.Empty, null, string.Empty);

            #region GENERATE ITEMS IN DA LIST
            void AddElement(MyTreeElement element)
            {
                root.Children.Add(element);
                element.Parent = root;
            }

            
            // AddElement(UniFluxDebuggerUtils.Test_GetFluxItem());

            AddElement(
                new MyTreeElement(
                    "// this thing still in testing beep boop", 
                    root.Depth + 1, 
                    ++_id, 
                    ContainerIcon, 
                    () => string.Empty, 
                    Array.Empty<string>(), 
                    string.Empty, 
                    default, 
                    kind: string.Empty
                )
            );

            AddElement(
                new MyTreeElement(
                    "ActionFlux<string>",
                    root.Depth + 1, 
                    ++_id, 
                    ContainerIcon, 
                    () => string.Empty, 
                    Array.Empty<string>(), 
                    string.Empty, 
                    default, 
                    kind: string.Empty
                )
            );
            AddElement(
                new MyTreeElement(
                    "ActionFlux<bool>",
                    root.Depth + 1, 
                    ++_id, 
                    ContainerIcon, 
                    () => string.Empty, 
                    Array.Empty<string>(), 
                    string.Empty, 
                    default, 
                    kind: string.Empty
                )
            );

            #endregion

            var list = new List<MyTreeElement>();
            TreeElementUtility.TreeToList(root, list);
            return list;
        }

        private void OnGUI()
        {
            GUI.enabled = Application.isPlaying;
            //
            Repaint();
            InitIfNeeded();
            //
            PresentDebuggerEnabled();
            //
            GUI.enabled = true;
            if(GUILayout.Button("TEST"))
            {
                UniFlux.Editor.UniFluxDebuggerUtils.Test();
            }
            GUI.enabled = false;

            GUILayout.Label("[X] 1 Conocer los objetos creados");
            GUILayout.Label("[X] 2 Conocer los diccionarios de los objetos creados");
            GUILayout.Label("[X] 3 Conocer las suscripciones de los diccionarios creados");
            GUILayout.Space(10);
            GUILayout.Label("[X] 4 Poder ejecutar una clave de un diccionario");
            GUILayout.Label("[X] 5 Poder ejecutar una suscripción del diccionario");
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
                    GUI.enabled = true;
                    GUILayout.Label("This only works in Play mode");
                    GUI.enabled = false;
                }
                GUILayout.FlexibleSpace();

                var refreshIcon = EditorGUIUtility.IconContent("d_TreeEditor.Refresh");
                refreshIcon.tooltip = "Forces Tree View to Refresh";
        
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