using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    public UILayers Layer { get; protected set; } = UILayers.Mid;
    public virtual void OnEnter() { }
    public virtual void OnExit() { Destroy(gameObject); }
    public virtual void OnPause() { }
    public virtual void OnResume() { }
}
