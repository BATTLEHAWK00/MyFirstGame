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
        
    }

    // Start is called before the first frame update
    void Start()
    {
        OnGameStart(null);
        EventManager.Getinstance().EventTrigger(EventTypes.Game_OnStart);
        EventManager.Getinstance().AddListener<CubeCell>(EventTypes.Cell_OnSelected,CellSelection.Getinstance().CellSelected);
        MonoBase.Getinstance().GetMono().AddUpdateListener(() => {
            if (Application.isFocused == isinBackground)
            {
                if (Application.isFocused)
                { AudioManager.Getinstance().Resume(); }
                else
                { AudioManager.Getinstance().Mute(); }
                isinBackground = !Application.isFocused;
            }
        });
    }
    void OnGameStart(object info)
    {
        Debug.Log("[消息]游戏开始");
        GameObject grid = ResManager.Getinstance().Load<GameObject>("Prefabs/GridSystem/GridSystem");
        grid.transform.SetParent(GameObject.Find("Ground").transform,false);
        GridSystem = grid.GetComponent<GridSystem>();
        GridSystem.enabled = true;
        GridSystem.gameObject.name = "GridSystem";
        HolyWaterSystem.Getinstance().Init();
        RoundSystem.Getinstance().Init(1);
        UIManager.Getinstance();
        UIManager.Getinstance().PushPanel(PanelTypes.HUDPanel);
        //GameNetwork.Getinstance().GetNetWorkManager();
        UIManager.Getinstance().MsgOnScreen("游戏开始啦!");
        AudioManager.Getinstance().PlayBGM("1");
        AudioManager.Getinstance().PlaySound("GameMain/GameStart", 0.05f);
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
    public void Unload()
    {
        UIManager.Destroy();
        EventManager.Destroy();
        RoundSystem.Destroy();
        HolyWaterSystem.Destroy();
    }
    public void SetGameMain(GameMain _gameMain)
    {
        Unload();
        gameMain = _gameMain;
    }
}