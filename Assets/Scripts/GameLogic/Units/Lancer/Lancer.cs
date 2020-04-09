using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancer : UnitBase
{
    public Lancer()
    {
        base.SetUnitType("长矛手", UnitType.Lancer);
        base.MaxHP = 3;
        base.HP = MaxHP;
        base.Description = "长矛手";
    }
}
