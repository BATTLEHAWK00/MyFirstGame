using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicSystemManager : BaseManager<GameLogicSystemManager>
{
    List<GameLogicSystem<object>> systemsList = new List<GameLogicSystem<object>>();
    public bool AddSystem(GameLogicSystem<object> system)
    {
        Debug.Log(system.GetType().Name);
        if (systemsList.Contains(system))
            return false;
        systemsList.Add(system);
        return true;
    }
    public void RemoveSystem(GameLogicSystem<object> system)
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
