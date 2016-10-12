//
// Klak - Utilities for creative coding with Unity
//
// Copyright (C) 2016 Keijiro Takahashi
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Klak.Wiring.Patcher
{
    public abstract class PatcherWindow : EditorWindow
    {
        bool isPlayMode
        {
            get
            {
                return EditorApplication.isPlaying ||
                    EditorApplication.isPlayingOrWillChangePlaymode;
            }
        }

        protected Patch _patch;
        protected PatchManager _patchManager;
        protected NodeFactory _nodeFactory;
        
        // Node property editor
        protected Editor _propertyEditor;
        
        // Hierarchy change flag
        bool _hierarchyChanged;

        [MenuItem("Window/Klak/Patcher")]
        static void Init()
        {
            EditorWindow.GetWindow<PatcherMainWindow>("Patcher").Show();
            EditorWindow.GetWindow<PatcherInspectorWindow>("Patcher Inspector").Show();
        }

        public static void OpenPatch(Wiring.Patch patchInstance)
        {
            var window = EditorWindow.GetWindow<PatcherMainWindow>("Patcher", typeof(SceneView));
            var uI = typeof(Editor).Assembly.GetType("UnityEditor.InspectorWindow");
            var inspector = EditorWindow.GetWindow<PatcherInspectorWindow>("Patcher Inspector", uI);

            window._patch = new Patch(patchInstance);
            window._patchManager.Select(window._patch);

            inspector._patch = window._patch;
            inspector._patchManager = window._patchManager;

            window.Show();
            inspector.Show();
            window.ResetPosition();
            window.ResetSelection();
        }

        protected void ResetSelection()
        {
            Node.ActiveNode = null;
            NodeLink.SelectedLink = null;
        }

        protected virtual void OnEnable()
        {
            ResetSelection();
            _patchManager = new PatchManager();
            _nodeFactory = new NodeFactory();
            _patchManager.Reset();
            _patch = _patchManager.RetrieveLastSelected();

            Undo.undoRedoPerformed += OnUndo;
            EditorApplication.hierarchyWindowChanged += OnHierarchyWindowChanged;
        }

        void OnDisable()
        {
            if (_propertyEditor != null)
            {
                DestroyImmediate(_propertyEditor);
                _propertyEditor = null;
            }

            _patchManager = null;
            _nodeFactory = null;
            _patch = null;

            Undo.undoRedoPerformed -= OnUndo;
            EditorApplication.hierarchyWindowChanged -= OnHierarchyWindowChanged;
        }

        void OnUndo()
        {
            // We have to rescan patches and nodes,
            // because there may be an unknown ones.
            _patchManager.Reset();
            if (_patch != null && _patch.isValid) _patch.Rescan();

            // Manually update the GUI.
            Repaint();
        }

        void OnFocus()
        {
            // Do nothing if not yet enabled.
            if (_patchManager == null) return;

            // Rescan if there are changes in the hierarchy while unfocused.
            if (_hierarchyChanged)
            {
                _patchManager.Reset();
                if (_patch != null && _patch.isValid) _patch.Rescan();
            }
        }

        void OnLostFocus()
        {
            // To record hierarchy change while unfocused.
            _hierarchyChanged = false;
        }

        void OnHierarchyWindowChanged()
        {
            _hierarchyChanged = true;
        }

        void OnInspectorUpdate()
        {
            Repaint();
        }

        void OnGUI()
        {
            if (isPlayMode)
            {
                DrawPlaceholderGUI("Not available in play mode");
                return;
            }

            // If there is something wrong with the patch manager, reset it.
            if (!_patchManager.isValid) _patchManager.Reset();

            // Patch validity check.
            if (_patch != null)
                if (!_patch.isValid)
                    _patch = null; // Seems like not good. Abandon it.
                else if (!_patch.CheckNodesValidity())
                    _patch.Rescan(); // Some nodes are not good. Rescan them.

            // Get a patch if no one is selected.
            if (_patch == null)
                _patch = _patchManager.RetrieveLastSelected();


            if (_patch == null && Selection.activeGameObject != null)
            {
                var p = Selection.activeGameObject.GetComponent<Klak.Wiring.Patch>();
                if (p != null)
                {
                    _patch = new Patch(p);
                }
            }
            // Draw a placeholder if no patch is available.
            // Disable GUI during the play mode, or when no patch is available.
            if (_patch == null)
            {
                DrawPlaceholderGUI("No patch available");
                return;
            }

            //if (Event.current.isKey)
            //{
            //    if (Event.current.keyCode == KeyCode.Escape)
            //    {
            //        Node._activeWindowID = -1;
            //        EditorGUIUtility.keyboardControl = 0;
            //    }
            //}
            DrawGUI();
        }

        void DrawPlaceholderGUI(string message)
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(message, EditorStyles.largeLabel);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        protected abstract void DrawGUI();

        // Find and get the active node.
        protected Node GetActiveNode()
        {
            var all = _patch.nodeList.Where(n => n.isActive).ToList();
            return all.FirstOrDefault();
        }

        // Find and get the active node.
        protected Node GetFocusedNode()
        {
            for (int i = 0; i < _patch.nodeList.Count; i++)
            {
                var node = _patch.nodeList[i];
                if (node.isFocused)
                {
                    return node;
                }
            }
            return null;
        }

        // Reset the internal state.
        void ResetState()
        {
            _patchManager.Reset();

            if (_patch == null || !_patch.isValid)
                _patch = _patchManager.RetrieveLastSelected();
            else
                _patch.Rescan();

            Repaint();
        }
    }
}
