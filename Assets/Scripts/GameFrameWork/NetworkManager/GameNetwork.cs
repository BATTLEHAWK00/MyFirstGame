using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameNetwork : BaseManager<GameNetwork>
{
    private NetworkManager networkManager;
    public NetworkManager GetNetWorkManager()
    {
        if(networkManager==null)
        {
            GameObject obj = ResManager.Get().Load<GameObject>("Prefabs/Network/NetworkManager");
            GameObject.DontDestroyOnLoad(obj);
            networkManager = obj.GetComponent<NetworkManager>();
        }
        return networkManager;
    }
}