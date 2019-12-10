using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fashi : UnitMain
{
    const int HP = 1;
    const int Attack = 1;
    const int AttackRange = 1;
    public override Shuxing Shuxing { get; set; } = new Fashi_class(HP, Attack, AttackRange);

    public Fashi()
    {
        base._Type = UnitType.Fashi;
    }
}

public class Fashi_class : Shuxing
{
    public Fashi_class(int a, int b, int c) : base(a, b, c)
    {
        base.Name = "法师";
    }
}