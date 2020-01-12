using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum UnitType
{
    Archer,
    Changmaoshou,
    Farmer,
    Fashi,
    Rider
}
public class UnitMain : MonoBehaviour
{
    private UI ui_script;
    virtual public Shuxing Shuxing { get; set; }
    public float Top_Offset=new float();
    protected UnitType _Type;
    private int _Side;
    #region 外部只读属性
    public UnitType Type { get { return _Type; } }
    public int Side { get { return _Side; } }
    #endregion
    public void SetSide(int side){_Side = side;}
    // Start is called before the first frame update
    void Start()
    {
        ui_script = GameObject.Find("/UI").GetComponent<UI>();
        Game.GameSystemCurrent.UnitList.Add(this);
    }
    // Update is called once per frame
    void Update()
    {
        //if(gameObject==null)
        //    Debug.Log("null");
        //if (!shuxing.isBeingPlaced)
        //{
        //    ui_script.LockObjectToGround(gameObject, 0f);
        //}
        CheckHp();  //检查HP 为零销毁
    }

    void CheckHp()
    {
        if (Shuxing.Hp <= 0)
        {
            //Qizi.ShuxingList.Remove();
            Game.GameSystemCurrent.UnitList.Remove(this);
            GameObject.Destroy(gameObject);
        }
    }
    public void HP_Operate(int value)
    {
        Shuxing.Hp += value;
    }
}

public abstract class Shuxing
{
    #region 本身属性
    public string Name;
    public int Hp;
    public int Attack;
    public int Attackrange;
    #endregion
    #region 功能属性
    public bool isBeingPlaced;
    public int BelongToWho;
    #endregion
    //public int Burned;
    //public int Frozen;
    //public int Poisoned;
    //public int Vulnerable;
    //public bool Lightsheld;
    //public bool Charge;
    //public bool Assistance;
    //public bool Triumphal;
    //public int Crazy;
    //public bool NoArmour;
    //public bool Combo;

    public Shuxing(int a, int b, int c)
    {
        Hp = a;
        Attack = b;
        Attackrange = c;
    }

    public string GetState()   //显示人物属性
    {
        string content = "属性：";
        content += ("\nHP：" + Hp.ToString());
        content += ("\n攻击力：" + Attack.ToString());
        content += ("\n攻击范围：" + Attackrange.ToString());
        return content;
    }

}