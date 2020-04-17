using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameMain : MonoBehaviour
{
    //public _debug _Debug;
    public GridSystem GridSystem;
    private bool isinBackground;
    private void Awake()
    {
        MonoBase.Getinstance().GetMono().AddUpdateListener(() => {
            if(Application.isFocused != isinBackground)
            {
                if(isinBackground)
                { AudioManager.Getinstance().Mute(); }
                else
                { AudioManager.Getinstance().Resume(); }
            }

            isinBackground = Application.isFocused;
        });

    }

    // Start is called before the first frame update
    void Start()
    {
        OnGameStart(null);
        EventManager.Getinstance().EventTrigger(EventTypes.Game_OnStart);
        EventManager.Getinstance().AddListener<CubeCell>(EventTypes.Cell_OnSelected,CellSelection.Getinstance().CellSelected);
    }
    void OnGameStart(object info)
    {
        Debug.Log("[消息]游戏开始");
        HolyWaterSystem.Getinstance().Init();
        RoundSystem.Getinstance().Init(1);
        UIManager.Getinstance();
        UIManager.Getinstance().PushPanel(PanelTypes.HUDPanel);
        GameNetwork.Getinstance().GetNetWorkManager();
        AudioManager.Getinstance().PlayBGM("1");
        AudioManager.Getinstance().PlaySound("GameMain/GameStart",0.05f);
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
        GameGlobal.Getinstance().SetGameMain(this);
    }
}
public class GameGlobal : BaseManager<GameGlobal>
{
    private GameMain gameMain;
    public GameMain GameMain { get { return gameMain; } }

    public void SetGameMain(GameMain _gameMain)
    {
        gameMain = _gameMain;
    }
}