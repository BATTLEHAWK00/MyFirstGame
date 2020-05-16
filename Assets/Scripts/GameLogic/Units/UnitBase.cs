using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Mirror;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening.Core.Easing;

public class UnitSounds
{
    public readonly string OnDeath = "Units/OnDeath";
    public readonly string OnBorn = "Units/OnBorn";
}
/// <summary>
/// 单位基本属性类
/// </summary>
public abstract class UnitBase : NetworkBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    #region Unity编辑器里编辑
    public float Height = 10f;
    #endregion
    #region 私有成员
    private CubeCell _position; //单元格位置
    #endregion
    #region 公有成员
    public int GetHP() { return HP; }  //获取HP
    public CubeCell GetPosition() { return _position; }   //获取单元格位置
    public void SetPosition(CubeCell cubeCell) { _position = cubeCell; }    //设置单元格位置
    #endregion
    #region 外部访问器
    public string Description { get; protected set; }    //单位描述
    public int AttackRange { get; protected set; } = 2;  //单位攻击距离
    public int Attack { get; protected set; } = 1;    //单位攻击力
    public int MaxHP { get; protected set; }     //最大生命值
    public string UnitName { get; protected set; }   //单位名
    public UnitType UnitType { get { return unitType; } } //单位类型

    #endregion
    #region 单位共有属性
    private bool dead = false;
    public bool CanOperate = true;
    protected int HP = -1;    //生命
    protected UnitType unitType;
    public int HolyWaterCost { get; protected set; } = 1;
    protected delegate void AttackDelegate(UnitBase Target);
    protected AttackDelegate AttackFunc;
    #endregion
    #region Buff状态
    //Buff列表
    private List<Buff.Buff> unitBuffs = new List<Buff.Buff>();
    //抵挡攻击
    public bool missAttack = false;
    //双重攻击
    public bool dualAttack = false;
    //凯旋
    public bool triumph = false;
    #endregion
    #region Debug方法
    public void ForceDie() { HP = 0; }
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

    }
    void EventRemove()
    {
        //事件销毁

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
            //UIManager.Getinstance().MsgOnScreen(UnitName + "被召唤了出来!");
        }
    }
    void Die() 
    {
        dead = true;
        HideHP();
        //GetComponent<MeshRenderer>().material.DOFade(0f,1f);
        transform.DOMoveY(transform.position.y+5f, 0.25f).SetEase(Ease.InOutQuad).SetDelay(0.25f).OnComplete(() => {
            transform.DOScale(0.25f, 0.25f).SetEase(Ease.InBack).OnComplete(()=> {
                //BeforeDeath事件
                BeforeDeath();
                //触发OnDeath事件
                OnDeath();
                //销毁物体
                Destroy(gameObject);
            });
        });
        /*
        //死亡动画
        transform.DOShakePosition(1f, 2f, 10).OnComplete(()=> {
            //触发OnDeath事件
            OnDeath();
            //销毁物体
            Destroy(gameObject);
        });*/
    }
    void OnDeath()
    {
        //播放死亡音效
        AudioManager.Getinstance().PlaySound(new GameSounds().UnitSounds.OnDeath, 0.2f);
        //广播死亡消息
        UIManager.Getinstance().MsgOnScreen(UnitName + "已死亡!");
        //行计数器扣除
        GameGlobal.Getinstance().GameMain.GridSystem.RowCounter[GetPosition().Position.y]--;
        //触发单位死亡事件(Unit_OnUnitDeath)
        EventManager.Getinstance().EventTrigger(EventTypes.Unit_OnDeath, gameObject);
        //事件移除
        EventRemove();
        //解引用
        GetPosition().CurrentUnit = null;
        //移除回合系统列表
        RoundSystem.Getinstance().RemoveUnit(this);
    }
    void OnBorn()
    {
        //行计数器加1
        GameGlobal.Getinstance().GameMain.GridSystem.RowCounter[GetPosition().Position.y]++;
        //添加事件
        EventAdd();
        EventManager.Getinstance().EventTrigger(EventTypes.Unit_OnBorn, gameObject);
        //向回合系统列表添加单位
        RoundSystem.Getinstance().AddUnit(this);
    }
    void BeforeDeath()
    {

    }
    void AfterBorn()
    {
        //出生动画
        Vector3 po = transform.position;
        po.y += 100f;
        transform.position = po;
        transform.DOMoveY(transform.position.y - 100f, 0.4f).SetEase(Ease.InQuad).OnComplete(() => {
            //transform.DOShakePosition(0.25f, 1f, 20, default, default, false);
            Camera.main.transform.DOShakePosition(0.3f, 0.05f, 20, default, default, true);
            AudioManager.Getinstance().PlaySound(new GameSounds().UnitSounds.OnBorn, 0.2f);
        });
        
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
        //OnBorn事件
        OnBorn();
        //子类初始化函数
        _Start();
        //AfterBorn事件
        AfterBorn();
    }
    // Update is called once per frame
    void Update()   //帧刷新事件
    {
        #region HP检测
        if (!dead && HP <= 0)
            Die();
        #endregion
        _Update();
        if (onUpdate != null)
            onUpdate.Invoke();
    }
    void Awake()
    {
        if (!HolyWaterSystem.Getinstance().CostHolyWater(HolyWaterCost))
        {
            Destroy(gameObject); return;
        }
        _Awake();
        ResManager.Getinstance().LoadAsync<GameObject>("Prefabs/UI/Unit/HP_Bar", (obj) => {
            obj.name = "HP_Bar";
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
        ShowHP();
        if (missAttack)
        {
            ShowMessage("光盾:格挡成功");
            return;
        }
        HP -= amount;
        ShowMessage("-" + amount);
    }
    public void AddHP(int amount)
    {
        ShowHP();
        int _amount;
        if (MaxHP - HP >= amount)
            _amount = amount;
        else
            _amount = MaxHP-HP;
        if (HP >= MaxHP)
            return;
        HP += amount;
        AudioManager.Getinstance().PlaySound("Units/OnHeal",0.2f);
        ShowMessage("+" + _amount);
    }
    #endregion
    #region UI方法
    public void ShowMessage(string msg)
    {
        ResManager.Getinstance().LoadAsync<GameObject>("Prefabs/UI/Unit/MessageText/MessageText", (obj) => {
            if (gameObject == null)
                return;
            obj.transform.SetParent(transform.Find("HP_Bar"), false);
            obj.SetActive(false);
            obj.transform.Translate(Vector3.up * Height * 0.15f);
            obj.GetComponent<MessageText>().SetText(msg);
            obj.SetActive(true);
        });
    }
    public void ShowHP()
    {
        HP_Bar.SetActive(true);
        HP_Bar.GetComponentInParent<HP_Bar>().Seconds = 2f;
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
    #region 状态Buff
    public void AddBuff(Buff.Buff buff)
    {
        Debug.Log(string.Format("给{0}添加Buff:{1}", UnitName, buff.GetType().Name));
        buff.SetTarget(this);
        unitBuffs.Add(buff);
    }
    public void RemoveBuff(Buff.Buff buff)
    {
        Debug.Log(string.Format("给{0}移除Buff:{1}", UnitName, buff.GetType().Name));
        unitBuffs.Remove(buff);
    }
    #endregion
}
