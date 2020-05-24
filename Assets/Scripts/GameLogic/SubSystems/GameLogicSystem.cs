using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameLogicSystem<T>:BaseManager<T> where T:new()
{
    public GameLogicSystem()
    {
        Debug.Log(this.GetType().Name+"启动");
        //GameLogicSystemManager.Get().AddSystem(this as GameLogicSystem<object>);
    }
}
