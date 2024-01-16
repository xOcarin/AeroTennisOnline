using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class networkManager : NetworkManager
{
    //[SerializeField] private PlayerObjectController GamePlayerPrefab;

    public void StartGame(string SceneName)
    {
        ServerChangeScene(SceneName);
    }
    
}
