using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgBar : MonoBehaviour
{
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
        
    }
    public void Event_Msg(string msg)
    {
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = msg;
        gameObject.SetActive(true);
        Invoke("Die", 5f);
    }
    public void Die()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
