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
    public Material OccupiedMaterial;
    private Material CurrentMaterial;
    private MeshRenderer meshRenderer;
    private bool OnMouse = false;
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
        _Position.X = x;
        _Position.Y = y;
        //Debug.Log(x.ToString() + y.ToString());
    }
    void MovePosition() //处理物体移动
    {

    }
    public void OnPointerClick(PointerEventData eventData)  //鼠标点击事件
    {
        OnMouse = false;
        if(RoundSystem.Getinstance().IsWaitingPlayer())
            EventManager.Getinstance().EventTrigger<CubeCell>("Grid_OnSelected",this);
        AudioManager.Getinstance().PlaySound("Grid/OnClick", 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        meshRenderer.material = HighLightedMaterial;
        OnMouse = true;
        AudioManager.Getinstance().PlaySound("Grid/OnMouse",0.1f);
        //Debug.Log("鼠标进入");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        meshRenderer.material = NormalMaterial;
        //Debug.Log("鼠标退出");
    }
}
