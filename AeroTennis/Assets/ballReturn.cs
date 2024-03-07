using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballReturn : MonoBehaviour
{
    public Vector3 targetLocation; // Specify the target location in the Unity Editor
    public Rigidbody ballZoneRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreArea1"))
        {
            // Move the object to the target location
            transform.position = targetLocation;
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.velocity = Vector3.zero;
        }
    }
}
