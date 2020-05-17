using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : BaseManager<CardSystem>
{
    Transform cardContent;
    List<CardBase> cardList = new List<CardBase>();
    CardsPanel cardsPanel;
    public void SetCardsPanel(CardsPanel cardsPanel)
    {
        this.cardsPanel = cardsPanel;
    }
    public Transform CardContent
    {
        get
        {
            return cardContent;
        }
        set
        {
            if (cardContent == null)
                cardContent = value;
        }
    }
    public void AddCard(CardBase cardtype)
    {
        //if(cardContent.parent.parent.GetComponent<>)
        cardsPanel.Anim_Up(0.5f);
        MonoBase.Get().GetMono().RunDelayTask(() => {
            ResManager.Get().LoadAsync<GameObject>("Prefabs/UI/Panels/HUDPanel/Card/Card", (obj) => {
                obj.transform.SetParent(cardContent, false);
                obj.GetComponent<Card>().SetCardBase(cardtype);
                cardtype.SetCardPanel(obj);
                MonoBase.Get().GetMono().RunDelayTask(() => {
                    cardsPanel.Anim_Down(1f);
                }, 1f);
            });
        }, 0.25f);

    }
}
