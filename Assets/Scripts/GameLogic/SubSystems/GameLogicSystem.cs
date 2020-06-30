using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameLogicSystem 
{
    void ForceDestroy();
}
public abstract class GameLogicSystem<T>:BaseManager<T>,IGameLogicSystem where T:new()
{
    public GameLogicSystem()
    {
        Debug.Log(this.GetType().Name+"启动");
        GameLogicSystemManager.Get().AddSystem(this as IGameLogicSystem);
    }
}
