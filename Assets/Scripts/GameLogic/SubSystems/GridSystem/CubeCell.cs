using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class CubeCell : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    #region 只读变量
    public Vector2Int Position { get { return _Position; } }
    #endregion
    [SerializeField]
    private Vector3 CenterOffset;    //中心偏移 在Unity中修改
    private Vector2Int _Position;
    [HideInInspector]
    public UnitBase CurrentUnit;    //单元内所处物体
    [SerializeField]
    private Material HighLightedMaterial=null;
    [SerializeField]
    private Material NormalMaterial=null;
    [SerializeField]
    private Material OccupiedMaterial=null;
    private MeshRenderer meshRenderer;
    public bool isLocal { get; private set; }
    private void Awake()
    {
        if (Position.y <= 1)
            isLocal = true;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //}
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
        if (!RoundSystem.Get().IsWaitingPlayer())
            return;
        EventManager.Get().EventTrigger<CubeCell>(EventTypes.Cell_OnSelected,this);
        if (this.CurrentUnit != null)
            UnitSelection.Get().Set(this);
        if (UnitSelection.Get().GetStart() == this)
            meshRenderer.material = OccupiedMaterial;
        AudioManager.Get().PlaySound("Grid/OnClick", 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UnitSelection.Get().GetStart() != this && UnitSelection.Get().GetEnd() != this)
            meshRenderer.material = HighLightedMaterial;
        AudioManager.Get().PlaySound("Grid/OnMouse",0.1f);
        name = name + "(OnHovering)";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(UnitSelection.Get().GetStart()!=this && UnitSelection.Get().GetEnd()!=this)
            meshRenderer.material = NormalMaterial;
        name = string.Format("CubeCell({0},{1})", Position.x, Position.y);
    }
}
