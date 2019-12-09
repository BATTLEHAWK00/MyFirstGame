using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : UnitMain
{
    const uint HP = 1;
    const uint Attack = 1;
    const uint AttackRange = 1;
    public override Shuxing Shuxing { get; set; } = new Farmer_class(HP, Attack, AttackRange);

    public Farmer()
    {
        base._Type = UnitType.Farmer;
    }
}

public class Farmer_class : Shuxing
{

    public Farmer_class(uint a, uint b, uint c) : base(a, b, c)
    {
        base.Name = "农民";
    }
}
