using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ReflectionTest
{
    public static void PrintFields(Type type)
    {
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);        

        foreach(FieldInfo field in fields)
        {
            Debug.Log(field.FieldType.Name);
            Debug.Log(field.Name);
        }

    }

    public static void PrintMethods(Type type)
    {
        MethodInfo[] methods = type.GetMethods();
       
        foreach (MethodInfo method in methods)
        {
            String accessLevel = "public";
            if (method.IsPrivate) accessLevel = "private";

            Debug.Log(accessLevel);
            Debug.Log(method.ReturnType);
            Debug.Log(method.Name);            
        }

    }


}
