using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;

public class PlayerMove : NetworkBehaviour
{
    public float speed = 30;
    public Rigidbody rigidbody3d;

    // Counter to track the number of connected players
    private static int playerCount = 0;
    public GameObject playerCamera;
    
    private managmentScrip gameManager;

    
    
    
    
    void FixedUpdate()
    {
        
            // Control the local player
            Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            Vector3 localInputDirection = playerCamera.transform.TransformDirection(inputDirection);
            rigidbody3d.velocity = localInputDirection * speed * Time.fixedDeltaTime;
    }
}