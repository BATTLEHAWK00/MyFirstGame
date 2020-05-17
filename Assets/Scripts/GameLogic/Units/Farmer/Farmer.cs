using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Farmer : UnitBase
{
    void AddHolyWaterPerRound(object info)
    {
        if(GetPosition().Position.y==0)
            HolyWaterSystem.Get().AddHolyWater(1);
    }
    protected override void _Awake()
    {
        var data = TheGameCommon.JsonFunc.FromFile<ArcherData>("Prefabs/Units/UnitData/Farmer");
        EventManager.Get().AddListener<object>(EventTypes.RoundSystem_YourTurn, AddHolyWaterPerRound);
    }
    public Farmer()
    {
        base.SetUnitType("农民", UnitType.Farmer);
        base.MaxHP = 3;
        base.HP = MaxHP;
        base.Description = "农民";
        base.HolyWaterCost = 1;
    }
}
public class FarmerData
{
    public string Description;
    public int HP;
    public int Attack;
    public int AttackRange;
}