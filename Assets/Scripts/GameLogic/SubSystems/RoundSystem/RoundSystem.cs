using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 回合系统
/// </summary>
public class RoundSystem : BaseManager<RoundSystem>
{
    #region 私有属性
    private bool isWaiting = false; //是否正在等待玩家
    private int side = 1;   //游戏属于哪一方
    #endregion
    #region 公有属性
    public bool IsWaitingPlayer(){ return isWaiting; }//是否正在等待玩家
    public int Side { get { return side; } }
    public uint Rounds { get; private set; } = 0;
    #region 单位列表
    private List<UnitBase> unitList = new List<UnitBase>();
    public void AddUnit(UnitBase unitBase) { unitList.Add(unitBase); }
    public void RemoveUnit(UnitBase unitBase) { unitList.Remove(unitBase); }
    #endregion
    #endregion
    #region 方法
    public void NextRound() //进入下一回合
    {
        Rounds++;
        if (Rounds % 2 == side)
            isWaiting = true;
        else
            isWaiting = false;
        EventManager.Get().EventTrigger(EventTypes.RoundSystem_NextRound);
        if (isWaiting)
        {
            foreach (var i in unitList)
                i.CanOperate = true;
            EventManager.Get().EventTrigger(EventTypes.RoundSystem_YourTurn);    //触发你的回合事件
        }
        else
        {
            foreach (var i in unitList)
                i.CanOperate = false;
        }
        
    }
    public void Init(int side) //回合系统初始化
    {
        this.side = side;
        EventManager.Get().AddListener<object>(EventTypes.RoundSystem_YourTurn, YourTurn);   //监听轮到玩家事件
        NextRound();
    }
    void YourTurn(object info)  //你的回合事件
    {
        //Debug.Log("[消息]你的回合");
        UIManager.Get().MsgOnScreen("你的回合");
    }
    #endregion
}
