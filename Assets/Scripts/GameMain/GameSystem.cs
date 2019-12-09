using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem
{
    //对外只读
    public bool isWaiting { get { return _isWaiting; } }
    //对外只读
    public bool IsPlacing;  //是否正在放置
    public List<Shuxing> ShuxingListA = new List<Shuxing>(); //A方属性列表
    public List<Shuxing> ShuxingListB = new List<Shuxing>(); //B方属性列表
    private int Round = 0;     //回合数
    public readonly int Side;   //0是A方 1是B方 只读
    private bool _isWaiting = false;     //判断是否等待玩家操作


    public int GetRound()   //获取回合数
    {
        return Round;
    }
    public int WhoseRound() //哪方回合
    {
        if (Round % 2 == 1)
            return 0;
        else
            return 1;
    }
    public void NextRound() //下一回合
    {
        Round++;
        if(WhoseRound()==Side)
            _isWaiting = true;
        else
            _isWaiting = false;
    }
    public void Test1()
    {
        _isWaiting = true;
    }
    public GameSystem(int a)    //构造函数初始化
    {
        Side = a;
        NextRound();
    }
}

public static class Game
{
    #region 这里存放全局变量
    public static GameSystem GameSystemCurrent;
    public static Move MoveCurrent;
    public static Select SelectCurrrent;
    public static GameObject NewObject;
    #endregion

    public static void GameInitialize(int a)
    {
        GameSystemCurrent = new GameSystem(a);
    }
    public static void InGameInitialize()
    {
        MoveCurrent = new Move();
        SelectCurrrent = new Select();
    }
    public static void GameWin(int b)
    {
        Debug.Log("游戏结束");
        SceneManager.LoadScene("GameWin");

    }
}