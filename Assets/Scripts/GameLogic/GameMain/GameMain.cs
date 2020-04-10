using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    //public _debug _Debug;
    public GridSystem GridSystem;
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Getinstance().AddListener<object>("Game_OnStart", OnGameStart);
        EventManager.Getinstance().EventTrigger("Game_OnStart");
        EventManager.Getinstance().AddListener<CubeCell>("Grid_OnSelected",CellSelection.Getinstance().CellSelected);
    }
    void OnGameStart(object info)
    {
        Debug.Log("[消息]游戏开始");
        HolyWaterSystem.Getinstance().Init();
        RoundSystem.Getinstance().Init(1);
        AudioManager.Getinstance().PlayBGM("1");
        AudioManager.Getinstance().PlaySound("GameMain/GameStart",0.1f);
        UIManager.Getinstance().MsgOnScreen("游戏开始啦!");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Getinstance().SwitchPanel(PanelTypes.GameSettingPanel);
        }
            
    }
    public GameMain()
    {
        TheGame.Getinstance().SetGameMain(this);
    }
}
public class TheGame : BaseManager<TheGame>
{
    private GameMain gameMain;
    public GameMain GameMain { get { return gameMain; } }

    public void SetGameMain(GameMain _gameMain)
    {
        gameMain = _gameMain;
    }
}