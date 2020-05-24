using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class UnitBase
{
    #region UI组件
    protected GameObject HP_Bar;
    #endregion
    #region UI方法
    public void ShowMessage(string msg)
    {
        ResManager.Get().LoadAsync<GameObject>("Prefabs/UI/Unit/MessageText/MessageText", (obj) => {
            if (gameObject == null)
                return;
            obj.transform.SetParent(transform.Find("HP_Bar"), false);
            obj.SetActive(false);
            obj.transform.Translate(Vector3.up * Height * 0.15f);
            obj.GetComponent<MessageText>().SetText(msg);
            obj.SetActive(true);
        });
    }
    public void ShowHP()
    {
        HP_Bar.SetActive(true);
        HP_Bar.GetComponentInParent<HP_Bar>().Seconds = 2f;
    }

    public void HideHP()
    {
        HP_Bar.SetActive(false);
    }
    #endregion
}