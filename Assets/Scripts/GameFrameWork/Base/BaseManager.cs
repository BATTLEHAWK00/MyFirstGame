using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseManager<T> where T:new ()
{ 
    private static T instance; 
    public static void Destroy()
    {
        if(instance != null)
            (instance as BaseManager<T>).onDestroy();
        instance = default;
    }
    public void ForceDestroy()
    {
        Debug.Log(this.GetType().Name +"销毁");
        Destroy();
    }
    protected virtual void onDestroy() { }
    public static T Get()
    {
        if (instance == null)
            instance = new T();
        return instance;
    }
}
