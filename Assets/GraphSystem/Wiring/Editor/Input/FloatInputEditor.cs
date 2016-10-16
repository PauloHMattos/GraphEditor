//using UnityEditor;

//namespace Klak.Wiring
//{
//    [CanEditMultipleObjects]
//    [CustomEditor(typeof(FloatInput))]
//    public class FloatInputEditor : NodeBaseEditor
//    {
//        SerializedProperty _value;
//        SerializedProperty _interpolator;
//        SerializedProperty _valueEvent;

//        void OnEnable()
//        {
//            _value = serializedObject.FindProperty("_value");
//            _interpolator = serializedObject.FindProperty("_interpolator");
//            _valueEvent = serializedObject.FindProperty("_valueEvent");
//        }

//        public override void OnInspectorGUI()
//        {
//            serializedObject.Update();

//            EditorGUILayout.PropertyField(_value);
//            EditorGUILayout.PropertyField(_interpolator);
//            EditorGUILayout.Space();

//            EditorGUILayout.PropertyField(_valueEvent);

//            serializedObject.ApplyModifiedProperties();
//        }
//    }
//}