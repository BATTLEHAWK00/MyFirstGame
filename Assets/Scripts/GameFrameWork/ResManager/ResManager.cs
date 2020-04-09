﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResManager : BaseManager<ResManager>   //资源管理模块
{
    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="name">资源路径</param>
    /// <returns></returns>
    public T Load<T>(string name) where T:Object
    {
        T obj = Resources.Load<T>(name);
        if(obj==null)
        {
            Debug.LogError(string.Format("同步加载{0}失败!",name));
            return null;
        }
        if (obj is GameObject)
            return GameObject.Instantiate(obj);
        return obj;
    }
    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="name">资源路径</param>
    /// <param name="callback">回调委托方法</param>
    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        //开启异步加载协程
        Mono.Getinstance().GetMono().StartCoroutine(LoadAsyncCoroutine<T>(name, callback));
    }
    IEnumerator LoadAsyncCoroutine<T>(string name,UnityAction<T> callback) where T: Object
    {
        ResourceRequest request = Resources.LoadAsync<T>(name);
        yield return request;
        if (request.asset == null)
        {
            Debug.LogError(string.Format("异步加载{0}失败!", name));
            yield return null;
        }
        if (request.asset is GameObject)
            callback(GameObject.Instantiate(request.asset) as T);
        else
            callback(request.asset as T);
        //Debug.Log(string.Format("异步加载{0}成功!", name));
    }
}
