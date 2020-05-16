using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager<T> where T:new ()
{ 
    private static T instance; 
    public static void Destroy()
    {
        instance = default(T);
    }
    public static T Getinstance()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
}
