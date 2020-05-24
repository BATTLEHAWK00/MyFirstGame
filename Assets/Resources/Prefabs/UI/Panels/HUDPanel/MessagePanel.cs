using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MessagePanel : MonoBehaviour
{
    [SerializeField]
    private Transform Content = null;
    private void Awake()
    {
        UIManager.Get().MessagePanelContent = Content;
        //Debug.Log(GetComponent<ScrollRect>().verticalNormalizedPosition);
    }
}
