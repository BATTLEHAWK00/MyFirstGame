using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    public UILayers Layer { get; protected set; } = UILayers.Mid;
    public virtual void Awake() 
    {
        CanvasGroup canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    public virtual void OnEnter() { }
    public virtual void OnExit() { Destroy(gameObject); }
    public virtual void OnPause() 
    {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
            canvasGroup.blocksRaycasts = false;
    }
    public virtual void OnResume() 
    {
        Debug.Log(this.GetType().Name + " Resumed");
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if(canvasGroup!=null)
            canvasGroup.blocksRaycasts = true;
    }
}
