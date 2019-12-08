using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public uint HolyWater = 0;  //定义圣水
    public int HP=5;     //定义血量
    public int Side;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
            Game.GameWin(Side);
    }
}
