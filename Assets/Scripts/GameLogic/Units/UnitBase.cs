using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Events_Unit
{
    Unit_OnUnitDeath,
    Unit_OnUnitBirth
}

public class UnitBase : MonoBehaviour
{
    #region 私有成员
    private string _unitName;
    private UnitType _unitType;
    #endregion
    #region 共有成员
    public string UnitName { get { return _unitName; } }
    public UnitType UnitType { get { return _unitType; } }
    public int GetHP() { return _HP; }
    #endregion
    #region 单位共有属性
    protected int _HP=-1;    //生命
    protected string _Description;    //单位描述
    #endregion
    // Start is called before the first frame update
    bool CheckInitError()
    {
        if (_HP == -1)
        {
            Debug.LogError("[单位]HP未初始化!");
            return true;
        }
        return false;
    }
    void Awake()
    {

    }
    void Start()
    {
        //检测初始化是否有错误
        if (CheckInitError())
        {
            Destroy(gameObject);
            return;
        }
        //添加事件
        EventAdd();
        EventManager.Getinstance().EventTrigger("Unit_OnUnitBirth",gameObject);
        Invoke("Die", 3);
        //子类初始化函数
        _Start();
    }
    public void Die() { _HP = 0; }
    void EventAdd()
    {
        EventManager.Getinstance().AddListenner("Unit_OnUnitDeath", OnDeathBroadcast);
        EventManager.Getinstance().AddListenner("Unit_OnUnitBirth", OnBirthBroadcast);
    }
    // Update is called once per frame
    void Update()
    {
        #region HP检测
        if (_HP <= 0)
            OnDeath();
        #endregion
        _Update();
    }
    virtual protected void _Start() { }
    virtual protected void _Update() { }
    protected void SetUnitType(string name,UnitType unitType)
    {
        _unitName = name;
        _unitType = unitType;
    }
    public void CostHP(int amount)
    {
        _HP -= amount;
    }
    
    void OnDeath()
    {
        EventManager.Getinstance().EventTrigger("Unit_OnUnitDeath", gameObject);
        Destroy(gameObject);
    }
    void OnDeathBroadcast(object info)
    {
        Debug.Log(string.Format("[消息]{0}已死亡", (info as GameObject).GetComponent<UnitBase>().UnitName));
    }
    void OnBirthBroadcast(object info)
    {
        Debug.Log(string.Format("[消息]{0}已生成", (info as GameObject).GetComponent<UnitBase>().UnitName));
    }
}
