using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

partial class UnitBase:IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    #region 输入事件
    public void OnPointerEnter(PointerEventData eventData)
    {
        _position.OnPointerEnter(eventData);
        GetComponent<ShowOutline>().enabled = true;
        UnityEditor.Selection.activeGameObject = gameObject;
        if (!dead)
            ShowHP();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _position.OnPointerExit(eventData);
        GetComponent<ShowOutline>().enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _position.OnPointerClick(eventData);
    }
    #endregion
}