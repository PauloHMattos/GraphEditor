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

using UnityEditor;

namespace Klak.Wiring
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(AxisInput))]
    public class AxisInputEditor : NodeBaseEditor
    {
        SerializedProperty _axisName;
        SerializedProperty _interpolator;
        SerializedProperty _multiply;
        SerializedProperty _multiplyValue;
        SerializedProperty _valueEvent;

        protected override void OnEnable()
        {
            base.OnEnable();
            _axisName = serializedObject.FindProperty("_axisName");
            _interpolator = serializedObject.FindProperty("_interpolator");
            _valueEvent = serializedObject.FindProperty("_valueEvent");
            _multiply = serializedObject.FindProperty("_multiply");
            _multiplyValue = serializedObject.FindProperty("_multiplyValue");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(_axisName);
            EditorGUILayout.PropertyField(_interpolator);
            EditorGUILayout.PropertyField(_multiply);
            EditorGUILayout.PropertyField(_multiplyValue);

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_valueEvent);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
