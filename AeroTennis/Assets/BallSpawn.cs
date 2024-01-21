using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BallSpawn : NetworkBehaviour
{
    public GameObject yourPrefab;
    public Vector3 spawnPosition = new Vector3(0f, .25f, 0f);

    [Command]
    public void CmdSpawnBall()
    {
        // Check if the client has authority
        if (!isOwned)
        {
            // Do not proceed without authority
            Debug.Log("ERROR NO AUTHORITY");
            return;
        }
        else
        {
            Debug.Log("Ball Spawned");
        }

        // Instantiate and spawn the ball on the server
        GameObject spawnedObject = Instantiate(yourPrefab, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(spawnedObject);
    }
}