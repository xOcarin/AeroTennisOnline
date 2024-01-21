using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : NetworkBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f; 
    public Vector3 direction = Vector3.forward;
    
  
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        
        
        if(other.CompareTag("Racket"))
        {
            direction = new Vector3(Random.Range(-0.5f, 0.5f), 0, 1);
            speed = speed * -1;
        }
        
        if(other.CompareTag("ScoreArea1"))
        {
            StartCoroutine(RespawnBall());
        }
        
        if(other.CompareTag("ScoreArea2"))
        { 
            StartCoroutine(RespawnBall());
        }
    }

    IEnumerator RespawnBall()
    {
        yield return new WaitForSeconds(2f);
        direction = new Vector3(Random.Range(-0.5f, 0.5f), 0, 1);
        speed = speed * -1;
        transform.position = new Vector3(0, .25f, 0);
    }
    
    
}
