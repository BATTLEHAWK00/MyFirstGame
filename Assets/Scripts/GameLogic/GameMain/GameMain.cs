using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public _debug Debug;
    // Start is called before the first frame update
    void Start()
    {
        TheGame.Getinstance().SetGameMain(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class TheGame : BaseManager<TheGame>
{
    private GameMain gameMain;
    public GameMain GameMain { get { return gameMain; } }
    public _debug Debug()
    {
        return gameMain.Debug;
    }
    public void SetGameMain(GameMain _gameMain)
    {
        gameMain = _gameMain;
    }
}