using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardsPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Transform Content = null;
    const float HideAmount = 0.75f;
    float width;
    float originalPosition;
    bool OnMouse;
    private void Awake()
    {
        width = (transform as RectTransform).rect.height;
        originalPosition = transform.position.y;
        CardSystem.Get().CardContent = Content;
        CardSystem.Get().SetCardsPanel(this);
        Debug.Log(originalPosition);
        Vector3 po = transform.position;
        po.y = originalPosition - width * HideAmount;
        transform.position = po;
    }
    private void Start()
    {
        StartCoroutine(Anim_UpDown());
    }
    public IEnumerator Anim_UpDown()
    {
        yield return new WaitForSeconds(0.5f);
        Anim_Up(1f);
        yield return new WaitForSeconds(1f);
        Anim_Down(1f);
        yield break;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //return;
        OnMouse = true;
        Anim_Up(0.5f);
        //(transform as RectTransform).DOMoveY(originalPosition, 0.5f).SetEase(Ease.InOutQuad);
    }
    public void Anim_Up(float duration)
    {
        transform.DOMoveY(originalPosition, duration).OnComplete(() =>
        {
            Vector3 vector = transform.position;
            vector.y = originalPosition;
            transform.position = vector;
            Debug.Log(transform.position.y);
        });
    }
    public void Anim_Down(float duration)
    {
        MonoBase.Get().GetMono().RunDelayTask(() =>
        {
            if (!OnMouse)
                transform.DOMoveY(originalPosition - width * HideAmount, 0.5f).SetEase(Ease.OutQuad);
        }, duration);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //return;
        OnMouse = false;
        Anim_Down(0.5f);
    }
}
