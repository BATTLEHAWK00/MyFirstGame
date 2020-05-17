using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buff;
public class _Debug : MonoBehaviour
{
    public void NextRound()
    {
        RoundSystem.Get().NextRound();
    }
    public void ForceToDie()
    {
        UnitSelection.Get().GetStart().CurrentUnit.ForceDie();
    }
    public void Buff()
    {
        UIManager.Get().PushPanel(PanelTypes.ChooseBuffPanel);
    }
    public void AddCard()
    {
        CardSystem.Get().AddCard(new Slay());
    }
}
