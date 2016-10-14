using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class Extensions
{
    public static List<MethodInfo> GetMethods(this MonoBehaviour mb, Type returnType, Type[] paramTypes, BindingFlags flags)
    {
        return mb.GetType().GetMethods(flags)
            .Where(m => m.ReturnType == returnType)
            .Select(m => new { m, Params = m.GetParameters() })
            .Where(x =>
            {
                return paramTypes == null ? // in case we want no params
                 x.Params.Length == 0 :
                 x.Params.Length == paramTypes.Length &&
                 x.Params.Select(p => p.ParameterType).ToArray().IsEqualTo(paramTypes);
            })
            .Select(x => x.m)
            .ToList();
    }
    public static List<PropertyInfo> GetProperties(this MonoBehaviour mb, Type propertyType, BindingFlags flags)
    {
        return mb.GetType().GetProperties(flags)
            .Where(m => m.PropertyType == propertyType)
            .ToList();
    }

    public static List<MethodInfo> GetMethods(this GameObject go, Type returnType, Type[] paramTypes, BindingFlags flags)
    {
        var mbs = go.GetComponents<MonoBehaviour>();
        List<MethodInfo> list = new List<MethodInfo>();
        foreach (var mb in mbs)
        {
            list.AddRange(mb.GetMethods(returnType, paramTypes, flags));
        }
        return list;
    }

    public static List<PropertyInfo> GetProperties(this GameObject go, Type propertyType, BindingFlags flags)
    {
        var mbs = go.GetComponents<MonoBehaviour>();
        List<PropertyInfo> list = new List<PropertyInfo>();
        foreach (var mb in mbs)
        {
            list.AddRange(mb.GetProperties(propertyType, flags));
        }
        return list;
    }

    public static bool IsEqualTo<T>(this IList<T> list, IList<T> other)
    {
        if (list.Count != other.Count) return false;
        for (int i = 0, count = list.Count; i < count; i++)
        {
            if (!list[i].Equals(other[i]))
            {
                return false;
            }
        }
        return true;
    }
}

public class GridMovement : MonoBehaviour
{
    public Vector2 direction;
    public Vector2 gridSize = Vector2.one;
    public float moveInterval = 0.3f;

    protected virtual void Start ()
    {
        InvokeRepeating("Move", 0f, moveInterval);
    }

    void Update()
    {
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
            direction = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            direction = -Vector2.up;
        else if (Input.GetKey(KeyCode.LeftArrow))
            direction = -Vector2.right;
        else if (Input.GetKey(KeyCode.UpArrow))
            direction = Vector2.up;
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.Scale(direction, gridSize));
    }
}
