using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public enum UnitType
{
    Archer, //弓箭手
    Farmer, //农民
    Lancer, //长矛手
    Rider,  //骑士
    Witcher //法师
}
[Serializable]
public class PathInfo { public List<UnitPrefabs> List; }
[Serializable]
public class UnitPrefabs
{
    public string unitType;
    public string Path;
    public UnitType UnitType { get { return (UnitType)Enum.Parse(typeof(UnitType),unitType); } }
}