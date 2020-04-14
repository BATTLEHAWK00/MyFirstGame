using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    #region 私有成员
    private Transform hud;
    private Transform canvas;
    private Transform top;
    private Transform mid;
    private Transform bottom;
    private Transform system;
    private Dictionary<PanelTypes, string> panelPrefabsPathDic = new Dictionary<PanelTypes, string>();
    private Dictionary<PanelTypes, PanelBase> panelInstancesDic = new Dictionary<PanelTypes, PanelBase>();
    private Dictionary<MultiPanelTypes, List<PanelBase>> multipanelInstancesDic = new Dictionary<MultiPanelTypes, List<PanelBase>>();
    private Stack<PanelBase> panelStack = new Stack<PanelBase>();
    #endregion
    #region 外部只读属性
    public Transform Canvas { get { return canvas; } }
    public Transform Top { get { return top; } }
    public Transform Mid { get { return mid; } }
    public Transform Bottom { get { return bottom; } }
    public Transform System { get { return system; } }
    public Transform HUD { get { return hud.transform; } }
    #endregion
    #region 公告消息
    private GameObject MsgBarPrefab = null;
    private Stack<MsgBar> msgBarsStack = new Stack<MsgBar>();
    public void MsgOnScreen(string text)    //广播消息
    {
        if (MsgBarPrefab == null)
            MsgBarPrefab = ResManager.Getinstance().Load<GameObject>("Prefabs/UI/HUD/MsgBar", false);
        ResManager.Getinstance().LoadAsync(MsgBarPrefab, (obj) => {
            if (obj == null)
                Debug.LogError("[错误]公告栏无法加载!");
            obj.transform.SetParent(HUD.transform, false);
            obj.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        });
    }
    public void PushMsgBar(MsgBar msgBar)
    {
        if (msgBarsStack.Count > 0)
            msgBarsStack.Peek().OnPause();
        msgBarsStack.Push(msgBar);
    }
    public void PopMsgBar()
    {
        if (msgBarsStack.Count <= 0)
            return;
        MsgBar msgBar = msgBarsStack.Peek();
        msgBar.Die();
        msgBarsStack.Pop();
        if (msgBarsStack.Count > 0)
            msgBarsStack.Peek().OnResume();
    }
    #endregion

    #region 控制面板的开启和关闭
    public void PushPanel(PanelTypes panelType)
    {
        while (panelStack.Count > 0 && panelStack.Peek() == null)
                panelStack.Pop();
        if (panelStack.Count > 0)
            panelStack.Peek().OnPause();
        var panel = getPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }
    public void PopPanel()
    {
        if (panelStack.Count == 0)
            return;
        var panel = panelStack.Peek();
        if(panel!=null)
            panel.OnExit();
        else while(panel==null)
           panel = panelStack.Pop();
        if (panelStack.Count == 0)
            return;
        panelStack.Peek().OnResume();
    }
    public void ForceExitPanel(PanelTypes panelType)
    {
        panelInstancesDic[panelType].OnExit();
    }
    public void SwitchPanel(PanelTypes panelType)
    {
        Debug.Log(panelStack.Count);
        if (panelInstancesDic.ContainsKey(panelType) && panelInstancesDic[panelType] != null)
            panelInstancesDic[panelType].OnExit();
        else
            PushPanel(panelType);
    }
    public PanelBase GetPanel(PanelTypes panelType)
    {
        if (panelInstancesDic.ContainsKey(panelType) && panelInstancesDic[panelType] != null)
            return panelInstancesDic[panelType];
        return null;
    }
    private PanelBase getPanel(PanelTypes paneltype)
    {
        if (panelInstancesDic.ContainsKey(paneltype) && panelInstancesDic[paneltype] != null)
            return panelInstancesDic[paneltype];
        var panel = ResManager.Getinstance().Load<GameObject>("Prefabs/UI/Panels/" + panelPrefabsPathDic[paneltype]);
        var panelbase = panel.GetComponent<PanelBase>();
        if (panelInstancesDic.ContainsKey(paneltype))
            panelInstancesDic[paneltype] = panelbase;
        else
            panelInstancesDic.Add(paneltype, panelbase);
        panelbase.transform.SetParent(Layer(panelbase.Layer), false);
        return panelbase;
    }
    #endregion
    #region 控制多实例UI的开启和关闭

    #endregion
    Transform Layer(UILayers layer)
    {
        switch (layer)
        {
            case UILayers.Bottom:return bottom;
            case UILayers.Mid:return mid;
            case UILayers.Top:return top;
            case UILayers.System:return system;
            case UILayers.HUD:return hud;
            default:return null;
        }
    }
    public UIManager()
    {
        canvas = GameObject.Find("UI/Canvas").transform;
        top = Canvas.transform.Find("Top");
        mid = Canvas.transform.Find("Mid");
        bottom = Canvas.transform.Find("Bottom");
        system = Canvas.transform.Find("System");
        hud = system.Find("HUD");
        #region Prefab路径
        panelPrefabsPathDic.Add(PanelTypes.UnitGenerationPanel,"GameLogic/UnitGenerationPanel/Call_Panel");
        panelPrefabsPathDic.Add(PanelTypes.GameSettingPanel, "GameSettingPanel");
        panelPrefabsPathDic.Add(PanelTypes.RoundPanel, "GameLogic/RoundsPanel");
        panelPrefabsPathDic.Add(PanelTypes.HolyWaterPanel, "GameLogic/HolyWaterPanel");
        #endregion
    }
}
public enum UILayers
{
    Top,
    Mid,
    Bottom,
    System,
    HUD
}