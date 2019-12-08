using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    private UI ui_script;
    public GameObject Plane;
    // Start is called before the first frame update
    void Start()
    {
        ui_script = GameObject.Find("/UI").GetComponent<UI>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position;
        if(UnitGeneration.NewObject !=null)
        {
            position = GameFunc.GetMousePointedPosition();
            position.y = Plane.transform.position.y + 10f;
            UnitGeneration .NewObject.transform.position = position;
        }
    }
    public void _GenerateArcher()
    {
        UnitGeneration.GenerateArcher();
    }

}
public static class UnitGeneration
{
    public static GameObject NewObject = null;
    public static void GenerateArcher()
    {
        if (NewObject == null)
        {
            NewObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/archer"));
            GameFunc.GetObjectShuxing(NewObject).isBeingPlaced = true;
            Game.GameSystemCurrent.IsPlacing = true;
        }
    }
    public static void GenerateChangmaoshou()
    {
        if (NewObject == null)
        {
            NewObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/changmaoshou"));
            GameFunc.GetObjectShuxing(NewObject).isBeingPlaced = true;
            Game.GameSystemCurrent.IsPlacing = true;
        }
    }
    public static void GenerateFarmer()
    {
        if (NewObject == null)
        {
            NewObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/farmer"));
            GameFunc.GetObjectShuxing(NewObject).isBeingPlaced = true;
            Game.GameSystemCurrent.IsPlacing = true;
        }
    }
    public static void GenerateFashi()
    {
        if (NewObject == null)
        {
            NewObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/fashi"));
            GameFunc.GetObjectShuxing(NewObject).isBeingPlaced = true;
            Game.GameSystemCurrent.IsPlacing = true;
        }
    }
    public static void GenerateRider()
    {
        if (NewObject == null)
        {
            NewObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/rider"));
            GameFunc.GetObjectShuxing(NewObject).isBeingPlaced = true;
            Game.GameSystemCurrent.IsPlacing = true;
        }
    }
}