using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common
{
    public static void Swap<T>(ref T a, ref T b)    //交换方法
    {
        T t = a;
        a = b;
        b = t;
    }
    /*
    public static void Swap(ref object a,ref object b)
    {
        object t = a;
        a = b;
        b = t;
    }
    */
}
