using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changmaoshou : UnitMain
{
    const int HP = 1;
    const int Attack = 1;
    const int AttackRange = 1;
    public override Shuxing Shuxing { get; set; } = new Changmaoshou_class(HP, Attack, AttackRange);
    public Changmaoshou()
    {
        base._Type = UnitType.Changmaoshou;
    }
}

public class Changmaoshou_class : Shuxing
{
    public Changmaoshou_class(int a, int b, int c) : base(a, b, c)
    {
        base.Name = "长矛手"; 
    }
}
