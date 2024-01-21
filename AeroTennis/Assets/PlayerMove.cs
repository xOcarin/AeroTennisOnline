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

    void FixedUpdate()
    {
        Debug.Log(playerCount);
        if (isLocalPlayer)
        {
            // Control the local player's racket
            rigidbody3d.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0) * speed * Time.fixedDeltaTime;
        }
    }
}