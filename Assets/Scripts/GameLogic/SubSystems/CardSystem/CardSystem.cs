using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : GameLogicSystem<CardSystem>
{
    Transform cardContent;
    List<CardBase> cardList = new List<CardBase>();
    CardsPanel cardsPanel;
    const float interval = 0.5f;
    float cooldown = 0f;
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
    public bool Busy()
    {
        if (cooldown > 0)
            return true;
        else
            return false;
    }
    public void AddCard(CardBase cardtype)
    {
        cardsPanel.Anim_Up(0.5f);
        MonoBase.Get().GetMono().RunDelayTask(() => {
            ResManager.Get().LoadAsync<GameObject>("Prefabs/UI/Panels/HUDPanel/Card/Card", (obj) => {
                obj.transform.SetParent(cardContent, false);
                obj.GetComponent<Card>().SetCardBase(cardtype);
                cardtype.SetCardPanel(obj);
                MonoBase.Get().GetMono().StartCoroutine(cardsPanel.WaitForAnim_Down());
            });
        }, cooldown);
        cooldown = interval;
    }
    void CoolDownTimer()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
        else if (cooldown < 0)
            cooldown = 0;
    }
    protected override void onDestroy()
    {
        MonoBase.Get().GetMono().RemoveUpdateListener(CoolDownTimer);
        Debug.Log("卡牌系统销毁完毕");
    }
    public CardSystem()
    {
        MonoBase.Get().GetMono().AddUpdateListener(CoolDownTimer);
    }
}
