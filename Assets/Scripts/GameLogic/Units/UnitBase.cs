using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSounds
{
    public readonly string OnDeath = "Units/OnDeath";
    public readonly string OnBorn = "Units/OnBorn";
}
public class UnitBase : MonoBehaviour
{
    #region 私有成员
    private string _unitName;
    private UnitType _unitType;
    private int _side;
    private CubeCell _position;
    #endregion
    #region 公有成员
    public string UnitName { get { return _unitName; } }    //获取单位显示名称
    public UnitType UnitType { get { return _unitType; } }  //单位类型
    public int GetHP() { return _HP; }  //获取HP
    public int Side { get { return _side; } }   //属于哪一方
    public CubeCell GetPosition(){ return _position;}   //获取单元格位置
    #endregion
    #region 单位共有属性
    protected int _HP=-1;    //生命
    protected int Attack;   //攻击力
    protected int AttackRange;  //攻击距离
    protected string _Description;    //单位描述
    #endregion
    // Start is called before the first frame update
    bool CheckInitError()
    {
        if (_HP == -1)
        {
            Debug.LogError("[单位]HP未初始化!");
            return true;
        }
        return false;
    }
    void Awake()
    {
        
    }
    void Start()
    {
        //检测初始化是否有错误
        if (CheckInitError())
        {
            Destroy(gameObject);
            return;
        }
        //添加事件
        EventAdd();
        EventManager.Getinstance().EventTrigger("Unit_OnUnitBirth",gameObject);
        //Invoke("Die", 3);
        //子类初始化函数
        _Start();
        AudioManager.Getinstance().PlaySound(new GameSounds().UnitSounds.OnBorn, 0.2f);
        EventManager.Getinstance().EventTrigger<string>("UI_MsgBar", UnitName + "被召唤了出来!");
    }
    public void Die() { _HP = 0; }
    void EventAdd()
    {
        //添加事件
        EventManager.Getinstance().AddListener<GameObject>("Unit_OnUnitDeath", OnDeathBroadcast);
        EventManager.Getinstance().AddListener<GameObject>("Unit_OnUnitBirth", OnBirthBroadcast);
    }
    // Update is called once per frame
    void Update()
    {
        #region HP检测
        if (_HP <= 0)
            OnDeath();
        #endregion
        _Update();
    }
    virtual protected void _Start() { }
    virtual protected void _Update() { }
    protected void SetUnitType(string name,UnitType unitType)
    {
        _unitName = name;
        _unitType = unitType;
    }
    public void CostHP(int amount)
    {
        _HP -= amount;
    }
    
    void OnDeath()
    {
        //触发单位死亡事件(Unit_OnUnitDeath)
        EventManager.Getinstance().EventTrigger("Unit_OnUnitDeath", gameObject);
        //播放死亡音效
        AudioManager.Getinstance().PlaySound(new GameSounds().UnitSounds.OnDeath, 0.2f);
        //广播死亡消息
        EventManager.Getinstance().EventTrigger<string>("UI_MsgBar", UnitName + "已死亡!");
        //销毁物体
        Destroy(gameObject);
    }
    void OnDeathBroadcast(GameObject info)
    {
        if (info == gameObject)
            Debug.Log(string.Format("[消息]{0}({1})已死亡", info.GetComponent<UnitBase>().UnitName,info.GetInstanceID()));
    }
    void OnBirthBroadcast(GameObject info)
    {
        if(info == gameObject)
            Debug.Log(string.Format("[消息]{0}({1})已生成", info.GetComponent<UnitBase>().UnitName,info.GetInstanceID()));
    }
    private void OnDestroy()
    {
        //事件销毁
        EventManager.Getinstance().RemoveListener<GameObject>("Unit_OnUnitDeath", OnDeathBroadcast);
        EventManager.Getinstance().RemoveListener<GameObject>("Unit_OnUnitBirth", OnBirthBroadcast);
    }
}
