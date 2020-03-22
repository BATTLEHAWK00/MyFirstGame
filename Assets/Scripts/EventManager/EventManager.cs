using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

interface IGameEventInfo { }
public class Game_Event<T>:IGameEventInfo
{
    private UnityAction<T> actions;
    public Game_Event(UnityAction<T> action) { actions += action; }
    public void Add(UnityAction<T> action)
    {
        if (action != null)
            actions += action;
        else
            Debug.LogError("[事件]事件action为空!");
    }
    public void Remove(UnityAction<T> action)
    {
        if (action != null)
            actions -= action;
        else
            Debug.LogError("[事件]事件action为空!");
    }
    public UnityAction<T> Get() { return actions; }
}
/// <summary>
/// 事件管理器
/// </summary>
public class EventManager: BaseManager <EventManager>
{
    private Dictionary<string, IGameEventInfo> EventDic = new Dictionary<string, IGameEventInfo>();
    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">事件动作</param>
    public void AddListener<T>(string name,UnityAction<T> action)
    {
        if (EventDic.ContainsKey(name))
            (EventDic[name] as Game_Event<T>).Add(action);
        else
        {
            EventDic.Add(name,new Game_Event<T>(action));
            //Debug.Log("[事件]添加事件:" + name);
        } 
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="info">参数</param>
    public void EventTrigger<T>(string name,T info)
    {
        if (EventDic.ContainsKey(name))
        {
            Debug.Log("[事件]触发事件:" + name);
            (EventDic[name] as Game_Event<T>).Get().Invoke(info);
        }
        else
            Debug.LogWarning(string.Format("[事件]事件名({0})被触发,但无监听对象!", name));
    }
    /// <summary>
    /// 触发事件
    /// </summary>
    /// <param name="name">事件名</param>
    public void EventTrigger(string name)
    {
        if (EventDic.ContainsKey(name))
        {
            Debug.Log("[事件]触发事件:" + name);
            (EventDic[name] as Game_Event<object>).Get().Invoke(null);
        }
        else
            Debug.LogWarning(string.Format("[事件]事件名({0})被触发,但无监听对象!", name));
    }
    /// <summary>
    /// 删除事件
    /// </summary>
    /// <param name="name">事件名</param>
    public void RemoveListener(string name)
    {
        if (EventDic.ContainsKey(name))
        {
            EventDic.Remove(name);
            //Debug.Log("[事件]删除事件:" + name);
        }
        else
            Debug.LogError(string.Format("[事件]事件名({0})不存在!", name));
    }
    public void RemoveListener<T>(string name,UnityAction<T> action)
    {
        if (EventDic.ContainsKey(name))
        {
            (EventDic[name] as Game_Event<T>).Remove(action);
            //Debug.Log("[事件]删除事件:" + name);
        }
        else
            Debug.LogError(string.Format("[事件]事件名({0})不存在!", name));
    }
}
