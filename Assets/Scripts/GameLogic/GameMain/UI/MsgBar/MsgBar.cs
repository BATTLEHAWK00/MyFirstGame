using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgBar : MonoBehaviour
{
    private float seconds = 0;
    private void Awake()
    {
        EventManager.Getinstance().AddListener<string>("UI_MsgBar", Event_Msg);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
        if(seconds>0)
            seconds += 1.5f;
        else
            seconds += 3f;
        //Invoke("Die", 5f);
    }
    public void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
