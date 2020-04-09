using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancer : UnitBase
{
    public Lancer()
    {
        base.SetUnitType("长矛手", UnitType.Lancer);
        base._MaxHP = 3;
        base._HP = _MaxHP;
        base._Description = "长矛手";
    }
}
