using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode] //让网格在Unity编辑器里出现
public class GridSystem : MonoBehaviour
{
    #region 此处变量在Unity中修改
    public uint Width;  //宽度
    public uint Length; //长度
    public CubeCell CubePrefab; //正方形单元预制
    public GameObject Ground;   //地面
    public Vector3 Offset;  //位置偏移
    #endregion
    CubeCell[] CubeCells;   //单元格数组
    void Awake()    //建立单元格系统
    {
        if (transform.childCount > 0)
            return;
        CubeCells = new CubeCell[Width * Length];
        for (uint i = 0,z = 0;z < Width;z++)
            for (uint x = 0; x < Length; x++)
                CreateCell(x,z, i++);
    }
   void Start()
    {

    }
    void CreateCell(uint x, uint z, uint i) //创建单元格
    {
        Vector3 position;
        position.x = x * 10f;
        position.y = Ground.transform.localPosition.y;
        position.z = z * 10f;
        CubeCell Cell = CubeCells[i] = Instantiate<CubeCell>(CubePrefab);
        Cell.SetPosition(x,z);
        Cell.transform.SetParent(transform, false);
        Cell.transform.localPosition = position+Offset;
        Cell.name = "CubeCell (" + x.ToString() + "," + z.ToString() + ")";
    }

    public CubeCell FindCubeCell(VectorInGame a) //按坐标查找单元格
    {
        for(uint i=0;i<CubeCells.Length;i++)
        {
            if (CubeCells[i].Position.X == a.X & CubeCells[i].Position.Y == a.Y)
                return CubeCells[i];
        }
        return null;
    }
}
public struct VectorInGame
{
    public uint X;
    public uint Y;

    public uint Distance()
    {
        Debug.Log((uint)Mathf.CeilToInt(Mathf.Sqrt(X * X + Y * Y)));
        return (uint)Mathf.CeilToInt(Mathf.Sqrt(X * X + Y * Y));
    }

}
