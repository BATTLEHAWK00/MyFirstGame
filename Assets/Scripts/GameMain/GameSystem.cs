using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameSystem:MonoBehaviour
{
    //对外只读
    public bool isWaiting { get { return _isWaiting; } }
    //对外只读
    public bool IsPlacing;  //是否正在放置
    public List<UnitMain> UnitList = new List<UnitMain>();
    private int Round = 0;     //回合数
    public readonly int Side;   //0是A方 1是B方 只读
    private bool _isWaiting = false;     //判断是否等待玩家操作
    private uint TimeLeft=900;
    public GameObject TimerText;
    public GameObject Player1;
    public GameObject Player2;
    void Awake()
    {
        Game.GameSystemCurrent = this;
    }
    void Start()
    {
        StartCoroutine(Tick()); //开始倒计时
    }
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
    public GameSystem()    //构造函数初始化
    {
        Side = Game.Side;
        NextRound();
        Debug.Log("开始");
    }
    IEnumerator Tick()
    {
        if (TimeLeft == 0)
            if (Player1.GetComponent<PlayerLogic>().HP > Player2.GetComponent<PlayerLogic>().HP)
                Game.GameWin(0);
            else
                Game.GameWin(1);
        TimeLeft--;
        TimerText.GetComponent<UnityEngine.UI.Text>().text = "时间剩余："+GetTime(TimeLeft);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Tick());
    }
    string GetTime(uint time)
    {
        int m = Mathf.FloorToInt(time / 60);
        int s = Mathf.FloorToInt(time - m * 60f);
        return m.ToString("00") + ":" + s.ToString("00");
    }
}

public static class Game
{
    #region 这里存放全局变量
    public static GameSystem GameSystemCurrent;
    public static Move MoveCurrent;
    public static Select SelectCurrrent;
    public static GameObject NewObject;
    public static int Side;
    #endregion

    public static void GameInitialize(int a)
    {
        Side = a;
    }
    public static void InGameInitialize()
    {
        SelectCurrrent = new Select();
    }
    public static void GameWin(int b)
    {
        Debug.Log("游戏结束");
        SceneManager.LoadScene("GameWin");
    }
}