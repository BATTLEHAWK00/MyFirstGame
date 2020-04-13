using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
