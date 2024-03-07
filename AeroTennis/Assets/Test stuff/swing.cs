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
    
    private Vector3 lastMousePosition;
    private bool isMouseSwinging;
    private float swingCooldown = 1f;
    private float lastSwingTime;

    public float mouseDistance;

    private void Start()
    {
        Cursor.visible = false;
        StartCoroutine(SwingPower());
    }

    private void FixedUpdate()
    {
        //Debug.Log("in range: " + isInRange);
        /*if (Input.GetMouseButtonDown(0))
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
        */

        if ((Time.time - lastSwingTime) < swingCooldown)
        {
            mouseDistance = 0;
        }
        
        if ((mouseDistance > 500) && isInRange)
        {

            // Check if the swing cooldown has expired
            if (Time.time - lastSwingTime >= swingCooldown)
            {
                Debug.Log(mouseDistance);
                if (mouseDistance > 500 & mouseDistance < 1000)
                {
                    Debug.Log("weak");
                    shotDir = .05f;
                }
                else if(mouseDistance > 1000 & mouseDistance < 1300)
                {
                    Debug.Log("Med");
                    shotDir = .2f;
                }
                else if(mouseDistance > 1300)
                {
                    Debug.Log("Strong");
                    shotDir = .4f;
                }


                // If it has, register the swing
                LaunchBallZoneObject();
                // Update the last swing time
                lastSwingTime = Time.time;
            }
        }
        



    }

    IEnumerator SwingPower()
    {
        while (true)
        {
            
            Vector3 currentMousePosition = Input.mousePosition;
            mouseDistance = Vector3.Distance(currentMousePosition, lastMousePosition);
            
            yield return new WaitForSeconds(.5f);
            mouseDistance = Vector3.Distance(currentMousePosition, lastMousePosition);
            lastMousePosition = currentMousePosition;
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
        GameObject  ball = GameObject.Find("BallHolder");
        //uncomment for online:
        //GameObject  ball = GameObject.Find("BallOff (1)");
        if (ball != null)
        {
            Rigidbody ballZoneRigidbody = ball.GetComponent<Rigidbody>();
            if (ballZoneRigidbody != null)
            {
                if (ball.transform.position.x > transform.position.x)
                {
                    //play hitleft anim
                    shotDir *= -1;
                }
                else
                {
                    //play hitright anim
                }
                ball.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + .5f);
                Vector3 launchDirection = (ball.transform.position - transform.position).normalized;

                
            

                launchDirection.x = shotDir;
                    
                
                launchDirection.y = upwardForce; //upward force
                
                ballZoneRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            }
        }
    }
    
    

    
}
