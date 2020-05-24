using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MsgBar : MonoBehaviour
{
    private float seconds = 0;
    [Range(1,10f)]
    public float RemainTime = 1f;
    private bool IsPaused=false;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = scale * 0.25f;
        transform.DOScale(scale,0.5f).SetEase(Ease.OutBack);
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().DOFade(1f, 0.5f);
        seconds = RemainTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds <= 0)
            End();
        if(!IsPaused)
            seconds -= Time.deltaTime;
    }
    public void OnPause()
    {
        IsPaused = true;
    }
    public void OnResume()
    {
        IsPaused = false;
        seconds = 0.25f;
    }
    public void End()
    {
        Vector3 scale = transform.localScale;
        GetComponent<CanvasGroup>().DOFade(0f, 0.4f);
        transform.DOScale(scale*0.25f, 0.5f).OnComplete(()=> {
            Destroy(gameObject);
        });
    }
}
