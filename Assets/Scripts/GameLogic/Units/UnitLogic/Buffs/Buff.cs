using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Buff
{
    public abstract class Buff
    {
        //BUFF显示名称
        public string BuffName { get; protected set; } = "NULL";
        //BUFF生效回合数
        protected int rounds = 0;
        //BUFF作用单位
        protected UnitBase unit;
        /// <summary>
        /// 下一回合事件
        /// </summary>
        /// <param name="info"></param>
        public void OnNextRound(object info)
        {
            if(unit==null)
            {
                Destroy();
                return;
            }
            onNextRound();
            if (rounds > 0)
                rounds--;
            if (rounds == 0)
                Destroy();
            Debug.Log("Buff生效:" + GetType().Name + " 剩余回合:" + rounds.ToString());
        }
        /// <summary>
        /// BUFF失效前事件
        /// </summary>
        virtual protected void beforeDestroy() { }
        /// <summary>
        /// BUFF失效事件
        /// </summary>
        void Destroy()
        {
            try
            {
                beforeDestroy();
            }
            finally
            {
                EventManager.Get().RemoveListener<object>(EventTypes.RoundSystem_NextRound, OnNextRound);
                unit.ShowMessage("Buff失效:" + BuffName);
                unit.RemoveBuff(this);
                unit = null;
            }
        }
        /// <summary>
        /// 下一回合事件
        /// </summary>
        protected virtual void onNextRound() { }
        /// <summary>
        /// 设置作用单位
        /// </summary>
        /// <param name="unitBase"></param>
        public void SetTarget(UnitBase unitBase)
        {
            unit = unitBase;
            afterTargetSet();
            unitBase.ShowMessage("Buff生效:" + BuffName);
        }
        /// <summary>
        /// 作用单位设置后事件
        /// </summary>
        virtual protected void afterTargetSet()
        {
            MonoBase.Get().GetMono().RunDelayTask(() => {
                OnNextRound(null);
            }, 0.25f);
        }
        public Buff(int rounds)
        {
            this.rounds = rounds;
            EventManager.Get().AddListener<object>(EventTypes.RoundSystem_NextRound, OnNextRound);
        }
    }
}

