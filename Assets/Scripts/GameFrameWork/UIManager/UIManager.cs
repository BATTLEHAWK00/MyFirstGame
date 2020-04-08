using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    private GameObject hud;
    private GameObject MsgBar;
    private Transform canvas;
    private Transform top;
    private Transform mid;
    private Transform bottom;
    private Transform system;
    #region 外部只读属性
    public Transform Canvas { get { return canvas; } }
    public Transform Top { get { return top; } }
    public Transform Mid { get { return mid; } }
    public Transform Bottom { get { return bottom; } }
    public Transform System { get { return system; } }
    public Transform HUD { get { return hud.transform; } }
    #endregion
    /*
    public void MsgOnScreen(string text)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UI/MsgBar"));
        if (obj == null)
            Debug.LogError("[错误]公告栏无法加载!");
        Debug.Log(obj.name);
        obj.transform.SetParent(HUD.transform);
        obj.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
    }
    */
    public void ShowPanel(string panelname)
    {
        ResManager.Getinstance().LoadAsync<GameObject>("UI/"+panelname,(panel)=> { 
            
        });
    }
    public void MsgOnScreen(string text)
    {
        EventManager.Getinstance().EventTrigger<string>("UI_MsgBar", text);
    }
    public UIManager()
    {
        //ResManager.Getinstance().Load<GameObject>("Prefabs/UI/UI");
        canvas = GameObject.Find("UI/Canvas").transform;
        top = Canvas.transform.Find("Top");
        mid = Canvas.transform.Find("Mid");
        bottom = Canvas.transform.Find("Bottom");
        system = Canvas.transform.Find("System");
        hud = Top.Find("HUD").gameObject;
        MsgBar = Top.Find("HUD/MsgBar").gameObject;
    }
}
