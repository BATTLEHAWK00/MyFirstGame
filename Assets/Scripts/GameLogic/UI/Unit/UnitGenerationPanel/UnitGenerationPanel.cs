using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class UnitGenerationPanel : PanelBase
{
    private Dictionary<UnitType, string> unitPrefabDic = new Dictionary<UnitType, string>();
    public void GenerateUnit(UnitType unitType)
    {
        ResManager.Getinstance().LoadAsync<GameObject>(unitPrefabDic[unitType], (obj) =>
        {
            UnitBase unitBase = obj.GetComponent<UnitBase>();
            obj.transform.SetParent(GameObject.Find("Units").transform);
            obj.transform.position = CellSelection.Getinstance().GetCurrentSelected().transform.position;
            unitBase.SetPosition(CellSelection.Getinstance().GetCurrentSelected());
            CellSelection.Getinstance().GetCurrentSelected().CurrentUnit = unitBase;
            Pop();
        });
    }
    public void Pop()
    {
        UIManager.Getinstance().PopPanel();
    }
    public void GenerateArcher()
    {
        GenerateUnit(UnitType.Archer);
    }
    public void GenerateWitcher()
    {
        GenerateUnit(UnitType.Witcher);
    }
    public void GenerateRider()
    {
        GenerateUnit(UnitType.Rider);
    }
    public void GenerateLancer()
    {
        GenerateUnit(UnitType.Lancer);
    }
    public void GenerateFarmer()
    {
        GenerateUnit(UnitType.Farmer);
    }
    public override void Awake()
    {
        base.Awake();
        var paths = TheGameCommon.JsonFunc.ListFromFile<PrefabPath>("Prefabs/Units/UnitPrefabs");
        foreach (var i in paths)
            unitPrefabDic.Add(i.UnitType, i.Path);
    }
    [System.Serializable]
    class PrefabPath
    {
        public string unitType=null;
        public string Path=null;
        public UnitType UnitType
        {
            get
            {
                return (UnitType)Enum.Parse(typeof(UnitType), unitType);
            }
        }
    }
}
