using UnityEditor;

namespace Klak.Wiring
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Rigidbody2DOut))]
    public class Rigidbody2DOutEditor : NodeBaseEditor
    {
        SerializedProperty _targetRigidbody;

        protected override void OnEnable()
        {
            base.OnEnable();
            _targetRigidbody = serializedObject.FindProperty("_targetRigidbody");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(_targetRigidbody);

            serializedObject.ApplyModifiedProperties();
        }
    }
}