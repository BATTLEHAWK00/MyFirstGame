using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Move:MonoBehaviour
{
    public GameObject UI;
    private UI ui_script;
    private bool isMoving = false;
    private Vector3 m_begin;
    private Vector3 m_target;
    public GameObject MovingObject;
    public GameObject TargetObject;
    private float MoveSpeed = initspeed;
    const float acceleration = 5f,initspeed=5f;

    public void Awake()
    {
        Game.MoveCurrent = this;
    }
    public IEnumerator AttackMove()
    {
        if (MovingObject == null)
        {
            isMoving = false;
            ui_script.HideMovingCirle1();
            ui_script.HideMovingCirle2();
        }
        if (!isMoving && MovingObject != null && TargetObject != null && !isMoving)   //移过去
        {
            yield return StartCoroutine(MoveTo());
            TargetObject.GetComponent<UnitMain>().HP_Operate(-1);
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(MoveBack());
        }
        yield return null;
    }
    IEnumerator MoveTo()
    {
        isMoving = true;
        while(Vector3.Distance(MovingObject.transform.position, m_target) > 2f)
        {
            MoveSpeed += acceleration;
            ui_script.ShowMovingCirle1(m_begin);
            MovingObject.transform.position = Vector3.MoveTowards(MovingObject.transform.position, m_target, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        MoveSpeed = initspeed;
        yield return null;
    }
    IEnumerator MoveBack()
    {
        while(Vector3.Distance(MovingObject.transform.position, m_begin) != 0f)
        {
            MoveSpeed += acceleration;
            MovingObject.transform.position = Vector3.MoveTowards(MovingObject.transform.position, m_begin, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        MoveInit();
        ui_script.HideMovingCirle1();
        ui_script.HideMovingCirle2();
    }
    void MoveInit()
    {
        MovingObject = null;
        TargetObject = null;
        isMoving = false;
        MoveSpeed = 5f;
    }
    public void CheckMove(GameObject OnClickObject)
    {
        if (Game.GameSystemCurrent.isWaiting)
        {
            if (!isMoving && MovingObject==null)    //设置移动物体
            {
                MovingObject = OnClickObject;
                m_begin = OnClickObject.transform.position;
                ui_script.ShowMovingCirle1(m_begin);
            }
            else if (!isMoving && TargetObject==null)  //设置目标
            {
                m_begin = MovingObject.transform.position;
                m_target = OnClickObject.transform.position;
                TargetObject = OnClickObject;
                ui_script.ShowMovingCirle2(m_target);
            }
        }
    }
    public void Start()
    {
        ui_script = UI.GetComponent<UI>();
    }
    }

