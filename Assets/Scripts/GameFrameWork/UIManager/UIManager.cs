using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    #region 私有成员
    private GameObject hud;
    private GameObject MsgBar;
    private Transform canvas;
    private Transform top;
    private Transform mid;
    private Transform bottom;
    private Transform system;
    private Dictionary<PanelTypes, string> panelPrefabsPathDic = new Dictionary<PanelTypes, string>();
    private Dictionary<PanelTypes, PanelBase> panelInstancesDic = new Dictionary<PanelTypes, PanelBase>();
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

    public void MsgOnScreen(string text)    //广播消息
    {
        ResManager.Getinstance().LoadAsync<GameObject>("Prefabs/UI/HUD/MsgBar",(obj)=> {
            if (obj == null)
                Debug.LogError("[错误]公告栏无法加载!");
            obj.transform.SetParent(HUD.transform,false);
            obj.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        });
    } 
    #region 控制面板的开启和关闭
    public void PushPanel(PanelTypes panelType)
    {
        if (panelStack.Count > 0)
            panelStack.Peek().OnPause();
        var panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }
    public void PopPanel(PanelTypes panelType)
    {
        if (panelStack.Count == 0)
            return;
        var panel = panelStack.Peek();
        panel.OnExit();
        panelStack.Pop();
        if (panelStack.Count == 0)
            return;
        panelStack.Peek().OnResume();
    }
    private PanelBase GetPanel(PanelTypes paneltype)
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
        hud = Top.Find("HUD").gameObject;
        #region Prefab路径
        panelPrefabsPathDic.Add(PanelTypes.UnitGenerationPanel,"GameLogic/UnitGenerationPanel/Call_Panel");
        #endregion
    }
}
public enum UILayers
{
    Top,
    Mid,
    Bottom,
    System
}