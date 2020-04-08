﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitSounds
{
    public readonly string OnDeath = "Units/OnDeath";
    public readonly string OnBorn = "Units/OnBorn";
}
/// <summary>
/// 单位基本属性类
/// </summary>
public class UnitBase : MonoBehaviour
{
    #region 私有成员
    private string _unitName;   //单位名
    private UnitType _unitType; //单位类型
    private int _side;          //属于哪一方
    private CubeCell _position; //单元格位置
    #endregion
    #region 公有成员
    public string UnitName { get { return _unitName; } }    //获取单位显示名称
    public UnitType UnitType { get { return _unitType; } }  //单位类型
    public int GetHP() { return _HP; }  //获取HP
    public int Side { get { return _side; } }   //属于哪一方
    public CubeCell GetPosition() { return _position; }   //获取单元格位置
    public void SetPosition(CubeCell cubeCell) { _position = cubeCell; }    //设置单元格位置
    public int AttackRange { get { return attackRange; } }  //获取单位攻击距离
    public int Attack { get { return attack; } }    //获取单位攻击力
    #endregion
    #region 单位共有属性
    protected int _HP = -1;    //生命
    protected int attack = 1;   //攻击力
    protected int attackRange = 2;  //攻击距离
    protected string _Description;    //单位描述
    protected delegate void AttackDelegate(UnitBase Target);
    protected AttackDelegate AttackFunc;
    #endregion
    #region Debug方法
    public void Die() { _HP = 0; }
    #endregion
    #region 单位初始化
    protected void SetUnitType(string name, UnitType unitType)
    {
        _unitName = name;
        _unitType = unitType;
    }
    bool CheckInitError()
    {
        if (_HP == -1)
        {
            Debug.LogError("[单位]HP未初始化!");
            return true;
        }
        return false;
    }
    #endregion
    #region 单位事件
    void EventAdd()
    {
        //添加事件
        EventManager.Getinstance().AddListener<GameObject>("Unit_OnUnitDeath", OnDeathBroadcast);
        EventManager.Getinstance().AddListener<GameObject>("Unit_OnUnitBirth", OnBirthBroadcast);
    }
    void EventRemove()
    {
        //事件销毁
        EventManager.Getinstance().RemoveListener<GameObject>("Unit_OnUnitDeath", OnDeathBroadcast);
        EventManager.Getinstance().RemoveListener<GameObject>("Unit_OnUnitBirth", OnBirthBroadcast);
    }
    void OnDeathBroadcast(GameObject info)
    {
        if (info == gameObject) //若事件本体为此物体
            Debug.Log(string.Format("[消息]{0}({1})已死亡", info.GetComponent<UnitBase>().UnitName, info.GetInstanceID()));
    }
    void OnBirthBroadcast(GameObject info)
    {
        if (info == gameObject) //若事件本体为此物体
            Debug.Log(string.Format("[消息]{0}({1})已生成", info.GetComponent<UnitBase>().UnitName, info.GetInstanceID()));
    }
    void OnDeath()
    {
        //触发单位死亡事件(Unit_OnUnitDeath)
        EventManager.Getinstance().EventTrigger("Unit_OnUnitDeath", gameObject);
        //播放死亡音效
        AudioManager.Getinstance().PlaySound(new GameSounds().UnitSounds.OnDeath, 0.2f);
        //广播死亡消息
        EventManager.Getinstance().EventTrigger<string>("UI_MsgBar", UnitName + "已死亡!");
        //行计数器扣除
        TheGame.Getinstance().GameMain.GridSystem.RowCounter[GetPosition().Position.Y]--;
        GetPosition().CurrentObject = null;
        //销毁物体
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        EventRemove();
    }
    #endregion
    #region MonoBehavior模块事件
    // Start is called before the first frame update
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
        EventManager.Getinstance().EventTrigger("Unit_OnUnitBirth", gameObject);
        //Invoke("Die", 3);
        //子类初始化函数
        _Start();
        AudioManager.Getinstance().PlaySound(new GameSounds().UnitSounds.OnBorn, 0.2f);
        EventManager.Getinstance().EventTrigger<string>("UI_MsgBar", UnitName + "被召唤了出来!");
    }
    // Update is called once per frame
    void Update()   //帧刷新事件
    {
        #region HP检测
        if (_HP <= 0)
            OnDeath();
        #endregion
        _Update();
    }
    virtual protected void _Start() { }
    virtual protected void _Update() { }
    #endregion
    #region 单位方法
    public void CostHP(int amount)  //扣血方法
    {
        _HP -= amount;
    }
    #endregion
}
