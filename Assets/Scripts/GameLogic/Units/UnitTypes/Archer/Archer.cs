using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : UnitBase
{
    protected override void _Awake()
    {
        var data = TheGameCommon.JsonFunc.FromFile<ArcherData>("Prefabs/Units/UnitData/Archer");
        /*
        Debug.Log(data.Description);
        Debug.Log(data.HP);
        Debug.Log(data.Attack);
        Debug.Log(data.AttackRange);*/
    }

    public Archer()
    {
        base.SetUnitType("弓箭手", UnitType.Archer);
        base.MaxHP = 3;
        base.HP = MaxHP;
        base.Description = "弓箭手";
        base.Damage = 2;
        base.AttackRange = 3;
        base.HolyWaterCost = 1;
        base.attackBase = new LongRangeAttack();
    }
}
[System.Serializable]
public class ArcherData
{
    public string Description;
    public int HP;
    public int Attack;
    public int AttackRange;
}