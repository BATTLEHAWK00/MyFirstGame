using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardBase
{
    protected string cardName;
    protected string cardDesctription;
    protected int holyWaterCost;
    private GameObject cardPanel;
    public string CardName
    {
        get { return cardName; }
    }
    public string CardDescription
    {
        get { return cardDesctription; }
    }
    public int HolyWaterCost
    {
        get { return holyWaterCost; }
    }
    public void Destroy() 
    {
        GameObject.Destroy(cardPanel);
    }
    public void SetCardPanel(GameObject card)
    {
        cardPanel = card;
    }
}
