using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;



public class swing : NetworkBehaviour
{
    public bool isInRange = false;
    public float shotDir = 0f;

    public float upwardForce = .5f;
    public float launchForce = 17f;

    public bool cooldownOver = true;
    
    public GameObject PlayerModel;
    
    Vector3 lastMousePos;
    float swipeThreshold = 300f; // Adjust this value as needed
    
 
    
    
    
    
    
    private void Start()
    {
        cooldownOver = true;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        /*
        print(cooldownOver);
        if ((Input.GetAxis("Mouse X") != 0) && cooldownOver)
        {
            if(Input.GetAxis("Mouse X") < 0)
            {
                PlayerModel.GetComponent<PlayerAnimScript>().playAnimation("SwingMovingRight");
                shotDir = -0.5f;
            }
            else if(Input.GetAxis("Mouse X") > 0)
            {
                PlayerModel.GetComponent<PlayerAnimScript>().playAnimation("SwingMovingleft");
                shotDir = 0.5f;
            }

            if (isInRange)
            {
                LaunchBall();
            }
            StartCoroutine(StartCooldown());
        }
        */
        
        // Track mouse input
        Vector3 currentMousePos = Input.mousePosition;

        // Calculate swipe distance
        float swipeDistance = (currentMousePos - lastMousePos).magnitude;

        // Check for swipe gesture
        if (swipeDistance > swipeThreshold)
        {
            if(Input.GetAxis("Mouse X") < 0)
            {
                PlayerModel.GetComponent<PlayerAnimScript>().playAnimation("SwingMovingRight");
                shotDir = -0.5f;
            }
            else if(Input.GetAxis("Mouse X") > 0)
            {
                PlayerModel.GetComponent<PlayerAnimScript>().playAnimation("SwingMovingleft");
                shotDir = 0.5f;
            }

            if (isInRange)
            {
                LaunchBall();
            }
            StartCoroutine(StartCooldown());
        }

        lastMousePos = currentMousePos;
        
        
        
        
        
        
        
    }
    
    //only really relavent to testing, as the ball usually wouldnt be in range that fast anyway.
    IEnumerator StartCooldown()
    {
        cooldownOver = false;
        yield return new WaitForSeconds(.1f);
        cooldownOver = true;
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


    

    
    
    private void LaunchBall()
    {
        GameObject ball = GameObject.Find("BallHolder");
        //uncomment for online:
        //GameObject  ball = GameObject.Find("BallOff (1)");
        if (ball != null)
        {
            Rigidbody ballZoneRigidbody = ball.GetComponent<Rigidbody>();
            if (ballZoneRigidbody != null)
            {
                ballZoneRigidbody.velocity = Vector3.zero;
                
                ball.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .5f);
                Vector3 launchDirection = (ball.transform.position - transform.position).normalized;
                
                
                launchDirection.x = shotDir;
                launchDirection.y = upwardForce;
                
                ballZoneRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            }
        }
    }
    
    

    
}
