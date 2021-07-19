using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;

public class MsgBar : MonoBehaviour
{
    private float seconds = 0;
    [Range(1, 10f)]
    public float RemainTime = 1f;
    private bool IsPaused = false;
    private bool IsDestoying = false;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = scale * 0.25f;
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBack, 5f);
        GetComponent<CanvasGroup>().alpha = 0f;
        GetComponent<CanvasGroup>().DOFade(1f, 0.5f).SetEase(Ease.OutBack);
        seconds = RemainTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds <= 0 && !IsDestoying)
        {
            IsDestoying = true;
            StartCoroutine(End());
        }
        if (!IsPaused)
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
    public IEnumerator End()
    {
        Vector3 scale = transform.localScale;
        Tween tweener1 = GetComponent<CanvasGroup>().DOFade(0f, 1f).SetEase(Ease.InBack);
        Tween tweener2 = transform.DOScale(scale * 0.1f, 1f).SetEase(Ease.InBack);
        yield return tweener1.WaitForCompletion();
        yield return tweener2.WaitForCompletion();
        Destroy(gameObject);
    }
}
