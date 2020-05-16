using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buff;
public class _Debug : MonoBehaviour
{
    public void NextRound()
    {
        RoundSystem.Getinstance().NextRound();
    }
    public void ForceToDie()
    {
        UnitSelection.Getinstance().GetStart().CurrentUnit.ForceDie();
    }
    public void Buff()
    {
        UIManager.Getinstance().PushPanel(PanelTypes.ChooseBuffPanel);
    }

}
