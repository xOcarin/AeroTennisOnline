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
    float swipeThreshold =150f; // Adjust this value as needed
    
 
    
    
    
    
    
    private void Start()
    {
        cooldownOver = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void FixedUpdate()
    {

        // Track mouse input
        Vector3 currentMousePos = Input.mousePosition;

        // Calculate swipe distance
        float swipeDistance = (currentMousePos - lastMousePos).magnitude;

        // Check for swipe gesture
        if ((swipeDistance > swipeThreshold) && cooldownOver)
        {
            if(Input.GetAxis("Mouse X") < 0)
            {
                //left
                PlayerModel.GetComponent<PlayerAnimScript>().playAnimation("SwingMovingRight");
                if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
                {
                    shotDir = 0f;
                }
                else if(Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                {
                    shotDir = -0.5f;
                }
                else if(Input.GetMouseButton(1) && !Input.GetMouseButton(0))
                {
                    shotDir = -0.15f;
                }
                else
                { 
                    shotDir = -0.33f;
                }
            }
            else if(Input.GetAxis("Mouse X") > 0)
            {
                //right
                PlayerModel.GetComponent<PlayerAnimScript>().playAnimation("SwingMovingleft");
                if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
                {
                    shotDir = 0f;
                }
                else if(Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                {
                    shotDir = 0.5f;
                }
                else if(Input.GetMouseButton(1) && !Input.GetMouseButton(0))
                {
                    shotDir = 0.15f;
                }
                else
                { 
                    shotDir = 0.33f;
                }
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
        yield return new WaitForSeconds(.5f);
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
