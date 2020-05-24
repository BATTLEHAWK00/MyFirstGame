using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Card : MonoBehaviour,IPointerClickHandler
{
    private CardBase cardBase;
    [SerializeField]
    private Text CardNameText = null;
    [SerializeField]
    private Text CardCostText = null;
    public void SetCardBase(CardBase cardBase)
    {
        this.cardBase = cardBase;
    }
    // Start is called before the first frame update
    void Start()
    {
        CardNameText.text = cardBase.CardName;
        CardCostText.text = "圣水花费:" + cardBase.HolyWaterCost.ToString();
        Anim_Appear();
    }
    void Anim_Appear()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        Vector3 scale = transform.localScale;
        transform.localScale = scale * 0.25f;
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBack);
        AudioManager.Get().PlaySound("UI/Cards/OnCardAdd",0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        cardBase.Destroy();
    }
}
