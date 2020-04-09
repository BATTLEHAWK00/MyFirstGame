using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class UnitSounds
{
    public readonly string OnDeath = "Units/OnDeath";
    public readonly string OnBorn = "Units/OnBorn";
}
/// <summary>
/// 单位基本属性类
/// </summary>
public class UnitBase : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    #region Unity编辑器里编辑
    public float Height=10f;
    #endregion
    #region 私有成员
    private CubeCell _position; //单元格位置
    #endregion
    #region 公有成员
    public int GetHP() { return HP; }  //获取HP
    public CubeCell GetPosition() { return _position; }   //获取单元格位置
    public void SetPosition(CubeCell cubeCell) { _position = cubeCell; }    //设置单元格位置
    #endregion
    #region 单位共有属性
    public bool CanOperate = true;
    public string UnitName { get; protected set; }   //单位名
    public UnitType unitType { get; protected set; } //单位类型
    protected int HP = -1;    //生命
    public string Description { get; protected set; }    //单位描述
    public int AttackRange { get; protected set; } = 2;  //单位攻击距离
    public int Attack { get; protected set; } = 1;    //单位攻击力
    public int MaxHP { get; protected set; }     //最大生命值
    private List<Buff> buffs = new List<Buff>();
    private List<Buff> deBuffs = new List<Buff>();
    protected delegate void AttackDelegate(UnitBase Target);
    protected AttackDelegate AttackFunc;
    #endregion
    #region Debug方法
    public void Die() { HP = 0; }
    #endregion
    #region UI组件
    protected GameObject HP_Bar;
    #endregion
    #region 委托
    protected UnityAction onUpdate;
    #endregion
    #region 单位初始化
    protected void SetUnitType(string name, UnitType unitType)
    {
        UnitName = name;
        this.unitType = unitType;
    }
    bool CheckInitError()
    {
        if (HP == -1)
        {
            Debug.LogError("[单位]HP未初始化!");
            return true;
        }
        return false;
    }

    #endregion
    #region 单位事件
    void EventAdd()
    {
        //添加事件
        EventManager.Getinstance().AddListener<GameObject>("Unit_OnUnitDeath", OnDeathBroadcast);
        EventManager.Getinstance().AddListener<GameObject>("Unit_OnUnitBirth", OnBirthBroadcast);
    }
    void EventRemove()
    {
        //事件销毁
        EventManager.Getinstance().RemoveListener<GameObject>("Unit_OnUnitDeath", OnDeathBroadcast);
        EventManager.Getinstance().RemoveListener<GameObject>("Unit_OnUnitBirth", OnBirthBroadcast);
    }
    void OnDeathBroadcast(GameObject info)
    {
        if (info == gameObject) //若事件本体为此物体
        {

        }
            //Debug.Log(string.Format("[消息]{0}({1})已死亡", info.GetComponent<UnitBase>().UnitName, info.GetInstanceID()));
    }
    void OnBirthBroadcast(GameObject info)
    {
        if (info == gameObject) //若事件本体为此物体
        {
            //Debug.Log(string.Format("[消息]{0}({1})已生成", info.GetComponent<UnitBase>().UnitName, info.GetInstanceID()));
            UIManager.Getinstance().MsgOnScreen(UnitName + "被召唤了出来!");
        }
    }
    void OnDeath()
    {
        //触发单位死亡事件(Unit_OnUnitDeath)
        EventManager.Getinstance().EventTrigger("Unit_OnUnitDeath", gameObject);
        //播放死亡音效
        AudioManager.Getinstance().PlaySound(new GameSounds().UnitSounds.OnDeath, 0.2f);
        //广播死亡消息
        UIManager.Getinstance().MsgOnScreen(UnitName + "已死亡!");
        //行计数器扣除
        TheGame.Getinstance().GameMain.GridSystem.RowCounter[GetPosition().Position.y]--;
        GetPosition().CurrentUnit = null;
        //移除回合系统列表
        RoundSystem.Getinstance().RemoveUnit(this);
        //销毁物体
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        EventRemove();
    }
    #endregion
    #region MonoBehavior模块事件
    // Start is called before the first frame update
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
        EventManager.Getinstance().EventTrigger("Unit_OnUnitBirth", gameObject);
        //向回合系统列表添加单位
        RoundSystem.Getinstance().AddUnit(this);
        //子类初始化函数
        _Start();
        AudioManager.Getinstance().PlaySound(new GameSounds().UnitSounds.OnBorn, 0.2f);
    }
    // Update is called once per frame
    void Update()   //帧刷新事件
    {
        #region HP检测
        if (HP <= 0)
            OnDeath();
        #endregion
        _Update();
        if (onUpdate != null)
            onUpdate.Invoke();
    }
    void Awake() 
    { 
        _Awake();
        ResManager.Getinstance().LoadAsync<GameObject>("Prefabs/UI/Unit/HP_Bar",(obj)=> {
            HP_Bar = obj.transform.Find("Bar").gameObject;
            HP_Bar.SetActive(false);
            obj.transform.SetParent(this.transform);
            Vector3 vector3 = this.transform.position;
            vector3.y += Height;
            obj.transform.position = vector3;
            obj.transform.rotation = Camera.main.transform.rotation;
            obj.GetComponent<HP_Bar>().SetUnit(this);
        });
    }
    virtual protected void _Awake() { }
    virtual protected void _Start() { }
    virtual protected void _Update() { }
    #endregion
    #region 单位方法
    public void CostHP(int amount)  //扣血方法
    {
        HP -= amount;
    }
    #endregion
    #region UI方法
    public void ShowHP()
    {
        HP_Bar.SetActive(true);
        HP_Bar.GetComponentInParent<HP_Bar>().Seconds = 1f;
    }

    public void HideHP()
    {
        HP_Bar.SetActive(false);
    }
    #endregion
    #region 输入事件
    public void OnPointerEnter(PointerEventData eventData)
    {
        _position.OnPointerEnter(eventData);
        ShowHP();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _position.OnPointerExit(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _position.OnPointerClick(eventData);
    }
    #endregion
}
