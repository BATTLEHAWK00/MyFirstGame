using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : BaseManager<RoundSystem>
{
    private uint Rounds = 0;
    private bool isWaiting = false;
    private int side = 1;
    private UnityEngine.UI.Text roundtext;
    public bool IsWaitingPlayer()
    {
        return isWaiting;
    }
    public int Side { get { return side; } }
    public void NextRound()
    {
        Rounds++;
        if (Rounds % 2 == side)
            isWaiting = true;
        else
            isWaiting = false;
        roundtext.text = "回合数:" + Rounds.ToString();
        if (isWaiting)
        {
            roundtext.text = "回合数:" + Rounds.ToString()+"(你的回合)";
            EventManager.Getinstance().EventTrigger("RoundSystem_YourTurn");
        }
    }
    public void Init(UnityEngine.UI.Text text,int side)
    {
        roundtext = text;
        this.side = side;
        EventManager.Getinstance().AddListener<object>("RoundSystem_YourTurn", YourTurn);
        NextRound();
    }
    void YourTurn(object info)
    {
        Debug.Log("[消息]你的回合");
        UIManager.Getinstance().MsgOnScreen("你的回合");
    }
}
