using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HUDPanel : PanelBase
{
    private GameObject rayBlocker;
    public override void Awake()
    {
        base.Awake();
        rayBlocker = ResManager.Get().Load<GameObject>("Prefabs/UI/Panels/HUDPanel/RayBlocker");
        rayBlocker.transform.SetParent(transform);
        rayBlocker.SetActive(false);
    }
    public override void OnEnter() 
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        DOTween.To(
            () => GetComponent<CanvasGroup>().alpha,
            (x) => GetComponent<CanvasGroup>().alpha = x,
            1f,
            2f
        );
    }
    public override void OnExit() { }
    public override void OnPause()
    {
        rayBlocker.SetActive(true);
    }
    public override void OnResume()
    {
        rayBlocker.SetActive(false);
    }
    public HUDPanel()
    {
        Layer = UILayers.HUD;
    }
}
