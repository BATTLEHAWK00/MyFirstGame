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

    public void MsgOnScreen(string text)
    {
        ResManager.Getinstance().LoadAsync<GameObject>("Prefabs/UI/HUD/MsgBar",(obj)=> {
            if (obj == null)
                Debug.LogError("[错误]公告栏无法加载!");
            obj.transform.SetParent(HUD.transform,false);
            obj.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
        });
    }
    public void PushPanel(PanelTypes panelType)
    {
        panelStack.Push(GetPanel(panelType));
    }
    public PanelBase GetPanel(PanelTypes paneltype)
    {
        PanelBase panel;
        if (panelInstancesDic.TryGetValue(paneltype, out panel) && panel != null)
            return panel;
        else
            ResManager.Getinstance().LoadAsync<GameObject>("UI/" + panelPrefabsPathDic[paneltype], (obj) =>
            {
                var panelbase = obj.GetComponent<PanelBase>();
                panelInstancesDic.Add(paneltype, panelbase);
                obj.transform.SetParent(Layer(panelbase.Layer));
            });
        return panel;
    }
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
        //panelPrefabsPathDic.Add();
        #endregion
    }
}
