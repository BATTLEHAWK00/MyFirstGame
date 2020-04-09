using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : UnitBase
{
    protected override void _Awake()
    {
        var data = TheGameCommon.JsonFunc.FromFile<ArcherData>("Prefabs/Units/UnitData/Archer");
        Debug.Log(data.Description);
        Debug.Log(data.HP);
        Debug.Log(data.Attack);
        Debug.Log(data.AttackRange);
    }

    public Archer()
    {
        base.SetUnitType("弓箭手", UnitType.Archer);
        base._MaxHP = 3;
        base._HP = _MaxHP;
        base._Description = "弓箭手";
        base.attack = 2;
        base.attackRange = 3;
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