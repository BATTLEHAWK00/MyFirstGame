using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    protected UILayers layer;
    public UILayers Layer { get { return layer; } }
    public virtual void OnEnter() { }
    public virtual void OnExit() { Destroy(gameObject); }
    public virtual void OnPause() { }
    public virtual void OnResume() { }
}
