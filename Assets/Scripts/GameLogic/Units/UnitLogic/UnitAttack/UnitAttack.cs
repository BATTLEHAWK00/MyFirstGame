using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class UnitAttackBase
{
    protected abstract void AttackTarget(UnitBase from, UnitBase target,bool canFightback = true);
    public void Attack(UnitBase from, UnitBase target,bool ignoreCheck=false, bool canFightback = true)
    {
        if (!ignoreCheck)
        {
            #region 异常处理
            if (from == null || target == null)
                return;
            if (from == target)
            { UIManager.Get().MsgOnScreen("你不能攻击自己!"); return; }
            if (from.AttackRange < AttackDistance(from, target))
            {
                UIManager.Get().MsgOnScreen(string.Format("{0}攻击距离({1})不够!与目标距离:{2}", from.UnitName, from.AttackRange, AttackDistance(from, target)));
                return;
            }
            if (!from.CanOperate)
            { UIManager.Get().MsgOnScreen("你已经操作过该单位!"); return; }
            if(!target.CanOperate)
            { UIManager.Get().MsgOnScreen("该单位已被操作!"); return; }
            #endregion
        }
        AttackTarget(from, target, canFightback);
    }
    public static int AttackDistance(UnitBase from, UnitBase to)    //计算攻击需要的距离
    {
        if (from == null || to == null)
            return -1;
        int cnt = 0;
        int fromY = from.GetPosition().Position.y;
        int toY = to.GetPosition().Position.y;
        if (fromY > toY)
            TheGameCommon.Common.Swap(ref fromY, ref toY);
        //foreach (var i in GameGlobal.Getinstance().GameMain.GridSystem.RowCounter)
        //   Debug.Log(i);
        for (int i = fromY; i <= toY; i++)
            if (GameGlobal.Get().GameMain.GridSystem.RowCounter[i] > 0)
                cnt++;
        return cnt;
    }
}