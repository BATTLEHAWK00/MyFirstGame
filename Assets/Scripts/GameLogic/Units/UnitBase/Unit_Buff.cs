using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class UnitBase
{
    #region Buff状态
    //Buff列表
    private List<Buff.Buff> unitBuffs = new List<Buff.Buff>();
    //抵挡攻击
    public bool missAttack = false;
    //双重攻击
    public bool dualAttack = false;
    //凯旋
    public bool triumph = false;
    #endregion
    #region Buff方法
    public void AddBuff(Buff.Buff buff)
    {
        Debug.Log(string.Format("给{0}添加Buff:{1}", UnitName, buff.GetType().Name));
        buff.SetTarget(this);
        unitBuffs.Add(buff);
    }
    public void RemoveBuff(Buff.Buff buff)
    {
        Debug.Log(string.Format("给{0}移除Buff:{1}", UnitName, buff.GetType().Name));
        unitBuffs.Remove(buff);
    }
    #endregion
}