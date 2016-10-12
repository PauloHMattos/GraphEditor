using UnityEditor;

namespace Klak.Wiring
{
    [CustomEditor(typeof (Rigidbody2DSelector))]
    public class Rigidbody2DSelectorEditor : NodeBaseEditor
    {
        SerializedProperty _rigidbody;

        protected override void OnEnable()
        {
            base.OnEnable();
            _rigidbody = serializedObject.FindProperty("_rigidbody");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(_rigidbody);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
