using UnityEditor;
using UnityEngine;

namespace Klak.Wiring.Patcher
{
    public class PatcherInspectorWindow : PatcherWindow
    {
        private Vector2 _scrollSide;
        private PatcherMainWindow _patcherMainWindow;

        protected override void OnEnable()
        {
            base.OnEnable();
            minSize = new Vector2(250, 0);
        }

        protected override void DrawGUI()
        {
            // Determine the active node.
            EditorGUIUtility.wideMode = true;
            EditorGUILayout.BeginVertical();
            _scrollSide = EditorGUILayout.BeginScrollView(_scrollSide);

            if (Node.ActiveNode == null)
            {
                if (NodeLink.SelectedLink != null)
                {
                    DrawNodeLinkInspector();
                }
            }
            else
            {
                DrawNodeInspector();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
        
        private void DrawNodeLinkInspector()
        {
            var nodeLink = NodeLink.SelectedLink;
            if (nodeLink == null)
            {
                return;
            }

            EditorGUILayout.LabelField(NodeLink.SelectedLinkId.ToString());
            EditorGUILayout.LabelField(nodeLink.fromNode.displayName);
            EditorGUILayout.LabelField(nodeLink.fromOutlet.displayName);
            EditorGUILayout.LabelField(nodeLink.toNode.displayName);
            EditorGUILayout.LabelField(nodeLink.toInlet.displayName);
        }

        private void DrawNodeInspector()
        {
            var activeNode = Node.ActiveNode;
            // Destroy the previous property editor if it's not needed.
            if (_propertyEditor != null)
            {
                var targetNodeInstance = (NodeBase)_propertyEditor.target;
                if (activeNode != null && !activeNode.IsRepresentationOf(targetNodeInstance))
                {
                    DestroyImmediate(_propertyEditor);
                    _propertyEditor = null;

                    // This is needed to clear the UnityEventDrawer cache.
                    EditorUtility.ClearPropertyDrawerCache();
                }
            }

            //EditorGUIUtility.labelWidth = 150;
            // Name field
            if (activeNode != null)
            {
                EditorGUILayout.LabelField("Node Attributes", EditorStyles.boldLabel);
                activeNode.DrawNameFieldGUI();
                EditorGUILayout.Space();

                // Show the property editor.
                EditorGUILayout.LabelField(activeNode.typeName + " Properties", EditorStyles.boldLabel);

                if (_propertyEditor == null)
                    _propertyEditor = activeNode.CreateEditor();

                _propertyEditor.OnInspectorGUI();
            }
        }
    }
}