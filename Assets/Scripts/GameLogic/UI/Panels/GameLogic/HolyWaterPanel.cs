using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HolyWaterPanel : MonoBehaviour
{
    private UnityEngine.UI.Text text;
    void Awake()
    {
        HolyWaterChanged(null);
        EventManager.Getinstance().AddListener<object>(EventTypes.HolyWater_OnChanged, HolyWaterChanged);
    }
    void HolyWaterChanged(object info)
    {
        if (text == null)
            text = GetComponentInChildren<UnityEngine.UI.Text>();
        text.text="圣水:"+ HolyWaterSystem.Getinstance().HolyWater.ToString();
    }
}
