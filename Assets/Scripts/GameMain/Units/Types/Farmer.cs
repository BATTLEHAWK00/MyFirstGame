using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : UnitMain
{
    const int HP = 1;
    const int Attack = 1;
    const int AttackRange = 1;
    public override Shuxing Shuxing { get; set; } = new Farmer_class(HP, Attack, AttackRange);

    public Farmer()
    {
        base._Type = UnitType.Farmer;
    }
}

public class Farmer_class : Shuxing
{

    public Farmer_class(int a, int b, int c) : base(a, b, c)
    {
        base.Name = "农民";
    }
}
