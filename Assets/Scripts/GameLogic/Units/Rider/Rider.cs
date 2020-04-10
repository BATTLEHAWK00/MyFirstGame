using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : UnitBase
{
    public Rider()
    {
        base.SetUnitType("骑士", UnitType.Rider);
        base.MaxHP = 10;
        base.HP = MaxHP;
        base.Description = "骑士";
        base.HolyWaterCost = 2;
    }
}
