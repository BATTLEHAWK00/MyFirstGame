using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterSystem : BaseManager<HolyWaterSystem>
{
    public int HolyWater { get; private set; } = 0;
    public bool CostHolyWater(int amount)
    {
        if(HolyWater<amount)
        { UIManager.Getinstance().MsgOnScreen("圣水不足!");return false; }
        HolyWater -= amount;
        UIManager.Getinstance().MsgOnScreen(string.Format("花费了{0}点圣水", amount));
        EventManager.Getinstance().EventTrigger<object>("HolyWater_OnChanged", null);
        return true;
    }
    public void AddHolyWater(int amount)
    {
        HolyWater += amount;
        EventManager.Getinstance().EventTrigger("HolyWater_OnChanged");
    }
    void AddHolyWaterPerRound(object info)
    {
        AddHolyWater(3);
        int cnt = 0;
        foreach(var i in TheGame.Getinstance().GameMain.GridSystem.CubeCells)
        {
            if (i.CurrentUnit != null)
                cnt++;
        }
        if (cnt == 0)
            AddHolyWater(1);
    }
    public void Init()
    {
        UIManager.Getinstance().PushPanel(PanelTypes.HolyWaterPanel);
        EventManager.Getinstance().AddListener<object>("RoundSystem_YourTurn",AddHolyWaterPerRound);
    }
}
