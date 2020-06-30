using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 单位基本属性类
/// </summary>
public abstract partial class UnitBase : MonoBehaviour
{
    #region Unity编辑器里编辑
    public float Height = 10f;
    #endregion
    #region 私有成员
    [SerializeField]
    protected CubeCell _position; //单元格位置
    #endregion
    #region 公有成员
    public int GetHP() { return HP; }  //获取HP
    public CubeCell GetPosition() { return _position; }   //获取单元格位置
    public void SetPosition(CubeCell cubeCell) { _position = cubeCell; }    //设置单元格位置
    #endregion
    #region 外部访问器
    public string Description { get; protected set; }    //单位描述
    public int AttackRange { get; protected set; } = 2;  //单位攻击距离
    public int Damage { get; protected set; } = 1;    //单位攻击力
    public int MaxHP { get; protected set; }     //最大生命值
    public string UnitName { get; protected set; }   //单位名
    public UnitType UnitType { get { return unitType; } } //单位类型

    #endregion
    #region 单位共有属性
    private bool dead = false;
    public bool CanOperate = true;
    protected int HP = -1;    //生命
    protected UnitType unitType;
    public int HolyWaterCost { get; protected set; } = 1;
    protected delegate void AttackDelegate(UnitBase Target);
    protected AttackDelegate AttackFunc;
    #endregion
    #region 委托
    protected UnityAction onUpdate;
    #endregion
    #region 单位初始化
    protected void SetUnitType(string name, UnitType unitType)
    {
        UnitName = name;
        this.unitType = unitType;
        
    }
    bool CheckInitError()
    {
        if (HP == -1)
        {
            Debug.LogError("[单位]HP未初始化!");
            return true;
        }
        return false;
    }

    #endregion
}
