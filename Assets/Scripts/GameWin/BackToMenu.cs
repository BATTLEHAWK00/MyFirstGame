using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    GameObject a = GameObject.Find("/Canvas/Panel/Text");
    // Start is called before the first frame update
    void Start()
    {
        //if (b == 0)
        //    a.GetComponent<UnityEngine.UI.Text>().text = "A赢了！";
        //else
        //    a.GetComponent<UnityEngine.UI.Text>().text = "B赢了！";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Back2Menu()
    {
        SceneManager.LoadScene("SelectSide");
    }
}
