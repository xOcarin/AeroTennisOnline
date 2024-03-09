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
    
    private void Start()
    {
        cooldownOver = true;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        //print((Input.GetAxis("Mouse X")));
        if ((cooldownOver && ((Input.GetAxis("Mouse X") != 0))) && isInRange )
        {
            print(Input.GetAxis("Mouse X"));
            // Determine shotDir based on mouse movement
            if(Input.GetAxis("Mouse X") < 0)
            {
                print("Mouse moved left");
                shotDir = -0.5f;
            }
            else if(Input.GetAxis("Mouse X") > 0)
            {
                print("Mouse moved right");
                shotDir = 0.5f;
            }

            // Launch ball and start cooldown
            LaunchBallZoneObject();
            StartCoroutine(StartCooldown());
        }
    }
    
    //only really relavent to testing, as the ball usually wouldnt be in range that fast anyway.
    IEnumerator StartCooldown()
    {
        cooldownOver = false;
        yield return new WaitForSeconds(1f);
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


    

    
    
    private void LaunchBallZoneObject()
    {
        GameObject ball = GameObject.Find("BallHolder");
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
                    PlayerModel.GetComponent<PlayerAnimScript>().playAnimation("SwingMovingRight");
                }
                else
                {
                    //play hitright anim
                    PlayerModel.GetComponent<PlayerAnimScript>().playAnimation("SwingMovingRight");
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
