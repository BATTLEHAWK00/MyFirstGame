using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttack : UnitAttackBase
{

    private IEnumerator attackTarget(UnitBase from, UnitBase target, bool canFightBack)
    {
        from.CanOperate = false;
        yield return from.Anim_OnAttack();
        //发射子弹
        ResManager.Get().LoadAsync<GameObject>("Prefabs/Units/Projectile/Projectile", (obj) =>
        {
            //设置起始位置
            Vector3 po = from.transform.position;
            po.y = from.GetComponent<UnitBase>().Height;
            obj.transform.position = po;
            //朝向攻击目标
            obj.transform.LookAt(target.transform);
            Quaternion rotation = obj.transform.rotation;
            //将上下旋转重置为0
            rotation.x = 0;
            obj.transform.rotation = rotation;
            //初始化子弹逻辑
            obj.GetComponent<Projectile>().Init(target.gameObject, () => {
                //子弹到达攻击目标
                target.CostHP(from.Damage);
                ResManager.Get().LoadAsync<GameObject>("Prefabs/VFX/ParticleExplosion/ParticleExplosion", (vfx) => {
                    vfx.transform.position = obj.transform.position;
                });
                target.Anim_OnDamage();
                //若对方未死亡，则回击
                if (canFightBack && target.GetHP() > 0)
                    target.Attack(from, true, true);
            }, 1f);
        });
    }
    protected override void AttackTarget(UnitBase from, UnitBase target, bool canFightBack)
    {
        MonoBase.Get().GetMono().StartCoroutine(attackTarget(from, target, canFightBack));
    }
}
