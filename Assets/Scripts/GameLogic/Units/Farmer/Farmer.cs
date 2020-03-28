using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : UnitBase
{
    public Farmer()
    {
        base.SetUnitType("农民", UnitType.Farmer);
        base._HP = 3;
        base._Description = "农民";
    }
}
