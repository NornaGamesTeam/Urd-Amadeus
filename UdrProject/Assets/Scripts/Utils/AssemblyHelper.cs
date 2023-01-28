using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AssemblyHelper : MonoBehaviour
{
    public static List<Type> GetTypesThatImplement<T>()
    {
        var list = new List<Type>();
        var typeT = typeof(T);
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (typeT.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                list.Add(type);
            }
        }

        return list;
    }
}
