using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDPanel : PanelBase
{
    private GameObject rayBlocker;
    public override void Awake()
    {
        base.Awake();
        rayBlocker = ResManager.Getinstance().Load<GameObject>("Prefabs/UI/Panels/HUDPanel/RayBlocker");
        rayBlocker.transform.SetParent(transform);
        rayBlocker.SetActive(false);
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
