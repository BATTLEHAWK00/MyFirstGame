using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterPanel : PanelBase
{
    private UnityEngine.UI.Text text;
    private void Awake()
    {
        HolyWaterChanged(null);
    }
    void HolyWaterChanged(object info)
    {
        if (text == null)
            text = GetComponentInChildren<UnityEngine.UI.Text>();
        text.text="圣水:"+ HolyWaterSystem.Getinstance().HolyWater.ToString();
    }
    public HolyWaterPanel()
    {
        base.Layer = UILayers.HUD;
        EventManager.Getinstance().AddListener<object>("HolyWater_OnChanged", HolyWaterChanged);
    }
}
