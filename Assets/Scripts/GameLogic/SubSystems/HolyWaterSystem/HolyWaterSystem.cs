using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HolyWaterSystem : BaseManager<HolyWaterSystem>
{ 
    public int HolyWater { get; private set; } = 0;
    public bool CostHolyWater(int amount)
    {
        if(HolyWater<amount)
        { 
            UIManager.Get().MsgOnScreen("圣水不足!");
            AudioManager.Get().PlaySound("GameMain/OnFail", 0.3f);
            return false; 
        }
        HolyWater -= amount;
        UIManager.Get().MsgOnScreen(string.Format("花费了{0}点圣水", amount));
        EventManager.Get().EventTrigger<object>(EventTypes.HolyWater_OnChanged, null);
        return true;
    }
    public void AddHolyWater(int amount)
    {
        HolyWater += amount;
        EventManager.Get().EventTrigger(EventTypes.HolyWater_OnChanged);
    }
    void AddHolyWaterPerRound(object info)
    {
        AddHolyWater(3);
        int cnt = 0;
        foreach(var i in GameGlobal.Get().GameMain.GridSystem.CubeCells)
        {
            if (i.CurrentUnit != null)
                cnt++;
        }
        if (cnt == 0)
            AddHolyWater(1);
    }
    public void Init()
    {
        EventManager.Get().AddListener<object>(EventTypes.RoundSystem_YourTurn,AddHolyWaterPerRound);
    }
}
