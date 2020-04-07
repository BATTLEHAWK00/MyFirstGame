using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgBar : MonoBehaviour
{
    private float seconds = 0;
    [Range(1,10f)]
    public float RemainTime = 3f;
    private Animation animation;
    private void Awake()
    {
        animation = gameObject.AddComponent<Animation>();
        EventManager.Getinstance().AddListener<string>("UI_MsgBar", Event_Msg);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //AnimationClip clip;
        //clip = Animation.Instantiate<AnimationClip>(Resources.Load<AnimationClip>("Animation/UI/MsgBar/Disppear"));
        //clip.legacy = true;
        //animation.AddClip(clip, "Disappear");
    }

    // Update is called once per frame
    void Update()
    {
        if (seconds <= 0)
            Die();
        seconds -= Time.deltaTime;
    }
    public void Event_Msg(string msg)
    {
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = msg;
        gameObject.SetActive(true);
        seconds = RemainTime;
        AnimationClip clip = Animation.Instantiate<AnimationClip>(Resources.Load<AnimationClip>("Animation/UI/MsgBar/Appear"));
        clip.legacy = true;
        animation.AddClip(clip, "Appear");
        animation.Play("Appear",PlayMode.StopAll);
        //Invoke("Die", 5f);
    }
    public void Die()
    {
        //Destroy(gameObject);
        //animation.Play("Disappear", PlayMode.StopAll);
        gameObject.SetActive(false);
    }
}
