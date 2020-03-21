using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public _debug _Debug;
    // Start is called before the first frame update
    void Start()
    {
        TheGame.Getinstance().SetGameMain(this);
        EventManager.Getinstance().AddListenner("Game_OnStart", OnGameStart);
        EventManager.Getinstance().EventTrigger("Game_OnStart", null);
    }
    void OnGameStart(object info)
    {
        Debug.Log("[消息]游戏开始");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
public class TheGame : BaseManager<TheGame>
{
    private GameMain gameMain;
    public GameMain GameMain { get { return gameMain; } }
    public _debug Debug()
    {
        return gameMain._Debug;
    }
    public void SetGameMain(GameMain _gameMain)
    {
        gameMain = _gameMain;
    }
}