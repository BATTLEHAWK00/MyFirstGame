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
        action_OnUpdate.Invoke();
    }
    public void AddUpdateListener(UnityAction action)
    {
        if (action != null)
            action_OnUpdate += action;
        else
            Debug.LogError("传入了空的UpdateAction!");
    }
    public void RemoveUpdateListener(UnityAction action)
    {
        if (action != null)
            action_OnUpdate -= action;
        else
            Debug.LogError("传入了空的UpdateAction!");
    }
}
