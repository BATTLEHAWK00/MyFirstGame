using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFunc
{
    public static Shuxing GetObjectShuxing(GameObject a) //获取相应物体属性
    {
        if (a == null)
            return null;
        if (a.GetComponentInParent<Changmaoshou>() != null)
            return a.GetComponentInParent<Changmaoshou>().Shuxing;

        else if (a.GetComponentInParent<Archer>() != null)
            return a.GetComponentInParent<Archer>().Shuxing;

        else if (a.GetComponentInParent<Farmer>() != null)
            return a.GetComponentInParent<Farmer>().Shuxing;

        else if (a.GetComponentInParent<Fashi>() != null)
            return a.GetComponentInParent<Fashi>().Shuxing;

        else if (a.GetComponentInParent<Rider>() != null)
            return a.GetComponentInParent<Rider>().Shuxing;
        return null;

    }
    public static Vector3 GetMousePointedPosition()    //捕获鼠标所指位置
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log("当前位置：" + hit.point.ToString());
            return hit.point;
        }
        else
            return Vector3.zero;
    }

    public static GameObject GetMousePointedObject()   //捕获鼠标所指物体
    {
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
            return hit.collider.gameObject;
        else
            return null;
    }

    public static void ShuxingInList(ref Shuxing a,List<Shuxing> b)
    {
        foreach(Shuxing i in b)
        {
            if (i != a)
                b.Add(a);
        }
    }
}
