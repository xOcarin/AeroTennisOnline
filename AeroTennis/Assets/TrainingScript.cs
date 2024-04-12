using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingScript : MonoBehaviour
{
    public float pushForce = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallZone"))
        {
            
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            otherRigidbody.velocity = Vector3.zero;
            otherRigidbody.angularVelocity = Vector3.zero;
            other.transform.position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z - .5f);
            Vector3 launchDirection = (other.transform.position - transform.position).normalized;
            
            launchDirection.x = 0f;
            launchDirection.y = .5f; //upward force
            
            
            otherRigidbody.AddForce(launchDirection * 17, ForceMode.Impulse);
        }
    }
}
