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

// Suppress "unused variable" warning messages.
#pragma warning disable 0414

using UnityEngine;
using UnityEngine.Events;
using System;

namespace Klak.Wiring
{
    // Attribute for marking inlets
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class InletAttribute : Attribute
    {
        public InletAttribute()
        {
        }
    }

    // Attribute for marking outlets
    [AttributeUsage(AttributeTargets.Field)]
    public class OutletAttribute : Attribute
    {
        public OutletAttribute()
        {
        }
    }

    // Attribute for marking outlets
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class NodeType : Attribute
    {
        public string[] Types { get; set; }

        public NodeType(params string[] types)
        {
            Types = types;
        }
    }

    public class EnumFlagAttribute : UnityEngine.PropertyAttribute
    {
        public string enumName;

        public EnumFlagAttribute()
        {
        }

        public EnumFlagAttribute(string name)
        {
            enumName = name;
        }
    }



    // Base class of wiring node classes
    public abstract class NodeBase : MonoBehaviour
    {
        public virtual bool ShowTriggerInlet
        {
            get { return true; }
        }

        public virtual bool UseAutomaticTrigger
        {
            get { return true; }
        }

        [HideInInspector] public Vector2 wiringNodePosition = uninitializedNodePosition;

        public bool isNodeActive = true;
        [EnumFlag("Automatic Trigger")] public TriggerType triggerMechanism = 0;

        [Inlet]
        public virtual void ManualTrigger()
        {
            if (isNodeActive)
                InvokeEvents();
        }

        protected virtual void InvokeEvents()
        {
        }

        protected virtual void Awake()
        {
            if (triggerMechanism == TriggerType.Awake && UseAutomaticTrigger)
            {
                ManualTrigger();
            }
        }

        protected virtual void Update()
        {
            if (triggerMechanism == TriggerType.Update && UseAutomaticTrigger)
            {
                ManualTrigger();
            }
        }

        protected virtual void LateUpdate()
        {
            if (triggerMechanism == TriggerType.LateUpdate && UseAutomaticTrigger)
            {
                ManualTrigger();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (triggerMechanism == TriggerType.FixedUpdate && UseAutomaticTrigger)
            {
                ManualTrigger();
            }
        }

        protected virtual void Start()
        {
            if (triggerMechanism == TriggerType.Start && UseAutomaticTrigger)
            {
                ManualTrigger();
            }
        }

        protected virtual void OnEnable()
        {
            if (triggerMechanism == TriggerType.Enable && UseAutomaticTrigger)
            {
                ManualTrigger();
            }
        }

        protected virtual void OnDisable()
        {
            if (triggerMechanism == TriggerType.Disable && UseAutomaticTrigger)
            {
                ManualTrigger();
            }
        }

        [Flags]
        public enum TriggerType
        {
            Awake = 1,
            Start = 2,

            Enable = 4,
            Disable = 8,

            Update = 16,
            FixedUpdate = 32,
            LateUpdate = 64,
        }

        [Serializable]
        public class VoidEvent : UnityEvent
        {
        }

        [Serializable]
        public class BoolEvent : UnityEvent<bool>
        {
        }

        [Serializable]
        public class ObjectEvent : UnityEvent<object>
        {
        }

        [Serializable]
        public class FloatEvent : UnityEvent<float>
        {
        }

        [Serializable]
        public class StringEvent : UnityEvent<string>
        {
        }

        [Serializable]
        public class IntEvent : UnityEvent<int>
        {
        }

        [Serializable]
        public class Vector3Event : UnityEvent<Vector3>
        {
        }

        [Serializable]
        public class QuaternionEvent : UnityEvent<Quaternion>
        {
        }

        [Serializable]
        public class ColorEvent : UnityEvent<Color>
        {
        }

        [Serializable]
        public class CollisionEvent : UnityEvent<Collision>
        {
        }

        [Serializable]
        public class Collision2DEvent : UnityEvent<Collision2D>
        {
        }

        [Serializable]
        public class ColliderEvent : UnityEvent<Collider>
        {
        }

        [Serializable]
        public class Collider2DEvent : UnityEvent<Collider2D>
        {
        }

        [Serializable]
        public class GameObjectEvent : UnityEvent<GameObject>
        {
        }

        [Serializable]
        public class TransformEvent : UnityEvent<Transform>
        {
        }

        [Serializable]
        public class RigidbodyEvent : UnityEvent<Rigidbody>
        {
        }

        [Serializable]
        public class Rigidbody2DEvent : UnityEvent<Rigidbody2D>
        {
        }

        [Serializable]
        public class ContactPoint2DEvent : UnityEvent<ContactPoint2D>
        {
        }

        [Serializable]
        public class BoundsEvent : UnityEvent<Bounds>
        {
        }

        static public Vector2 uninitializedNodePosition
        {
            get { return new Vector2(-1000, -1000); }
        }

        public virtual Color BackgroundNodeColor
        {
            get { return Color.white; }
        }

        public virtual Color ContentColor
        {
            get { return Color.black; }
        }
    }
}
