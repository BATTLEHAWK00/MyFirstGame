using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPos : MonoBehaviour
{
    private GameObject _Camera;
    public float BackwardOffset = 2f;
    public float HeightOffset = 5f;
    public float HorizontalOffset;
    // Start is called before the first frame update
    void Start()
    {
        _Camera = Camera.main.gameObject;
        Vector3 vector = gameObject.GetComponent<GridSystem>().FindCubeCell(new Vector2Int(gameObject.GetComponent<GridSystem>().Center().x, 0)).transform.position;
        vector.z -= BackwardOffset;
        vector.y += HeightOffset;
        vector.x += HorizontalOffset;
        Camera.main.transform.position = vector;
    }
}
