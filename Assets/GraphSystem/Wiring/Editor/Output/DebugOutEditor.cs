using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Klak.Wiring
{
    [CustomEditor(typeof(ColorOut))]
    public class DebugOutEditor : NodeBaseEditor
    {
        SerializedProperty _debugType;

        string[] _propertyList; // cached property list
        Type _cachedType;       // cached property type info

        void OnEnable()
        {
            _debugType = serializedObject.FindProperty("_debugType");
        }

        void OnDisable()
        {
            _propertyList = null;
        }

        // Check if a given property is capable of being a target.
        bool IsTargetable(PropertyInfo info)
        {
            return info.GetSetMethod() != null &&
                   info.PropertyType == typeof(Color);
        }

        // Cache properties of a given type if it's
        // different from a previously given type.
        void CachePropertyList(Type type)
        {
            if (_cachedType == type) return;

            _propertyList = type.GetProperties().
                Where(x => IsTargetable(x)).Select(x => x.Name).ToArray();

            _cachedType = type;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_debugType);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}