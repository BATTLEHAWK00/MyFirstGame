using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : UnitBase
{
    public Archer()
    {
        base.SetUnitType("弓箭手", UnitType.Archer);
        base._HP = 3;
        base._Description = "弓箭手";
    }
}
