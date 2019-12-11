using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Display : MonoBehaviour,IPointerEnterHandler ,IPointerExitHandler ,IPointerClickHandler 
{
    private UI UI_Script;

    public void OnPointerClick(PointerEventData eventData)
    {
        Game.MoveCurrent.CheckMove(gameObject);
        Game.SelectCurrrent.Check();
        Game.SelectCurrrent.SelectedObject = gameObject;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(GameFunc.GetObjectShuxing(gameObject).BelongToWho.ToString());
        if (gameObject.tag == "a" && (GameFunc.GetObjectShuxing(gameObject).BelongToWho == Game.GameSystemCurrent.Side))
        {
            UI_Script.ShowHP(gameObject, gameObject.GetComponent<UnitMain>().Top_Offset);
            UI_Script.ShowState(gameObject);
            UI_Script.ShowCirle(gameObject);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (gameObject.tag == "a")
        {

            UI_Script.HideHP();
            UI_Script.HideState();
            UI_Script.HideCirle();

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        UI_Script = GameObject.Find("/UI").GetComponent<UI>();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
