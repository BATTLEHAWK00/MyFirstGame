using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubeCell : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    #region 只读变量
    public Vector2Int Position { get { return _Position; } }
    #endregion
    public Vector3 CenterOffset;    //中心偏移 在Unity中修改
    private Vector2Int _Position;
    [HideInInspector]
    public UnitBase CurrentUnit;    //单元内所处物体
    public Material HighLightedMaterial;
    public Material NormalMaterial;
    public Material OccupiedMaterial;
    //private Material CurrentMaterial;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SetPosition(int x,int y)  //初始化单元格坐标
    {
        _Position.x = x;
        _Position.y = y;
        //Debug.Log(x.ToString() + y.ToString());
    }
    void MovePosition() //处理物体移动
    {

    }
    public void OnPointerClick(PointerEventData eventData)  //鼠标点击事件
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        if (!RoundSystem.Getinstance().IsWaitingPlayer())
            return;
        EventManager.Getinstance().EventTrigger<CubeCell>("Grid_OnSelected",this);
        if (this.CurrentUnit != null)
            UnitSelection.Getinstance().Set(this);
        if (UnitSelection.Getinstance().GetStart() == this)
            meshRenderer.material = OccupiedMaterial;
        AudioManager.Getinstance().PlaySound("Grid/OnClick", 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UnitSelection.Getinstance().GetStart() != this && UnitSelection.Getinstance().GetEnd() != this)
            meshRenderer.material = HighLightedMaterial;
        AudioManager.Getinstance().PlaySound("Grid/OnMouse",0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(UnitSelection.Getinstance().GetStart()!=this && UnitSelection.Getinstance().GetEnd()!=this)
            meshRenderer.material = NormalMaterial;
    }
}
