using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回合系统
/// </summary>
public class RoundSystem : BaseManager<RoundSystem>
{
    #region 私有属性
    private uint Rounds = 0;    //回合数
    private bool isWaiting = false; //是否正在等待玩家
    private int side = 1;   //游戏属于哪一方
    private UnityEngine.UI.Text roundtext;  //回合数UI显示
    #endregion
    #region 公有属性
    public bool IsWaitingPlayer(){ return isWaiting; }//是否正在等待玩家
    public int Side { get { return side; } }
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
        roundtext.text = "回合数:" + Rounds.ToString();
        if (isWaiting)
        {
            roundtext.text = "回合数:" + Rounds.ToString() + "(你的回合)";
            EventManager.Getinstance().EventTrigger("RoundSystem_YourTurn");    //触发你的回合事件
            foreach(var i in unitList)
            {
                i.CanOperate = true;
            }
        }
        else
        {
            foreach (var i in unitList)
            {
                i.CanOperate = false;
            }
        }
    }
    public void Init(int side) //回合系统初始化
    {
        roundtext = GameObject.Find("UI/Canvas/Top/HUD/RoundsPanel/Text").GetComponent<UnityEngine.UI.Text>();
        this.side = side;
        EventManager.Getinstance().AddListener<object>("RoundSystem_YourTurn", YourTurn);   //监听轮到玩家事件
        NextRound();
    }
    void YourTurn(object info)  //你的回合事件
    {
        Debug.Log("[消息]你的回合");
        UIManager.Getinstance().MsgOnScreen("你的回合");
    }
    #endregion
}
