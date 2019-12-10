using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject State_Text;
    public GameObject State_Panel;
    public GameObject HPText;
    public GameObject HP_Panel;
    public GameObject MouseCircle;
    public GameObject Plane;
    public GameObject MovingCircle1;
    public GameObject MovingCircle2;
    public GameObject RoundDisplay;
    public GameObject QiziNubmerDisplay;
    public GameObject DebugPanel;
    //private GameObject SideA_HP;
    //private GameObject SideB_HP;
    // Start is called before the first frame update
    void Start() //开局隐藏面板
    {
        //MouseCircle.GetComponent<CanvasRenderer>().SetAlpha(0.3f);
        State_Panel.SetActive(false);
        HP_Panel.SetActive(false);
        MouseCircle.SetActive(false);
        MovingCircle1.SetActive(false);
        MovingCircle2.SetActive(false);
        DebugPanel.SetActive(false);
    }
    void Awake() //初始化
    {
        #region 弃用
        //State_Text = GameObject.Find("/UICanvas_Screen/State_Panel/Text");
        //State_Panel = GameObject.Find("/UICanvas_Screen/State_Panel");
        //HPText = GameObject.Find("/UICanvas_World/HP_Panel/Text");
        //HP_Panel = GameObject.Find("/UICanvas_World/HP_Panel");
        //MouseCircle = GameObject.Find("/UICanvas_World/PointedCircle");
        //Plane = GameObject.Find("/Ambient/Plane");
        //MovingCircle1 = GameObject.Find("/UICanvas_World/MovingStartCircle");
        //MovingCircle2 = GameObject.Find("/UICanvas_World/MovingTargetCircle");
        //RoundDisplay = GameObject.Find("/UICanvas_Screen/RoundDisplay/Text");
        //QiziNubmerDisplay = GameObject.Find("/UICanvas_Screen/QiziNumber_Panel/Text");
        //DebugPanel = GameObject.Find("/UICanvas_Screen/Test/Panel");
        #endregion
        Game.GameInitialize(0);
        Game.InGameInitialize();
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Game.MoveCurrent.AttackMove());
        //回合数显示
        if (Game.GameSystemCurrent.WhoseRound() == Game.GameSystemCurrent.Side)
            RoundDisplay.GetComponent<UnityEngine.UI.Text>().text = "回合数：" + Game.GameSystemCurrent.GetRound().ToString() + "（你的回合）";
        else
            RoundDisplay.GetComponent<UnityEngine.UI.Text>().text = "回合数：" + Game.GameSystemCurrent.GetRound().ToString();
        //回合数显示
        //棋子数显示
        int n = 0;
        if (Game.GameSystemCurrent.Side == 0)
        {
            foreach (Shuxing i in Game.GameSystemCurrent.ShuxingListA)
                n++;
        }
        else
        {
            foreach (Shuxing i in Game.GameSystemCurrent.ShuxingListB)
                n++;
        }
        QiziNubmerDisplay.GetComponent<UnityEngine.UI.Text>().text = "当前棋子数：" + n.ToString();
        //棋子数显示
    }


    public void ShowState(GameObject b) //显示状态
    {
        Shuxing a = GameFunc.GetObjectShuxing(b);
        State_Panel.SetActive(true);
        State_Text.GetComponent<UnityEngine.UI.Text>().text = ("职业："+a.Name+"\n" + a.GetState());
    }
    public void HideState() //隐藏状态
    {
        State_Panel.SetActive(false);
    }
    public void ShowHP(GameObject a,float b) //显示血量
    {
        HP_Panel.SetActive(true);
        HP_Panel.transform.position = a.transform.position  + Vector3.up * b;
        HP_Panel.transform.rotation = Camera.main.transform.rotation;
        HPText.GetComponent<UnityEngine.UI.Text>().text = "HP:" + GameFunc.GetObjectShuxing(GameFunc.GetMousePointedObject()).Hp.ToString(); //显示血量
    }
    public void HideHP() //隐藏血量
    {
        HP_Panel.SetActive(false);
    }
    public void ShowCirle(GameObject a) //显示圆圈
    {
        Vector3 position;
        MouseCircle.SetActive(true);

        position = a.transform.position;
        position.y = Plane.transform.position.y + 0.1f;

        MouseCircle.transform.position = position ;

    }

    public void HideCirle() //隐藏圆圈
    {
        MouseCircle.SetActive(false);
    }

    public void ShowMovingCirle1(Vector3 a) //显示初物体圆圈
    {
        Vector3 position = a;
        MovingCircle1.SetActive(true);
        position.y = Plane.transform.position.y + 0.1f;
        MovingCircle1.transform.position = position;

    }

    public void HideMovingCirle1() //隐藏初物体圆圈
    {
        MovingCircle1.SetActive(false);
    }

    public void ShowMovingCirle2(Vector3 a) //显示末物体圆圈
    {
        Vector3 position = a;
        MovingCircle2.SetActive(true);
        position.y = Plane.transform.position.y + 0.1f;
        MovingCircle2.transform.position = position;

    }

    public void HideMovingCirle2() //隐藏末物体圆圈
    {
        MovingCircle2.SetActive(false);
    }

    public void LockObjectToGround(GameObject a,float offset) //将物体锁在地面
    {
        Vector3 position= a.transform.position;
        position.y = Plane.transform.position.y+offset;
        a.transform.position = position;
    }
    public void test()
    {
        if(DebugPanel.activeSelf)
            DebugPanel.SetActive(false);
        else
            DebugPanel.SetActive(true);
    }
    public void test1()
    {
        Game.GameSystemCurrent.Test1();
    }
    public void next() //下一回合
    {
        Game.GameSystemCurrent.NextRound();
    }
}
