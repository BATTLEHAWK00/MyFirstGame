using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager<T> where T:new ()
{ 
    private static T instance; 
    public static void Destroy()
    {
        instance = default;
    }
    public static T Get()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
}
