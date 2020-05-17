using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public UIManager asd;
    public int a;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
            UIManager.Get().MsgOnScreen("LocalPlayer");
    }
    private int frames=0;
    // Update is called once per frame
    void Update()
    {
        frames++;
        if (!hasAuthority)
            a++;
        if (frames == 30)
        { 
            frames = 0;
        }
    }
}
