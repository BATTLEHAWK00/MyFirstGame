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
        OnGameInitialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnGameStart();
        EventManager.Get().EventTrigger(EventTypes.Game_OnStart);
        EventManager.Get().AddListener<CubeCell>(EventTypes.Cell_OnSelected,CellSelection.Get().CellSelected);
        Debug.Log("[消息]游戏开始");
        MonoBase.Get().GetMono().AddUpdateListener(CheckFocus);
    }
    void CheckFocus()
    {
        if (Application.isFocused == isinBackground)
        {
            if (Application.isFocused)
            { AudioManager.Get().Resume(); }
            else
            { AudioManager.Get().Mute(); }
            isinBackground = !Application.isFocused;
        }
    }
    void OnGameInitialize()
    {
        GameObject grid = ResManager.Get().Load<GameObject>("Prefabs/GridSystem/GridSystem");
        grid.transform.SetParent(GameObject.Find("Ground").transform, false);
        GridSystem = grid.GetComponent<GridSystem>();
        GridSystem.enabled = true;
        GridSystem.gameObject.name = "GridSystem";
        HolyWaterSystem.Get().Init();
        RoundSystem.Get().Init(1);
        UIManager.Get();
        UIManager.Get().PushPanel(PanelTypes.HUDPanel);
        //GameNetwork.Getinstance().GetNetWorkManager();
    }
    void OnGameStart()
    {
        UIManager.Get().MsgOnScreen("游戏开始啦!");
        AudioManager.Get().PlayBGM("1");
        AudioManager.Get().PlaySound("GameMain/GameStart", 0.05f);
        CardSystem.Get().AddCard(new Slay());
        CardSystem.Get().AddCard(new BattleHorn());
        CardSystem.Get().AddCard(new HawkEye());
        CardSystem.Get().AddCard(new Purify());
    }
    // Update is called once per frame
    void Update()
    {
        CheckKey_Esc();
    }
    void CheckKey_Esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Get().SwitchPanel(PanelTypes.GameSettingPanel);
        }
    }
    private void OnDestroy()
    {
        MonoBase.Get().GetMono().RemoveUpdateListener(CheckFocus);
        UnloadGameLogic();
    }
    void UnloadGameLogic()
    {
        EventManager.Destroy();
        UIManager.Destroy();
        GameLogicSystemManager.Destroy();
    }
    public GameMain()
    {
        GameGlobal.Get().SetGameMain(this);
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