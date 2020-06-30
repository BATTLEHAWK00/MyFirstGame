using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShortRangeAttack : UnitAttackBase
{
    protected override void AttackTarget(UnitBase from, UnitBase target,bool canFightBack)
    {
        //Debug.Log(AttackDistance(from,target));
        from.ShowHP(); target.ShowHP();
        //开启攻击协程
        MonoBase.Get().GetMono().StartCoroutine(attack(from, target,canFightBack));
        from.CanOperate = false;
        //广播攻击消息
        UIManager.Get().MsgOnScreen(string.Format("{0}攻击了{1}", from.UnitName, target.UnitName));
        Debug.Log(string.Format("{0}攻击了{1}", from.UnitName, target.UnitName));
    }
    #region 攻击动画
    IEnumerator attack(UnitBase from, UnitBase target, bool canFightBack)
    {
        yield return DT_MoveTo(from, target);
        if (target == null)
            yield break;
        if (from.triumph)
            from.AddHP(1);
        if (from.dualAttack)
            yield return DT_MoveTo(from, target);
        if (target.GetHP() > 0f && canFightBack)
            target.Attack(from, true, false);
    }
    IEnumerator DT_MoveTo(UnitBase from, UnitBase target)
    {
        if (from == null || target == null)
            yield break;
        UnitSelection.Get().Waiting = true;
        Vector3 currentpositon = from.transform.position;
        Vector3 targetpositon = target.transform.position;
        yield return from.transform.DOMove(targetpositon, 0.4f).WaitForCompletion();
        target.CostHP(from.Damage);
        ResManager.Get().LoadAsync<GameObject>("Prefabs/VFX/ParticleExplosion/ParticleExplosion", (vfx) => {
            vfx.transform.position = from.transform.position;
        });
        target.Anim_OnDamage();
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
    #endregion
}
