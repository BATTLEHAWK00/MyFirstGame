﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode] //让网格在Unity编辑器里出现
public class GridSystem : MonoBehaviour
{
    #region 此处变量在Unity中修改
    public uint Width;  //宽度
    public uint Length; //长度
    public CubeCell CubePrefab; //正方形单元预制
    public GameObject Ground;   //地面
    public Vector3 Offset;  //位置偏移
    public float Distance = 10f;
    #endregion
    public List<CubeCell> CubeCells { get; private set; } = new List<CubeCell>();
    [HideInInspector]
    public List<int> RowCounter = new List<int>();
    //CubeCell[] CubeCells;   //单元格数组
    void Awake()
    {
        // 初始化行计数器
        for (int i = 0; i < Length; i++)
            RowCounter.Add(0);
        // 二维遍历创建单元格
        for (int i = 0, z = 0; z < Width; z++)
            for (int x = 0; x < Length; x++)
                CreateCell(x, z, i++);
    }
   void Start() //建立单元格系统
   {

   }
    private void Update()
    {
        //for(int i=0;i<RowCounter.Count;i++)
        //    Debug.Log(string.Format("第{0}行单位数:{1}", i,RowCounter[i]));
    }
    void CreateCell(int x, int z, int i) //创建单元格
    {
        Vector3 position;
        position.x = x * Distance;
        position.y = Ground.transform.localPosition.y;
        position.z = z * Distance;
        CubeCell Cell = Instantiate<CubeCell>(CubePrefab);
        CubeCells.Add(Cell);
        Cell.SetPosition(x,z);
        Cell.transform.SetParent(transform, false);
        Cell.transform.localPosition = position+Offset;
        Cell.name = "CubeCell (" + x.ToString() + "," + z.ToString() + ")";
    }

    public CubeCell FindCubeCell(Vector2Int a) //按坐标查找单元格
    {
        foreach(var i in CubeCells)
        {
            if (i.Position.x == a.x & i.Position.y == a.y)
                return i;
        }
        return null;
    }
    public Vector2Int Center()
    {
        Vector2Int vector = new Vector2Int();
        vector.x = (int)Width / 2;
        vector.y = (int)Length / 2;
        return vector;
    }
}

