﻿using System.Collections;
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
            end.GetComponent<MeshRenderer>().material.color = Color.blue;
            UnitAttack.Getinstance().AttackTarget(start.CurrentUnit, end.CurrentUnit);
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
        Mono.Getinstance().GetMono().AddUpdateListener(()=> {
            if (Input.GetMouseButtonDown(1))
            {
                if (start == null)
                { end = null;return; }
                ClearSelection();
                UIManager.Getinstance().MsgOnScreen("已清除当前选择");
            }
        });
    }
}
