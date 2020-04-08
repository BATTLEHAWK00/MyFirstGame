﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public void AttackTarget(UnitBase from,UnitBase target)
    {
        #region 异常处理
        if (from == null || target == null)
            return;
        if(from==target)
        { UIManager.Getinstance().MsgOnScreen("你不能攻击自己!");return; }
        if (from.AttackRange < AttackDistance(from, target))
        {
            UIManager.Getinstance().MsgOnScreen(string.Format("{0}攻击距离({1})不够!与目标距离:{2}", from.UnitName, from.AttackRange, AttackDistance(from, target)));
            Debug.LogWarning("攻击距离不够!");
        }
        #endregion
        //开启攻击协程
        TheGame.Getinstance().GameMain.MonoManager.StartCoroutine(Anim_MoveTo(from, target));
        //广播攻击消息
        UIManager.Getinstance().MsgOnScreen(string.Format("{0}攻击了{1}", from.UnitName, target.UnitName));
        Debug.Log(string.Format("{0}攻击了{1}",from.UnitName,target.UnitName));
    }
    #region 攻击动画
    IEnumerator Anim_MoveTo(UnitBase from, UnitBase target)
    {
        Vector3 currentpositon = from.transform.position;
        Vector3 targetpositon = target.transform.position;
        from.transform.position = Vector3.MoveTowards(currentpositon, targetpositon, 0.5f);
        if (Vector3.Distance(currentpositon, targetpositon) < 0.1f)
        {
            target.CostHP(from.Attack);
            yield return Anim_MoveBack(from);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            yield return Anim_MoveTo(from, target);
        }
    }
    IEnumerator Anim_MoveBack(UnitBase from)
    {
        Vector3 currentpositon = from.transform.position;
        Vector3 targetpositon = from.GetPosition().transform.position;
        from.transform.position = Vector3.MoveTowards(currentpositon, targetpositon, 0.5f);
        yield return new WaitForEndOfFrame();
        if (Vector3.Distance(currentpositon, targetpositon) < 0.1f)
            yield return null;
        else
            yield return Anim_MoveBack(from);
    }
    #endregion
    public int AttackDistance(UnitBase from,UnitBase to)    //计算攻击需要的距离
    {
        if (from == null || to == null)
            return -1;
        int cnt = 0;
        int fromY = from.GetPosition().Position.Y;
        int toY = to.GetPosition().Position.Y;
        if (fromY > toY)
            Common.Swap(ref fromY,ref toY);
        for (int i = fromY; i <= toY; i++)
            if (TheGame.Getinstance().GameMain.GridSystem.RowCounter[i]>0)
                cnt++;
        return cnt;
    }
}
