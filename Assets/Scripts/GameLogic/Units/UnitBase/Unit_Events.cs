using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

partial class UnitBase
{
    #region 单位事件
    void EventAdd()
    {
        //添加事件

    }
    void EventRemove()
    {
        //事件销毁

    }
    void OnDeathBroadcast(GameObject info)
    {
        if (info == gameObject) //若事件本体为此物体
        {

        }
        //Debug.Log(string.Format("[消息]{0}({1})已死亡", info.GetComponent<UnitBase>().UnitName, info.GetInstanceID()));
    }
    void OnBirthBroadcast(GameObject info)
    {
        if (info == gameObject) //若事件本体为此物体
        {
            //Debug.Log(string.Format("[消息]{0}({1})已生成", info.GetComponent<UnitBase>().UnitName, info.GetInstanceID()));
            //UIManager.Getinstance().MsgOnScreen(UnitName + "被召唤了出来!");
        }
    }
    void Die()
    {
        dead = true;
        HideHP();
        //GetComponent<MeshRenderer>().material.DOFade(0f,1f);
        transform.DOMoveY(transform.position.y + 5f, 0.25f).SetEase(Ease.InOutQuad).SetDelay(0.25f).OnComplete(() => {
            transform.DOScale(0.25f, 0.25f).SetEase(Ease.InBack).OnComplete(() => {
                //BeforeDeath事件
                BeforeDeath();
                //触发OnDeath事件
                OnDeath();
                //销毁物体
                Destroy(gameObject);
            });
        });
        /*
        //死亡动画
        transform.DOShakePosition(1f, 2f, 10).OnComplete(()=> {
            //触发OnDeath事件
            OnDeath();
            //销毁物体
            Destroy(gameObject);
        });*/
    }
    void OnDeath()
    {
        //播放死亡音效
        AudioManager.Get().PlaySound(SoundsList.Unit_OnDeath, 0.2f);
        //广播死亡消息
        UIManager.Get().MsgOnScreen(UnitName + "已死亡!");
        //行计数器扣除
        GameGlobal.Get().GameMain.GridSystem.RowCounter[GetPosition().Position.y]--;
        //触发单位死亡事件(Unit_OnUnitDeath)
        EventManager.Get().EventTrigger(EventTypes.Unit_OnDeath, gameObject);
        //事件移除
        EventRemove();
        //解引用
        GetPosition().CurrentUnit = null;
        //移除回合系统列表
        RoundSystem.Get().RemoveUnit(this);
    }
    void OnBorn()
    {
        //行计数器加1
        GameGlobal.Get().GameMain.GridSystem.RowCounter[GetPosition().Position.y]++;
        //添加事件
        EventAdd();
        EventManager.Get().EventTrigger(EventTypes.Unit_OnBorn, gameObject);
        //向回合系统列表添加单位
        RoundSystem.Get().AddUnit(this);
    }
    void BeforeDeath()
    {

    }
    void AfterBorn()
    {
        //出生动画
        Vector3 po = transform.position;
        po.y += 100f;
        transform.position = po;
        transform.DOMoveY(transform.position.y - 100f, 0.4f).SetEase(Ease.InQuad).OnComplete(() => {
            //transform.DOShakePosition(0.25f, 1f, 20, default, default, false);
            Camera.main.transform.DOShakePosition(0.3f, 0.05f, 20, default, default, true);
            AudioManager.Get().PlaySound(SoundsList.Unit_OnBorn, 0.3f);
        });

    }
    #endregion
}
