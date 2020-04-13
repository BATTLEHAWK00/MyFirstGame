using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HP_Bar : MonoBehaviour
{
    private UnitBase unitBase;
    public GameObject bar;
    public UnityEngine.UI.Text text;
    public float Seconds;
    private void Awake()
    {
        //bar = transform.Find("Bar").gameObject;
        // unitBase = transform.parent.gameObject.GetComponent<UnitBase>();
        unitBase = GetComponentInParent<UnitBase>();
    }
    private int LastHP = -1;
    private void Update()
    {
        if (Seconds <= 0)
            bar.SetActive(false);
        text.text = "HP:" + unitBase.GetHP().ToString();
        CheckHPChange();
        //Debug.Log(unitBase.gameObject);
        Seconds -= Time.deltaTime;
    }
    public void SetUnit(UnitBase unitBase)
    {
        this.unitBase = unitBase;
    }
    public void CheckHPChange()
    {
        if (LastHP == -1)
            LastHP = unitBase.GetHP();
        if (LastHP != unitBase.GetHP())
        {
            StartCoroutine(Anim_HP(LastHP, unitBase.GetHP(), 1f));
            
        }
        LastHP = unitBase.GetHP();
        IEnumerator Anim_HP(int LastHP, int Now, float speed)
        {
            float lastvalue = (float)LastHP / unitBase.MaxHP;
            float targetvalue = (float)Now / unitBase.MaxHP;
            float currentvalue = bar.GetComponent<UnityEngine.UI.Slider>().value;
            Seconds = 1f;
            //检测动画是否完成
            if (LastHP > Now)
            {
                if (currentvalue <= targetvalue)
                    yield break;
            }
            else
            {
                if (currentvalue >= targetvalue)
                    yield break;
            }
            //控制加速
            speed += 0.1f;
            //改变血条
            if (LastHP > Now)
                bar.GetComponent<UnityEngine.UI.Slider>().value -= 0.01f * speed;
            else
                bar.GetComponent<UnityEngine.UI.Slider>().value += 0.01f * speed;
            yield return new WaitForSeconds(0.01f);
            yield return Anim_HP(LastHP, Now, speed);
        }
    }
}
