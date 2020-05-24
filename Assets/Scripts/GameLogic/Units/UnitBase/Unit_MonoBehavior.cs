using System.Collections;
using System.Collections.Generic;
using UnityEngine;

partial class UnitBase
{
    #region MonoBehavior模块事件
    // Start is called before the first frame update
    void Start()
    {
        if (!HolyWaterSystem.Get().CostHolyWater(HolyWaterCost))
        {
            Destroy(gameObject); return;
        }
        ResManager.Get().LoadAsync<GameObject>("Prefabs/UI/Unit/HP_Bar", (obj) => {
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
        _Awake();
        gameObject.AddComponent<ShowOutline>().enabled = false;
        Color color = Color.white;
        color.a = 0.5f;
        GetComponent<ShowOutline>().OutlineColor = color;
        GetComponent<ShowOutline>().OutlineWidth = 5;
    }
    virtual protected void _Awake() { }
    virtual protected void _Start() { }
    virtual protected void _Update() { }
    #endregion
}
