using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : UnitMain
{
    const int HP = 1;
    const int Attack = 1;
    const int AttackRange = 1;
    public override Shuxing Shuxing { get; set; } = new Rider_class(HP, Attack, AttackRange);
    public Rider()
    {
        base._Type = UnitType.Rider;
    }
}

public class Rider_class : Shuxing
{

    public Rider_class(int a, int b, int c) : base(a, b, c)
    {
        base.Name = "骑士";
    }
}