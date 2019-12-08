using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fashi : UnitMain
{
    const uint HP = 1;
    const uint Attack = 1;
    const uint AttackRange = 1;
    public override Shuxing Shuxing { get; set; } = new Fashi_class(HP, Attack, AttackRange);

    //public Fashi()
    //{
    //    base.Shuxing = Shuxing;
    //}
}

public class Fashi_class : Shuxing
{
    public Fashi_class(uint a, uint b, uint c) : base(a, b, c)
    {
        base.Name = "法师";
    }
}