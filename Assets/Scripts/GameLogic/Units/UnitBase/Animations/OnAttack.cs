using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

partial class UnitBase
{
    public virtual IEnumerator Anim_OnAttack()
    {
        Vector3 startpo = transform.position;
        
        YieldInstruction yield1 = transform.DOMoveY(startpo.y + 3f, 0.25f).OnComplete(()=> {
            AudioManager.Get().PlaySound("Units/OnAttack_LongRange", 0.25f);
            GetComponent<ShowOutline>().enabled = true;
            transform.DOMoveY(startpo.y, 1f).SetEase(Ease.OutSine).SetDelay(0.25f).OnComplete(()=> {
                GetComponent<ShowOutline>().enabled = false;
            });
        }).WaitForCompletion();
        yield return yield1;
    }
}
