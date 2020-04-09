using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : UnitBase
{
    protected override void _Awake()
    {
        var data = TheGameCommon.JsonFunc.FromFile<ArcherData>("Prefabs/Units/UnitData/Farmer");
        Debug.Log(data.Description);
        Debug.Log(data.HP);
        Debug.Log(data.Attack);
        Debug.Log(data.AttackRange);
    }
    public Farmer()
    {
        base.SetUnitType("农民", UnitType.Farmer);
        base._MaxHP = 3;
        base._HP = _MaxHP;
        base._Description = "农民";
    }
}
public class FarmerData
{
    public string Description;
    public int HP;
    public int Attack;
    public int AttackRange;
}