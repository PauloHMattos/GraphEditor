using UnityEditor;

namespace Klak.Wiring
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TimeInput))]
    public class TimeInputEditor : NodeBaseEditor
    {
        SerializedProperty _timeType;
        SerializedProperty _valueEvent;

        void OnEnable()
        {
            _timeType = serializedObject.FindProperty("_timeType");
            _valueEvent = serializedObject.FindProperty("_valueEvent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_timeType);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_valueEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }
}