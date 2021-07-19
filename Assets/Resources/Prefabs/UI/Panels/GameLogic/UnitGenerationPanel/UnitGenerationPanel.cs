using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnitGenerationPanel : PanelBase
{
    private Dictionary<UnitType, string> unitPrefabDic = new Dictionary<UnitType, string>();
    private Dictionary<UnitType, GameObject> unitModels = new Dictionary<UnitType, GameObject>();
    [SerializeField]
    private Text unitText = null;
    [SerializeField]
    private Transform Units = null;
    private UnitType current;
    private UnitType Current
    {
        get { return current; }
        set
        {
            UnitType last = current;
            current = value;
            onCurrentChanged(last);
        }
    }
    void onCurrentChanged(UnitType last)
    {
        if (unitModels.ContainsKey(last) && unitModels[last] != null)
            unitModels[last].SetActive(false);
        if(unitModels.ContainsKey(Current) && unitModels[Current] != null)
        {
            unitModels[Current].SetActive(true);
            unitText.text = unitModels[Current].GetComponent<UnitBase>().UnitName;
            return;
        }
        ResManager.Get().LoadAsync<GameObject>(unitPrefabDic[Current], (obj) =>
        {
            obj.SetActive(false);
            unitText.text = obj.GetComponent<UnitBase>().UnitName;
            obj.GetComponent<UnitBase>().enabled = false;
            obj.transform.SetParent(Units,false);
            obj.transform.Rotate(new Vector3(0,180f,0));
            for(int i=0;i<obj.transform.childCount;i++)
                obj.transform.GetChild(i).gameObject.layer=8;
            obj.layer = 8;
            unitModels[Current] = obj;
            obj.SetActive(true);
        },true);
        //Debug.Log("CurrentChanged:" + Current);
    }
    public void GenerateUnit(UnitType unitType)
    {
        ResManager.Get().LoadAsync<GameObject>(unitPrefabDic[unitType], (obj) =>
        {
            UnitBase unitBase = obj.GetComponent<UnitBase>();
            obj.transform.SetParent(GameObject.Find("Units").transform);
            obj.transform.position = CellSelection.Get().GetCurrentSelected().transform.position;
            unitBase.SetPosition(CellSelection.Get().GetCurrentSelected());
            CellSelection.Get().GetCurrentSelected().CurrentUnit = unitBase;
            Pop();
        });
    }
    protected override void BeforeExit()
    {
        
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
        UIManager.Get().PopPanel();
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
