using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MessageText : MonoBehaviour
{
    private float time=3f;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
        (transform as RectTransform).DOAnchorPos(Vector2.up * 3f, 3f);
        GetComponent<Text>().DOFade(0f, 3f);
        transform.DOScale(0.02f, 0.25f).OnComplete(() => {
            transform.DOScale(0.01f, 2f);
        });
    }
    public void SetText(string text)
    {
        GetComponent<Text>().text = text;
    }
    // Update is called once per frame
    void Update()
    {
        if (time > 0f)
            time -= Time.deltaTime;
        if (time <= 0f)
            Destroy(gameObject);
    }
}
