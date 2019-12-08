using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Place : MonoBehaviour,IPointerClickHandler
{
    private Generate generate_script;

    public void OnPointerClick(PointerEventData eventData)
    {
        Shuxing a;
        a = GameFunc.GetObjectShuxing(UnitGeneration.NewObject);
        //bool finish = generate_script.finish;
        //GameObject NewObject = generate_script.NewObject;
        if (a!=null)
        {
            a.BelongToWho = Game.GameSystemCurrent.Side;
            a.isBeingPlaced = false;
            Game.GameSystemCurrent.IsPlacing = false;
            UnitGeneration.NewObject = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        generate_script = GameObject.Find("/UICanvas_Screen/Test/Panel/GenerateObject").GetComponent<Generate>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
