using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseBuffPanel : PanelBase
{
    [SerializeField]
    private Dropdown dropDown=null;
    public void Select()
    {
        Buff.Buff buff;
        switch (dropDown.value)
        {
            case 0: buff = new Buff.Heal(3, 1); break;
            case 1: buff = new Buff.Poison(3, 1); break;
            case 2: buff = new Buff.LightShield(3); break;
            case 3: buff = new Buff.MultiAttack(3); break;
            case 4: buff = new Buff.Triumph(3); break;
            default: buff = null; break;
        }
        if (buff != null)
            UnitSelection.Getinstance().GetStart().CurrentUnit.AddBuff(buff);
        Cancel();
    }
    public void Cancel()
    {
        UIManager.Getinstance().PopPanel();
    }
}
