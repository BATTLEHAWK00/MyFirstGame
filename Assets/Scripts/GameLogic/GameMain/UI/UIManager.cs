using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    private GameObject HUD;
    private GameObject MsgBar;
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
    public void MsgOnScreen(string text)
    {
        EventManager.Getinstance().EventTrigger<string>("UI_MsgBar", text);
    }
    public void Init()
    {
        HUD = GameObject.Find("UI/Canvas/HUD");
        MsgBar = GameObject.Find("UI/Canvas/HUD/MsgBar");
    }
}
