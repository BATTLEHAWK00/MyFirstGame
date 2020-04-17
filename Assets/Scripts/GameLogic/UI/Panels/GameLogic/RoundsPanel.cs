using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoundsPanel : MonoBehaviour
{
    private UnityEngine.UI.Text text;
    private void Awake()
    {
        EventManager.Getinstance().AddListener<object>(EventTypes.RoundSystem_NextRound, NextRound);
    }
    void NextRound(object info)
    {
        if (text == null)
            text = GetComponentInChildren<UnityEngine.UI.Text>();
        uint round = RoundSystem.Getinstance().Rounds;
        if(round % 2 == RoundSystem.Getinstance().Side)
            text.text = "回合数:" + RoundSystem.Getinstance().Rounds.ToString()+"(你的回合)";
        else
            text.text = "回合数:" + RoundSystem.Getinstance().Rounds.ToString();
    }    
}
