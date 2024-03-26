using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    public Vector3 targetPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallZone"))
        {
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            otherRigidbody.velocity = Vector3.zero;
            otherRigidbody.angularVelocity = Vector3.zero;
            // Teleport the collided object to the target position
            other.transform.position = targetPosition;
        }
    }
}
