using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TheGameCommon {
    public static class Common
    {
        public static void Swap<T>(ref T a, ref T b)    //交换方法
        {
            T t = a;
            a = b;
            b = t;
        }
        public static void Swap<T>(T a, T b)    //交换方法
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
    public static class JsonFunc
    {
        [System.Serializable]
        private class JsonList<T>
        {
            public List<T> jsonList=null;
        }
        public static List<T> ListFromFile<T>(string name)
        {
            TextAsset json = ResManager.Getinstance().Load<TextAsset>(name);
            JsonList<T> jsonObject = JsonUtility.FromJson<JsonList<T>>(json.text);
            return jsonObject.jsonList;
        }
        public static T FromFile<T>(string name)
        {
            TextAsset json = ResManager.Getinstance().Load<TextAsset>(name);
            T jsonObject = JsonUtility.FromJson<T>(json.text);
            return jsonObject;
        }
    }
}