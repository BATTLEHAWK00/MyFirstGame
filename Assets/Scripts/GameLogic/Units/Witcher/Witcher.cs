using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witcher : UnitBase
{
    public Witcher()
    {
        base.SetUnitType("法师", UnitType.Witcher);
        base.MaxHP = 3;
        base.HP = MaxHP;
        base.Description = "法师";
    }
}
