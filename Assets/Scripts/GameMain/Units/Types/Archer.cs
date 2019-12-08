using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : UnitMain
{
    const uint HP = 1;
    const uint Attack = 1;
    const uint AttackRange = 1;
    //public Archer_class Shuxing = new Archer_class(HP, Attack, AttackRange) ;
    public override Shuxing Shuxing { get; set; } = new Archer_class(HP, Attack, AttackRange);
    //public Archer()
    //{
    //    base.Shuxing = Shuxing;
    //}
}
public class Archer_class : Shuxing
{
    
    public Archer_class(uint a, uint b, uint c) : base(a, b, c)
    {
        base.Name = "弓箭手";
    }
}
