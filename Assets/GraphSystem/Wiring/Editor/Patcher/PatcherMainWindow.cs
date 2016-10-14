using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Klak.Wiring.Patcher
{
    public class PatcherMainWindow : PatcherWindow
    {
        private Vector2 _scrollPosition;


        #region Wiring state class
        WiringState _wiring;
        class WiringState
        {
            public Node node;
            public Inlet inlet;
            public Outlet outlet;

            public WiringState(Node node, Inlet inlet)
            {
                this.node = node;
                this.inlet = inlet;
            }

            public WiringState(Node node, Outlet outlet)
            {
                this.node = node;
                this.outlet = outlet;
            }
        }

        #endregion

        #region EditorWindow functions

        private void DrawBackground()
        {
            if (Event.current.type != EventType.Repaint)
                return;
            
            var width = _zoom / GUIStyles.backgroundGrid.width;
            var height = _zoom / GUIStyles.backgroundGrid.height;
            var offset = -_scrollPosition / _zoom;
            var uvDrawRect = new Rect(-offset.x * width,
                (offset.y - Screen.height) * height,
                Screen.width * width,
                Screen.height * height);
            
            GUI.DrawTextureWithTexCoords(
                new Rect(0, 17, Screen.width, Screen.height),
                GUIStyles.backgroundGrid,
                uvDrawRect);
        }

        protected override void DrawGUI()
        {
            //DrawBackground();
            EventHandler();

            // Menu
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            _nodeFactory.CreateNodeMenuGUI(_patch);
            GUILayout.Space(100);
            var patchIndex = _patchManager.GetIndexOf(_patch);
            var newPatchIndex = EditorGUILayout.Popup(
                patchIndex, _patchManager.MakeNameList(),
                EditorStyles.toolbarDropDown);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();



            // Main view
            EditorGUILayout.BeginVertical();
            DrawMainViewGUI();
            EditorGUILayout.EndVertical();

            // Re-initialize the editor if the patch selection was changed.
            if (patchIndex != newPatchIndex)
            {
                _patch = _patchManager.RetrieveAt(newPatchIndex);
                _patchManager.Select(_patch);
                ResetPosition();
                ResetSelection();
                Repaint();
            }

            // Cancel wiring with a mouse click or hitting the esc key.
            if (_wiring != null)
            {
                var e = Event.current;
                if (e.type == EventType.MouseUp ||
                    (e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape))
                {
                    _wiring = null;
                    e.Use();
                }
            }
        }
        
        private Node _currentCopiedNode;
        private void EventHandler()
        {
            FeedbackQueue.Reset();

            var e = Event.current;
            var activeNode = GetActiveNode();
            if (e.isKey && e.keyCode == KeyCode.Delete)
            {
                if (NodeLink.SelectedLink == null)
                    return;
                NodeLink.SelectedLink.RemoveLink();
                _patch.Rescan();
                Repaint();
            }
            else if (e.isKey && e.keyCode == KeyCode.C && e.modifiers == EventModifiers.Control)
            {
                if (activeNode == null)
                    return;
                _currentCopiedNode = activeNode;
            }
            else if (e.isKey && e.keyCode == KeyCode.V && e.modifiers == EventModifiers.Control && _currentCopiedNode != null)
            {
                var nodeBase = Instantiate(_currentCopiedNode._instance);
                nodeBase.name = ObjectNames.NicifyVariableName(_currentCopiedNode.typeName);
                nodeBase.wiringNodePosition += Vector2.one * 50;
                var copiedNode = _patch.AddNodeInstance(nodeBase);
                copiedNode.RemoveAllLinks(_patch);

                foreach (var node in _patch.nodeList)
                    node.RemoveLinksTo(copiedNode, _patch);

                Undo.RegisterCreatedObjectUndo(nodeBase.gameObject, "Paste Node");
            }
            else if (e.isMouse && e.button == 0 && e.modifiers == EventModifiers.None)
            {
                NodeLink.SelectedLink = null;
                foreach (var node in _patch.nodeList)
                {
                    if(node.CachedLinks == null)
                        node.CacheLinks(_patch);

                    for (int index = 0; index < node.CachedLinks.Count; index++)
                    {
                        var link = node.CachedLinks[index];
                        var pos = (e.mousePosition - new Vector2(0, 16/_zoom) + _scrollPosition)/_zoom;
                        if (link.OnLine(pos))
                        {
                            NodeLink.SelectedLinkId = index;
                            NodeLink.SelectedLink = link;
                            Node.ActiveNode = null;
                        }
                    }
                }
            }
        }
        
        private float _zoom = 1;
        
        #endregion

        #region Wiring functions

        // Go into the wiring state.
        void BeginWiring(object data)
        {
            _wiring = (WiringState)data;
        }

        // Remove a link between a pair of nodes.
        void RemoveLink(object data)
        {
            var link = (NodeLink)data;
            link.fromNode.RemoveLink(link.fromOutlet, link.toNode, link.toInlet);
        }

        // Draw the currently working link.
        void DrawWorkingLink()
        {
            var p1 = (Vector3)_wiring.node.windowPosition;
            var p2 = (Vector3)Event.current.mousePosition;

            if (_wiring.inlet != null)
            {
                // Draw a curve from the inlet button.
                p1 += (Vector3)_wiring.inlet.buttonRect.center;
                EditorUtility.DrawCurve(p2, p1, Color.yellow);
            }
            else
            {
                // Draw a curve from the outlet button.
                p1 += (Vector3)_wiring.outlet.buttonRect.center;
                EditorUtility.DrawCurve(p1, p2, Color.yellow);
            }

            // Request repaint continuously.
            Repaint();
        }

        #endregion

        #region Private methods

       
        // Process feedback from the leaf UI elemets.
        void ProcessUIFeedback(FeedbackQueue.RecordBase record)
        {
            // Delete request
            if (record is FeedbackQueue.DeleteNodeRecord)
            {
                var removeNode = ((FeedbackQueue.DeleteNodeRecord)record).node;
                if (Node.ActiveNode == removeNode)
                {
                    Node.ActiveNode = null;
                }
                // Remove related links.
                foreach (var node in _patch.nodeList)
                    node.RemoveLinksTo(removeNode, _patch);

                // Remove the node.
                removeNode.RemoveFromPatch(_patch);

                // Rescan the patch and repaint.
                _patch.Rescan();
                Repaint();
            }
            // Inlet button pressed
            if (record is FeedbackQueue.InletButtonRecord)
            {
                var info = (FeedbackQueue.InletButtonRecord)record;
                if (_wiring == null)
                {
                    _wiring = new WiringState(info.node, info.inlet);
                }
                else
                {
                    // Currently in wiring: try to make a link.
                    _wiring.node.TryLinkTo(_wiring.outlet, info.node, info.inlet);
                    _wiring = null;
                }
            }

            // Outlet button pressed
            if (record is FeedbackQueue.OutletButtonRecord)
            {
                var info = (FeedbackQueue.OutletButtonRecord)record;
                if (_wiring == null)
                {
                    _wiring = new WiringState(info.node, info.outlet);
                }
                else
                {
                    // Currently in wiring: try to make a link.
                    info.node.TryLinkTo(info.outlet, _wiring.node, _wiring.inlet);
                    _wiring = null;
                }
            }

            // Force to end wiring.
            //_wiring = null;
        }
        
        private Vector2 _mainViewMax = Vector2.zero;

        public void ResetPosition()
        {
            _scrollPosition = Vector2.zero;
            _mainViewMax = Vector2.zero;
            var min = Vector2.one * float.MaxValue;
            foreach (var node in _patch.nodeList)
            {
                min = Vector2.Min(min, node.windowPosition);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GUIScaleUtility.CheckInit();
        }

        void DrawMainViewGUI()
        {
            DrawBackground();
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            
            if (Event.current.button == 2 && Event.current.type == EventType.MouseDrag ||
                Event.current.button == 0 && Event.current.type == EventType.MouseDrag && Event.current.modifiers == EventModifiers.Alt)
            {
                _scrollPosition += -Event.current.delta;
                Event.current.Use();
            }
            if (Event.current.button == 1 && Event.current.type == EventType.MouseDown)
            {
                var menu = _nodeFactory.DrawMenu(_patch, Event.current.mousePosition);
                menu.DropDown(new Rect(Event.current.mousePosition, Vector2.one));
                Event.current.Use();
            }
            
            // Draw the link lines.
            if (Event.current.type == EventType.Repaint)
            {
                foreach (var node in _patch.nodeList)
                {
                    if (!node.DrawLinkLines(_patch))
                    {
                        // Request repaint if position info is not ready.
                        Repaint();
                        break;
                    }
                }
            }

            _mainViewMax = Vector2.zero;
            var mainViewMin = Vector2.one * 10000;
            // Draw all the nodes and make the bounding box.

            BeginWindows();
            var h = 0f;
            foreach (var node in _patch.nodeList)
            {
                node.DrawWindowGUI();
                _mainViewMax = Vector2.Max(_mainViewMax, node.windowPosition);
                mainViewMin = Vector2.Min(mainViewMin, node.windowPosition);
                h = Mathf.Max(h, node.LastRect.y + node.LastRect.height);
            }
            _mainViewMax.x += 200;
            mainViewMin.x -= 20;
            mainViewMin.y -= 20;
            foreach (var node in _patch.nodeList)
            {
                node.windowPosition -= mainViewMin;
            }
            EndWindows();

            var x = Mathf.Max(_mainViewMax.x * _zoom, Screen.width - 18);
            var y = Mathf.Max((h + 20) * _zoom, Screen.height - 37);
            
            //Place an empty box to expand the scroll view.
            GUILayout.Box(
                "", GUIStyle.none,
                GUILayout.Width(x),
                GUILayout.Height(y));
            // Draw working link line while wiring.
            if (_wiring != null)
                DrawWorkingLink();
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();

            // Process all the feedback from the leaf UI elements.
            while (!FeedbackQueue.IsEmpty)
                ProcessUIFeedback(FeedbackQueue.Dequeue());
        }
        
        #endregion
    }
}