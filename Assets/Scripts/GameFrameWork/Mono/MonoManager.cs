using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : MonoBehaviour
{
    private UnityAction action_OnUpdate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(action_OnUpdate!=null)
            action_OnUpdate.Invoke();
    }
    public void AddUpdateListener(UnityAction action)
    {
        if (action != null)
            action_OnUpdate += action;
        else
            Debug.LogError("传入了空的UpdateAction!");
    }
    public void RunDelayTask(UnityAction unityAction,float seconds)
    {
        StartCoroutine(delayTask(unityAction,seconds));
    }
    IEnumerator delayTask(UnityAction unityAction,float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(unityAction!=null)
            unityAction();
    }
    public void RemoveUpdateListener(UnityAction action)
    {
        if (action != null)
            action_OnUpdate -= action;
        else
            Debug.LogError("传入了空的UpdateAction!");
    }
}
public class MonoBase : BaseManager<MonoBase>
{
    private MonoManager mono;
    public MonoManager GetMono() { return mono; }
    public MonoBase()
    {
        GameObject gameObject = new GameObject();
        gameObject.name = "MonoManager";
        mono = gameObject.AddComponent<MonoManager>();
        GameObject.DontDestroyOnLoad(mono);
    }
}