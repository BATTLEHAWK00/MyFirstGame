using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : BaseManager<UnitSelection>
{
    private CubeCell start;
    private CubeCell end;
    public bool Waiting;
    public void Set(CubeCell cubeCell)
    {
        if (Waiting)
            return;
        if (start == null)
        {
            start = cubeCell;
            UIManager.Getinstance().MsgOnScreen("已选中"+start.CurrentUnit.UnitName);
            return;
        }
        
        if(end==null)
        {
            end = cubeCell;
            Attack attack = new Attack();
            attack.AttackTarget(start.CurrentUnit, end.CurrentUnit);
            end = null;
        }
            
    }
    public CubeCell GetStart()
    {
        return start;
    }
    public CubeCell GetEnd()
    {
        return end;
    }
    public void ClearSelection()
    {
        start = null;end = null;
    }
    public UnitSelection()
    {
        TheGame.Getinstance().GameMain.MonoManager.AddUpdateListener(()=> {
            if (Input.GetMouseButtonDown(1))
            {
                if (start == null)
                    return;
                ClearSelection();
                UIManager.Getinstance().MsgOnScreen("已清除当前选择");
            }
        });
    }
}
