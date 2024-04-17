using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    
    public float speed = 5f; 
    public Vector3 direction = Vector3.forward;
    private AudioManager2 audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager2>();
    }
    
}
