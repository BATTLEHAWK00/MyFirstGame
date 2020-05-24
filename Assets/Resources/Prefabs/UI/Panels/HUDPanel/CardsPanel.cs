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
    [SerializeField]
    private bool open;
    float width;
    float originalPosition;
    [SerializeField]
    bool OnMouse;
    private void Awake()
    {
        width = (transform as RectTransform).rect.height;
        originalPosition = transform.position.y;
        CardSystem.Get().CardContent = Content;
        CardSystem.Get().SetCardsPanel(this);
        //Debug.Log(originalPosition);
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
        if (open)
            return;
        transform.DOMoveY(originalPosition, duration).OnComplete(() =>
        {
            Vector3 vector = transform.position;
            vector.y = originalPosition;
            transform.position = vector;
            open = true;
            //EDebug.Log(transform.position.y);
        });
    }
    public void Anim_Down(float duration)
    {
        if (!open)
            return;
        transform.DOMoveY(originalPosition - width * HideAmount, 0.5f).SetEase(Ease.OutQuad).OnComplete(()=> {
            open = false;
        });
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //return;
        OnMouse = false;
        StartCoroutine(WaitForAnim_Down());
    }
    public IEnumerator WaitForAnim_Down()
    {
        while (CardSystem.Get().Busy())
            yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.5f);
        if (!OnMouse)
            Anim_Down(1f);
    }
}
