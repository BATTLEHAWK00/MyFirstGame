using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UnitAttack:BaseManager<UnitAttack>
{
    /// <summary>
    /// 攻击方法
    /// </summary>
    /// <param name="from">攻击方</param>
    /// <param name="target">攻击目标</param>
    public void AttackTarget(UnitBase from,UnitBase target)
    {
        #region 异常处理
        if (from == null || target == null)
            return;
        if(from==target)
        { UIManager.Get().MsgOnScreen("你不能攻击自己!");return; }
        if (from.AttackRange < AttackDistance(from, target))
        {
            UIManager.Get().MsgOnScreen(string.Format("{0}攻击距离({1})不够!与目标距离:{2}", from.UnitName, from.AttackRange, AttackDistance(from, target)));
            Debug.LogWarning("攻击距离不够!");
            return;
        }
        if (!from.CanOperate)
        { UIManager.Get().MsgOnScreen("你已经操作过该单位!");return; }
        #endregion
        //Debug.Log(AttackDistance(from,target));
        from.ShowHP();target.ShowHP();
        //开启攻击协程
        MonoBase.Get().GetMono().StartCoroutine(attack(from, target));
        from.CanOperate = false;
        //广播攻击消息
        UIManager.Get().MsgOnScreen(string.Format("{0}攻击了{1}", from.UnitName, target.UnitName));
        Debug.Log(string.Format("{0}攻击了{1}",from.UnitName,target.UnitName));
    }
    #region 攻击动画
    IEnumerator attack(UnitBase from, UnitBase target)
    {
        yield return DT_MoveTo(from, target);
        if (target == null)
            yield break;
        if (from.triumph)
            from.AddHP(1);
        if (from.dualAttack)
            yield return DT_MoveTo(from, target);
        if(target.GetHP()>0f)
            yield return DT_MoveTo(target, from);
    }
    IEnumerator DT_MoveTo(UnitBase from, UnitBase target)
    {
        if (from == null || target == null)
            yield break;
        UnitSelection.Get().Waiting = true;
        Vector3 currentpositon = from.transform.position;
        Vector3 targetpositon = target.transform.position;
        yield return from.transform.DOMove(targetpositon, 0.4f).WaitForCompletion();
        AudioManager.Get().PlaySound("Units/OnAttack",0.25f);
        target.CostHP(from.Attack);
        target.transform.DOShakePosition(0.25f, 1f, 20, default, default, false);
        Camera.main.transform.DOShakePosition(0.2f, 0.05f, 20, default, default, false);
        yield return DT_MoveBack(from);
    }
    IEnumerator DT_MoveBack(UnitBase from)
    {
        Vector3 currentpositon = from.transform.position;
        Vector3 targetpositon = from.GetPosition().transform.position;
        yield return from.transform.DOMove(targetpositon, 0.4f).WaitForCompletion();
        UnitSelection.Get().Waiting = false;
        yield break;
    }
    /* 弃用代码
    IEnumerator Anim_MoveTo(UnitBase from, UnitBase target)
    {
        UnitSelection.Getinstance().Waiting = true;
        Vector3 currentpositon = from.transform.position;
        Vector3 targetpositon = target.transform.position;
        from.transform.position = Vector3.MoveTowards(currentpositon, targetpositon, 0.5f);
        if (Vector3.Distance(currentpositon, targetpositon) < 0.1f)
        {
            target.CostHP(from.Attack);
            target.transform.DOShakePosition(0.25f,1f,20,default,default,false);
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
        {
            UnitSelection.Getinstance().Waiting = false;
            yield return null;
        }
        else
            yield return Anim_MoveBack(from);
    }*/
    #endregion
    public int AttackDistance(UnitBase from,UnitBase to)    //计算攻击需要的距离
    {
        if (from == null || to == null)
            return -1;
        int cnt = 0;
        int fromY = from.GetPosition().Position.y;
        int toY = to.GetPosition().Position.y;
        if (fromY > toY)
            TheGameCommon.Common.Swap(ref fromY,ref toY);
        //foreach (var i in GameGlobal.Getinstance().GameMain.GridSystem.RowCounter)
         //   Debug.Log(i);
        for (int i = fromY; i <= toY; i++)
            if (GameGlobal.Get().GameMain.GridSystem.RowCounter[i]>0)
                cnt++;
        return cnt;
    }
}
