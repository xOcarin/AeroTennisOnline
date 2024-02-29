using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;



public class swing : NetworkBehaviour
{
    public bool isInRange = false;
    public string shotDir;

    private void Start()
    {
        
    }

    private void Update()
    {
        //Debug.Log("in range: " + isInRange);
        if (Input.GetMouseButtonDown(0))
        {
            if (isInRange)
            {
             //swing
             LaunchBallZoneObject();
            }
            else
            {
             //swing and miss 
            }
        }
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            shotDir = "l";
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            shotDir = "r";
        }
        else
        {
            shotDir = "n";
        }
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallZone"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BallZone"))
        {
            isInRange = false;
        }
    }
    
    
    private void LaunchBallZoneObject()
    {
        Debug.Log("hit");
        GameObject  ball = GameObject.Find("BallOff (1)");
        if (ball != null)
        {
            Debug.Log("hit2");
            Rigidbody ballZoneRigidbody = ball.GetComponent<Rigidbody>();
            if (ballZoneRigidbody != null)
            {
                Vector3 launchDirection = (ball.transform.position - transform.position).normalized;


                if (shotDir == "l")
                {
                    Debug.Log("left");
                    launchDirection.x = -.2f;
                }else if (shotDir == "r")
                {
                    Debug.Log("right");
                    launchDirection.x = .2f;
                }
                else
                {
                    launchDirection.x = 0f;
                }
                Debug.Log(launchDirection.x);
                Debug.Log(launchDirection);
                launchDirection.y = 0.5f; //upward force
                float launchForce = 16.0f; 
                ballZoneRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            }
        }
    }
    
    

    
}
