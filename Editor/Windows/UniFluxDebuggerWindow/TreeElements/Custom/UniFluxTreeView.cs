using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UniFlux.Editor
{
    internal class UniFluxTreeView : TreeViewWithTreeModel<UniFluxTreeElement>
    {
        protected override bool _KeepHierarchyOnSearch => UniFluxDebuggerUtils.KeepHierarchyOnSearch;
        protected override string _Searcher => UniFluxDebuggerUtils.Seacher;
        private const float RowHeight = 20f;
        private const float ToggleWidth = 18f;

        public UniFluxTreeView(TreeViewState state, MultiColumnHeader multiColumnHeader, TreeModel<UniFluxTreeElement> model) : base(state, multiColumnHeader, model)
        {
            rowHeight = RowHeight;
            columnIndexForTreeFoldouts = 0;
            showAlternatingRowBackgrounds = true;
            showBorder = true;
            customFoldoutYOffset = (RowHeight - EditorGUIUtility.singleLineHeight) * 0.5f; // center foldout in the row since we also center content. See RowGUI
            extraSpaceBeforeIconAndLabel = ToggleWidth;
            Reload();
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = (TreeViewItem<UniFluxTreeElement>) args.item;

            for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
            {
                CellGUI(args.GetCellRect(i), item, ref args);
            }
        }
        private Texture2D _texture;

        private void TryInitTexture()
        {
            if (_texture == null)
            {
                _texture = new Texture2D(1, 1);
                _texture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.5f));
                _texture.Apply();
            }
        }

        private void CellGUI(Rect cellRect, TreeViewItem<UniFluxTreeElement> item, ref RowGUIArgs args)
        {
            TryInitTexture();
            CenterRectUsingSingleLineHeight(ref cellRect);

            DrawItemIcon(cellRect, item);
            cellRect.xMin += 20; // Icon size (16) + 4 of padding

            DrawName(item, cellRect, item.Data.Name);
        }

        private void DrawName(TreeViewItem item, Rect area, string name)
        {
            area.xMin += GetContentIndent(item);
            GUI.Label(area, name);
        }

        private void DrawItemIcon(Rect area, TreeViewItem<UniFluxTreeElement> item)
        {
            area.xMin += GetContentIndent(item);
            area.width = 16;
            GUI.DrawTexture(area, item.Data.Icon, ScaleMode.ScaleToFit);
        }

        public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState()
        {
            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    canSort = false,
                    headerContent = new GUIContent("Hierarchy"),
                    headerTextAlignment = TextAlignment.Left,
                    width = 280,
                    minWidth = 60,
                    autoResize = true,
                    allowToggleVisibility = false
                }
            };
            return new MultiColumnHeaderState(columns);
        }
    }
}