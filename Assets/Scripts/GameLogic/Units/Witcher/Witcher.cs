using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witcher : UnitBase
{
    public Witcher()
    {
        base.SetUnitType("法师", UnitType.Witcher);
        base._HP = 3;
        base._Description = "法师";
    }
}
