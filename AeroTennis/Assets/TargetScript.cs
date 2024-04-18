using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    private Vector3 initPos;
    private AudioManager2 audioManager;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        audioManager = FindObjectOfType<AudioManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            transform.position = new Vector3(5, -10, 0);
            audioManager.PlaySound("hittarget", .75f);
            StartCoroutine(respawnTarget());
        }
    }

    private IEnumerator respawnTarget()
    {
        yield return new WaitForSeconds(5f);
        audioManager.PlaySound("respawn", .75f);
        transform.position = initPos;
    }







}
