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
    public GameObject playerCamera;
    
    

    public GameObject ball;
    public Vector3 spawnPosition = new Vector3(0f, .25f, 0f);
    public GameObject spawnpoint1;
    public GameObject spawnpoint2;
    

    void Start()
    {
        
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
        GameObject spawnedObject = Instantiate(ball, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(spawnedObject);
    }
    
    
    
    void FixedUpdate()
    {
            // Control the local player
            Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            Vector3 localInputDirection = playerCamera.transform.TransformDirection(inputDirection);
            rigidbody3d.velocity = localInputDirection * speed * Time.fixedDeltaTime;
    }
}