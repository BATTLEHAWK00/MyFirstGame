using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : UnitMain
{
    const int HP = 1;
    const int Attack = 1;
    const int AttackRange = 1;
    //public Archer_class Shuxing = new Archer_class(HP, Attack, AttackRange) ;
    public override Shuxing Shuxing { get; set; } = new Archer_class(HP, Attack, AttackRange);
    public Archer()
    {
        base._Type = UnitType.Archer;
    }
}
public class Archer_class : Shuxing
{
    
    public Archer_class(int a, int b, int c) : base(a, b, c)
    {
        base.Name = "弓箭手";
    }
}
