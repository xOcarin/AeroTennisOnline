using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallZone"))
        {
            StartCoroutine(resetBall(other));
        }
    }


    IEnumerator resetBall(Collider other)
    {
        yield return new WaitForSeconds(.2f);
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        otherRigidbody.velocity = Vector3.zero;
        otherRigidbody.angularVelocity = Vector3.zero;
        other.transform.position = new Vector3(0,1,10);
    }
}
