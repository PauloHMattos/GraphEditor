using UnityEditor;

namespace Klak.Wiring
{
    [CustomEditor(typeof (NodeBase))]
    public class NodeBaseEditor : Editor
    {
        private NodeBase _nodeBase;
        SerializedProperty isNodeActive;
        SerializedProperty triggerMechanism;

        protected virtual void OnEnable()
        {
            _nodeBase = (NodeBase) target;
            isNodeActive = serializedObject.FindProperty("isNodeActive");
            triggerMechanism = serializedObject.FindProperty("triggerMechanism");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(isNodeActive);
            if(_nodeBase.UseAutomaticTrigger)
                EditorGUILayout.PropertyField(triggerMechanism);
            serializedObject.ApplyModifiedProperties();
        }
    }
}