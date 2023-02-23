using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class AssemblyHelper : MonoBehaviour
{
    public static List<Type> GetClassTypesThatImplement<T>() 
        => GetTypesThat<T>(type => !type.IsInterface && !type.IsAbstract);

    public static List<Type> GetInterfacesTypesThatImplement<T>() 
        => GetTypesThat<T>(type => type.IsInterface);

    private static List<Type> GetTypesThat<T>(Func<Type, bool> method)
    {
        var list = new List<Type>();
        var typeT = typeof(T);
        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (typeT.IsAssignableFrom(type) && method(type) && typeT != type)
            {
                list.Add(type);
            }
        }

        return list;
    }
}
