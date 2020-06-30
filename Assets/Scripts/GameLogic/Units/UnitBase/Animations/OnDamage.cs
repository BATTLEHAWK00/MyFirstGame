using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

partial class UnitBase
{
    public virtual void Anim_OnDamage()
    {
        AudioManager.Get().PlaySound("Units/OnAttack", 0.25f);
        transform.DOShakePosition(0.25f, 1f, 20, default, default, false);
    }
}
