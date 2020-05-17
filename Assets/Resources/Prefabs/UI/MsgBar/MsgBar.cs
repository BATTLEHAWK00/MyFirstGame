using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgBar : MonoBehaviour
{
    private float seconds = 0;
    [Range(1,10f)]
    public float RemainTime = 1f;
    private bool IsPaused=false;
    private Animator animator;
    private RectTransform rectTransform;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
        UIManager.Get().PushMsgBar(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        seconds = RemainTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds <= 0)
            Die();
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
    public void Die()
    {
        if(animator!=null)
            animator.Play("Disappear");
    }
    public void destroy()
    {
        UIManager.Get().PopMsgBar();
        Destroy(gameObject);
    }
}
