using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicSystemManager : BaseManager<GameLogicSystemManager>
{
    List<IGameLogicSystem> systemsList = new List<IGameLogicSystem>();
    public bool AddSystem(IGameLogicSystem system)
    {
        if (systemsList.Contains(system))
            return false;
        systemsList.Add(system);
        return true;
    }
    public void RemoveSystem(IGameLogicSystem system)
    {
        systemsList.Remove(system);
    }
    protected override void onDestroy()
    {
        foreach (var i in systemsList)
            if(i != null)
                i.ForceDestroy();
        systemsList = null;
    }
}
