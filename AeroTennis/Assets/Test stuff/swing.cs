using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class swing : MonoBehaviour
{
    public bool isInRange = false;
    public GameObject ball;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isInRange)
            {
             //swing
             Debug.Log("hit");
             LaunchBallZoneObject();
            }
            else
            {
             //swing and miss   
             Debug.Log("miss");
            }
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
        // Example: Launch the BallZone object in the opposite direction
        if (ball != null)
        {
            Rigidbody ballZoneRigidbody = ball.GetComponent<Rigidbody>();
            if (ballZoneRigidbody != null)
            {
                Vector3 launchDirection = (ball.transform.position - transform.position).normalized;
                launchDirection.y = 0.5f; // Add some upward force
                float launchForce = 15.0f; // Increase the launch force
                ballZoneRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            }
        }
    }
    
    

    
}
