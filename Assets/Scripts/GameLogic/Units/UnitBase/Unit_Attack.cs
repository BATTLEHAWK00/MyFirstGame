using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class UnitBase
{
    protected UnitAttackBase attackBase;
    public void Attack(UnitBase target,bool ignoreCheck = false, bool canFightBack=true)
    {
        attackBase.Attack(this, target,ignoreCheck,canFightBack);
    }
}