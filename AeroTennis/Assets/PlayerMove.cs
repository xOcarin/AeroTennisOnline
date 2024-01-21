using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
    public float speed = 30;
    public Rigidbody rigidbody3d;

    // Counter to track the number of connected players
    private static int playerCount = 0;
    
    

    public GameObject yourPrefab;
    public Vector3 spawnPosition = new Vector3(0f, .25f, 0f);

    void Start()
    {
        
        if (isServer)
        {
            // Increment the player count when a new player connects to the server
            playerCount++;
        }

        if (playerCount == 0)
        {
            transform.position = new Vector3(2f, 0f, -3f);
        }
        
    }


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
    
    
    
    void FixedUpdate()
    {
        //Debug.Log(playerCount);
        if (isLocalPlayer)
        {
            // Control the local player's racket
            rigidbody3d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * speed * Time.fixedDeltaTime;
        }
    }
}