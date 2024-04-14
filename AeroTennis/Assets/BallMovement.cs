using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    
    public float speed = 5f; 
    public Vector3 direction = Vector3.forward;
    
  
    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(direction * speed * Time.deltaTime);
    }

}
