using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class PanelBase : MonoBehaviour
{
    public UILayers Layer { get; protected set; } = UILayers.Mid;
    public virtual void Awake() 
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    public virtual void OnEnter() 
    {
        Vector2 po = transform.position;
        po.y -= Screen.currentResolution.height;
        transform.position = po;
        transform.DOMoveY(transform.position.y + Screen.currentResolution.height, 0.5f).SetEase(Ease.OutBack);
    }
    public virtual void OnExit() 
    {
        transform.DOMoveY((transform as RectTransform).anchoredPosition.y+Screen.currentResolution.height,0.25f).SetEase(Ease.InQuad).OnComplete(()=> {
            Destroy(gameObject);
        });
    }
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
