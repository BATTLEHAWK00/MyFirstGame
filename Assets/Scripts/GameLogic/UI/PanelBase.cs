using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : MonoBehaviour
{
    protected UILayers layer;
    public UILayers Layer { get { return layer; } }
}
public enum UILayers
{
    Top,
    Mid,
    Bottom,
    System
}