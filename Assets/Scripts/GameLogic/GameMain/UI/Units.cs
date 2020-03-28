using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Units : BaseManager<UI_Units>
{
    private CubeCell CurrentSelected;
    private GameObject CallPanel;
    public CubeCell GetCurrentSelected()
    {
        return CurrentSelected;
    }
    public void CellSelected(CubeCell cubeCell)
    {
        if (CallPanel == null)
            CallPanel = GameObject.Find("UI/Canvas/Call/Call_Panel");
        CallPanel.SetActive(true);
        CurrentSelected = cubeCell;
        Debug.Log("单元格选中:" + cubeCell.Position.X + "," + cubeCell.Position.Y);
    }
}
