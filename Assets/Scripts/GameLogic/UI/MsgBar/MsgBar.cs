using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgBar : MonoBehaviour
{
    private float seconds = 0;
    [Range(1,10f)]
    public float RemainTime = 3f;
    private Animator animator;
    private RectTransform rectTransform;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
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
        seconds -= Time.deltaTime;
    }
    public void Die()
    {
        animator.Play("Disappear");
    }
    public void destroy()
    {
        Destroy(gameObject);
    }
}
