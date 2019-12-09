using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerateUnit
{
    public void Generate(UnitTypes unitType)
    {
        if (Game.NewObject == null)
        {
            Game.NewObject = GameObject.Instantiate(Resources.Load<GameObject>(unitType.PrefabLocation));
            GameFunc.GetObjectShuxing(Game.NewObject).isBeingPlaced = true;
            Game.GameSystemCurrent.IsPlacing = true;
        }
    }
    public void Place()
    {
        GameFunc.GetObjectShuxing(Game.NewObject).isBeingPlaced = false;
        Game.GameSystemCurrent.IsPlacing = false;
        GameFunc.GetObjectShuxing(Game.NewObject).BelongToWho = Game.GameSystemCurrent.Side;
        Game.NewObject = null;
    }
}
public class UnitTypes
{
    public UnitType unitType;
    public string PrefabLocation;
    public enum UnitType
    {
        Archer,
        Changmaoshou,
        Farmer,
        Fashi,
        Rider
    }
    public UnitTypes(UnitType unitType)
    {
        this.unitType = unitType;
        switch (unitType)
        {
            case UnitType.Archer: this.PrefabLocation = "Prefabs/Units/archer";break;
            case UnitType.Changmaoshou: this.PrefabLocation = "Prefabs/Units/changmaoshou"; break;
            case UnitType.Farmer: this.PrefabLocation = "Prefabs/Units/archer"; break;
            case UnitType.Fashi: this.PrefabLocation = "Prefabs/Units/fashi"; break;
            case UnitType.Rider: this.PrefabLocation = "Prefabs/Units/rider"; break;
        }
    }
}