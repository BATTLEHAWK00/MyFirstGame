using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changmaoshou : UnitMain
{
    const uint HP = 1;
    const uint Attack = 1;
    const uint AttackRange = 1;
    public override Shuxing Shuxing { get; set; } = new Changmaoshou_class(HP, Attack, AttackRange);
    //public Changmaoshou()
    //{
    //    base.Shuxing = Shuxing;
    //}
}

public class Changmaoshou_class : Shuxing
{
    public Changmaoshou_class(uint a, uint b, uint c) : base(a, b, c)
    {
        base.Name = "长矛手"; 
    }
}
