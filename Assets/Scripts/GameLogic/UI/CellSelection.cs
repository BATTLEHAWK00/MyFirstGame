using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSelection : BaseManager<CellSelection>
{
    private CubeCell CurrentSelected;
    private GameObject CallPanel;
    public CubeCell GetCurrentSelected()
    {
        return CurrentSelected;
    }
    public void CellSelected(CubeCell cubeCell)
    {
        CurrentSelected = cubeCell;
        Debug.Log("单元格选中:" + cubeCell.Position.x + "," + cubeCell.Position.y);
        if (cubeCell.CurrentUnit != null)
            return;
        UnitSelection.Getinstance().ClearSelection();
        UIManager.Getinstance().PushPanel(PanelTypes.UnitGenerationPanel);
    }
}
