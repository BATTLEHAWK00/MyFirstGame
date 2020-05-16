using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnitGenerationPanel : PanelBase
{
    private Dictionary<UnitType, string> unitPrefabDic = new Dictionary<UnitType, string>();
    [SerializeField]
    private Text unitText = null;
    private UnitType current;
    private UnitType Current
    {
        get { return current; }
        set
        {
            current = value;
            onCurrentChanged();
        }
    }
    void onCurrentChanged()
    {
        ResManager.Getinstance().LoadAsync<GameObject>(unitPrefabDic[Current], (obj) =>
        {
            unitText.text = obj.GetComponent<UnitBase>().UnitName;
        },false);
        Debug.Log("CurrentChanged:" + Current);
    }
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
    public void Left()
    {
        if (Current == 0)
            return;
        Current--;
    }
    public void Right()
    {
        if (Current == UnitType.Witcher)
            return;
        Current++;
    }
    public void Generate()
    {
        GenerateUnit(Current);
    }
    public void Pop()
    {
        UIManager.Getinstance().PopPanel();
    }
    /*  弃用
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
    }*/
    public override void Awake()
    {
        base.Awake();
        var paths = TheGameCommon.JsonFunc.ListFromFile<PrefabPath>("Prefabs/Units/UnitPrefabs");
        foreach (var i in paths)
            unitPrefabDic.Add(i.UnitType, i.Path);
        SceneManager.LoadScene("InGame/Portrait");
        Current = UnitType.Archer;
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
