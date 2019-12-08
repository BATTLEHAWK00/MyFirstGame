using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rider : UnitMain
{
    const uint HP = 1;
    const uint Attack = 1;
    const uint AttackRange = 1;
    public override Shuxing Shuxing { get; set; } = new Rider_class(HP, Attack, AttackRange);
    //public Rider()
    //{
    //    base.Shuxing = Shuxing;
    //}
}

public class Rider_class : Shuxing
{

    public Rider_class(uint a, uint b, uint c) : base(a, b, c)
    {
        base.Name = "骑士";
    }
}