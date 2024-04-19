using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : NetworkBehaviour
{
    public float speed = 30;
    public Rigidbody rigidbody3d;

    // Counter to track the number of connected players
    private static int playerCount = 0;
    public GameObject playerCamera;
    
    private managmentScrip gameManager;

    public GameObject ball;
    public Vector3 spawnPosition = new Vector3(0f, .25f, 0f);
    
    public Button leaveButton;
    public Button host;
    private bool isLeaveButtonActive = false;
    
    
    //old
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



    void Update()
    {
        // Check if the server should be shut down
        if (isServer && ShouldShutDownServer())
        {
            ShutDownServer();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isLeaveButtonActive = !isLeaveButtonActive;
            Cursor.visible = isLeaveButtonActive;
            
        }
    }

    bool ShouldShutDownServer()
    {
        // Implement your logic here to determine when the server should be shut down
        // For example, you might check if a certain key is pressed or if a condition is met
        // This function should return true when the condition to shut down the server is met
        return false;
    }

    void ShutDownServer()
    {
        // Call the NetworkManager's StopServer function to shut down the server
        NetworkManager.singleton.StopServer();
    }
    
    
    
    
    
    
    public void GoToMainMenu()
    {
        /*
        print("clicking");
        
        // Disconnect the client
        if (NetworkClient.isConnected)
        {
            // If the client is also the server host, stop the server
            if (NetworkServer.active)
            {
                SceneManager.LoadScene("MainMenu");
                NetworkServer.DisconnectAll();
                NetworkServer.Shutdown();
            }
            
            // Disconnect the client
            NetworkClient.Disconnect();
        }
        
        // Load the MainMenu scene
        SceneManager.LoadScene("MainMenu");
        */
    }
}