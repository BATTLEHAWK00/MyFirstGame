using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Move
{
    private UI ui_script = GameObject.Find("/UI").GetComponent<UI>();
    private bool finish = true;
    private bool Set1 = false;
    private bool Set2 = false;
    private bool back = false;
    private Vector3 m_begin;
    private Vector3 m_target;
    private GameObject OnClickObject;
    public GameObject MovingObject;
    public GameObject TargetObject;
    private float MoveSpeed = 20f;

    // Start is called before the first frame update

    public void move()
    {
        if (MovingObject == null)
        {
            Set1 = false;
            Set2 = false;
            finish = true;
            ui_script.HideMovingCirle1();
            ui_script.HideMovingCirle2();
        }
        if ((!finish && (Set1 && Set2)) && !back)   //移过去
        {
            MoveSpeed += 5f;
            // Debug.Log(MoveSpeed.ToString());
            ui_script.ShowMovingCirle1(m_begin);
            MovingObject.transform.position = Vector3.MoveTowards(MovingObject.transform.position, m_target, MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(MovingObject.transform.position, m_target) < 2f)
            {
                back = true;
                MoveSpeed = 5f;
                TargetObject.GetComponent<PlayerLogic>().HP -= 1;
            }
        }
        if (back)   //移回
        {
            MoveSpeed += 5f;
            MovingObject.transform.position = Vector3.MoveTowards(MovingObject.transform.position,m_begin , MoveSpeed * Time.deltaTime);
            if (Vector3.Distance(MovingObject.transform.position, m_begin) ==0f)
            {
                MovingObject = null;
                TargetObject = null;
                back = false;
                finish = true;
                Set1 = false;
                Set2 = false;
                MoveSpeed = 20f;
                ui_script.HideMovingCirle1();
                ui_script.HideMovingCirle2();
            }

        }

        }
        public void CheckMove(GameObject OnClickObject)
        {
            //ui_script = GameObject.Find("/UI").GetComponent<UI>();
            //OnClickObject = GameFunc.GetMousePointedObject ();
            if(Game.GameSystemCurrent.isWaiting)
            {
                if (finish && OnClickObject.tag == "a" && !Set1 && (GameFunc.GetObjectShuxing(OnClickObject).BelongToWho == Game.GameSystemCurrent.Side))    //设置移动物体
                {
                    MovingObject = OnClickObject;
                    m_begin = OnClickObject.transform.position;
                    ui_script.ShowMovingCirle1(m_begin);
                    finish = false;
                    Set1 = true;
                }
                else if (!finish && OnClickObject.tag == "b" && !Set2 && (OnClickObject.GetComponent<PlayerLogic>().Side == Game.GameSystemCurrent.Side))  //设置目标
                {
                    m_begin = MovingObject.transform.position;
                    m_target = OnClickObject.transform.position;
                    TargetObject = OnClickObject;
                    ui_script.ShowMovingCirle2(m_target);
                    Set2 = true;
                }
            }
    }
    }

