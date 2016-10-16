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
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;
using System.Reflection;
using System;

namespace Klak.Wiring.Patcher
{
    public static class LinkUtility
    {
        #region Public functions

        // Try to create a link between two nodes.
        // Returns true if the link is established successfully.
        public static bool TryLinkNodes(
            NodeBase nodeFrom, Outlet outlet,
            NodeBase nodeTo, MethodInfo targetMethod
            )
        {
            var triggerEvent = outlet.boundEvent;
            // Determine the type of the target action.
            var actionType = GetUnityActionToInvokeMethod(targetMethod);
            if (actionType == null) return false; // invalid target method type

            // Create an action that is bound to the target method.
            var targetAction = Delegate.CreateDelegate(
                actionType, nodeTo, targetMethod
                );

            if (triggerEvent is UnityEvent)
            {
                // The trigger event has no parameter.
                // Add the action to the event with a default parameter.
                if (actionType == typeof (UnityAction))
                {
                    UnityEventTools.AddVoidPersistentListener(
                        triggerEvent, (UnityAction) targetAction
                        );
                    return true;
                }

                // TODO - Verificar a utilizade disso
                // ------------------------------------------------------------------------
                if (actionType == typeof(UnityAction<float>))
                {
                    UnityEventTools.AddFloatPersistentListener(
                        triggerEvent, (UnityAction<float>)targetAction, 1.0f
                        );
                    return true;
                }
                if (actionType == typeof(UnityAction<bool>))
                {
                    UnityEventTools.AddBoolPersistentListener(
                        triggerEvent, (UnityAction<bool>)targetAction, false
                        );
                    return true;
                }
                if (actionType == typeof(UnityAction<int>))
                {
                    UnityEventTools.AddIntPersistentListener(
                        triggerEvent, (UnityAction<int>)targetAction, 0
                        );
                    return true;
                }
                if (actionType == typeof(UnityAction<string>))
                {
                    UnityEventTools.AddStringPersistentListener(
                        triggerEvent, (UnityAction<string>)targetAction, ""
                        );
                    return true;
                }
                // ------------------------------------------------------------------------

            }
            else if (triggerEvent is UnityEvent<bool>)
            {
                // The trigger event has a float parameter.
                // Then the target method should have a float parameter too.
                if (actionType == typeof(UnityAction<bool>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<bool>)triggerEvent,
                        (UnityAction<bool>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<float>)
            {
                // The trigger event has a float parameter.
                // Then the target method should have a float parameter too.
                if (actionType == typeof(UnityAction<float>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<float>)triggerEvent,
                        (UnityAction<float>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<int>)
            {
                // The trigger event has a float parameter.
                // Then the target method should have a float parameter too.
                if (actionType == typeof(UnityAction<int>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<int>)triggerEvent,
                        (UnityAction<int>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<string>)
            {
                // The trigger event has a float parameter.
                // Then the target method should have a float parameter too.
                if (actionType == typeof(UnityAction<string>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<string>)triggerEvent,
                        (UnityAction<string>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Vector3>)
            {
                // The trigger event has a Vector3 parameter.
                // Then the target method should have a Vector3 parameter too.
                if (actionType == typeof (UnityAction<Vector3>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Vector3>) triggerEvent,
                        (UnityAction<Vector3>) targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Quaternion>)
            {
                // The trigger event has a Quaternion parameter.
                // Then the target method should have a Quaternion parameter too.
                if (actionType == typeof (UnityAction<Quaternion>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Quaternion>) triggerEvent,
                        (UnityAction<Quaternion>) targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Color>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Color>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Color>)triggerEvent,
                        (UnityAction<Color>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<GameObject>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<GameObject>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<GameObject>)triggerEvent,
                        (UnityAction<GameObject>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Transform>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Transform>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Transform>)triggerEvent,
                        (UnityAction<Transform>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Collision>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Collision>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Collision>)triggerEvent,
                        (UnityAction<Collision>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Collider>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Collider>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Collider>)triggerEvent,
                        (UnityAction<Collider>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Rigidbody>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Rigidbody>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Rigidbody>)triggerEvent,
                        (UnityAction<Rigidbody>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Collision2D>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Collision2D>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Collision2D>)triggerEvent,
                        (UnityAction<Collision2D>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Collider2D>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Collider2D>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Collider2D>)triggerEvent,
                        (UnityAction<Collider2D>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Rigidbody2D>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Rigidbody2D>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Rigidbody2D>)triggerEvent,
                        (UnityAction<Rigidbody2D>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<ContactPoint>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<ContactPoint>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<ContactPoint>)triggerEvent,
                        (UnityAction<ContactPoint>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<ContactPoint2D>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<ContactPoint2D>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<ContactPoint2D>)triggerEvent,
                        (UnityAction<ContactPoint2D>)targetAction
                        );
                    return true;
                }
            }
            else if (triggerEvent is UnityEvent<Bounds>)
            {
                // The trigger event has a color parameter.
                // Then the target method should have a color parameter too.
                if (actionType == typeof(UnityAction<Bounds>))
                {
                    // Add the action to the event.
                    UnityEventTools.AddPersistentListener(
                        (UnityEvent<Bounds>)triggerEvent,
                        (UnityAction<Bounds>)targetAction
                        );
                    return true;
                }
            }

            if (actionType == typeof(UnityAction))
            {
                UnityEventTools.AddVoidPersistentListener(triggerEvent, (UnityAction)targetAction);
                return true;
            }
            return false; // trigger-target mismatch
        }

        // Remove a link between two nodes.
        public static void RemoveLinkNodes(
            NodeBase nodeFrom, UnityEventBase triggerEvent,
            NodeBase nodeTo, MethodInfo targetMethod
            )
        {
            var methodName = targetMethod.Name;

            var eventCount = triggerEvent.GetPersistentEventCount();
            for (var i = 0; i < eventCount; i++)
            {
                if (nodeTo == triggerEvent.GetPersistentTarget(i) &&
                    methodName == triggerEvent.GetPersistentMethodName(i))
                {
                    UnityEventTools.RemovePersistentListener(triggerEvent, i);
                    break;
                }
            }
        }

        #endregion

        #region Private functions
        
        // Returns a UnityAction type that can be used to call the given method.
        private static Type GetUnityActionToInvokeMethod(MethodInfo method)
        {
            var args = method.GetParameters();

            // The method has no parameter: Use UnityAction.
            if (args.Length == 0) return typeof (UnityAction);

            // Only refer to the first parameter.
            var paramType = args[0].ParameterType;
            
            // Returns one of the corrensponding action types.
            if (paramType == typeof(void)) return typeof(UnityAction);
            if (paramType == typeof (bool)) return typeof (UnityAction<bool>);
            if (paramType == typeof (float)) return typeof (UnityAction<float>);
            if (paramType == typeof (int)) return typeof (UnityAction<int>);
            if (paramType == typeof (string)) return typeof (UnityAction<string>);
            if (paramType == typeof (Vector3)) return typeof (UnityAction<Vector3>);
            if (paramType == typeof (Quaternion)) return typeof (UnityAction<Quaternion>);
            if (paramType == typeof (Color)) return typeof (UnityAction<Color>);

            if (paramType == typeof(GameObject)) return typeof(UnityAction<GameObject>);
            if (paramType == typeof(Transform)) return typeof(UnityAction<Transform>);

            if (paramType == typeof(Collider)) return typeof(UnityAction<Collider>);
            if (paramType == typeof(Collider2D)) return typeof(UnityAction<Collider2D>);
            if (paramType == typeof(Collision)) return typeof(UnityAction<Collision>);
            if (paramType == typeof(Collision2D)) return typeof(UnityAction<Collision2D>);
            if (paramType == typeof(Rigidbody)) return typeof(UnityAction<Rigidbody>);
            if (paramType == typeof(Rigidbody2D)) return typeof(UnityAction<Rigidbody2D>);
            if (paramType == typeof(ContactPoint)) return typeof(UnityAction<ContactPoint>);
            if (paramType == typeof(ContactPoint2D)) return typeof(UnityAction<ContactPoint2D>);
            if (paramType == typeof(Bounds)) return typeof(UnityAction<Bounds>);

            if (paramType == typeof (object)) return typeof (UnityAction<object>);

            // No one matches the method type.
            return null;
        }

        #endregion

    }
}