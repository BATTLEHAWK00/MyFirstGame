using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeCell : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    #region 只读变量
    public VectorInGame Position { get { return _Position; } }
    #endregion
    public Vector3 CenterOffset;    //中心偏移 在Unity中修改
    private VectorInGame _Position;
    public GameObject CurrentObject;    //单元内所处物体
    public Material HighLightedMaterial;
    public Material NormalMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!Game.GameSystemCurrent.IsPlacing)
        {
            MovePosition();
            if (Game.SelectCurrrent.SelectedObject == CurrentObject && Game.SelectCurrrent.SelectedPosition != this)  //判断物体是否发生移动
                 CurrentObject = null;
        }
    }
    public void SetPosition(uint x,uint y)  //初始化单元格坐标
    {
        _Position.X = x;
        _Position.Y = y;
        //Debug.Log(x.ToString() + y.ToString());
    }
    void MovePosition() //处理物体移动
    {
        if (Game.SelectCurrrent.SelectedObject != null && CurrentObject == null)  //判断选择的物体是否处在本空单元
        {
            if (Game.SelectCurrrent.SelectedPosition == this)
            {
                CurrentObject = Game.SelectCurrrent.SelectedObject;
                CurrentObject.transform.position = gameObject.transform.position;
                Game.SelectCurrrent.SelectedObject = null;
                Game.SelectCurrrent.SelectedPosition = null;
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)  //鼠标点击事件
    {
        if(Game.SelectCurrrent.SelectedObject!=null)
            Game.SelectCurrrent.SelectedPosition = gameObject.GetComponent<CubeCell>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material = HighLightedMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material = NormalMaterial;
    }
}
