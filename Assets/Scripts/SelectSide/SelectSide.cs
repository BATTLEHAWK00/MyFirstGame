using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectA()
    {
        Game.GameInitialize(0);
        SceneManager.LoadScene("GameMain");
    }
    public void SelectB()
    {
        Game.GameInitialize(1);
        SceneManager.LoadScene("GameMain");
    }
}
