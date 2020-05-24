using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class UnitBase
{
    #region HP方法
    public void CostHP(int amount)  //扣血方法
    {
        ShowHP();
        if (missAttack)
        {
            ShowMessage("光盾:格挡成功");
            return;
        }
        HP -= amount;
        ShowMessage("-" + amount);
    }
    public void AddHP(int amount)
    {
        ShowHP();
        int _amount;
        if (MaxHP - HP >= amount)
            _amount = amount;
        else
            _amount = MaxHP - HP;
        if (HP >= MaxHP)
            return;
        HP += amount;
        AudioManager.Get().PlaySound("Units/OnHeal", 0.2f);
        ShowMessage("+" + _amount);
    }
    #endregion
}