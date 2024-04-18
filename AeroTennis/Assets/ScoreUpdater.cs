using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Mirror;
using Mirror.Examples.Pong;
using Telepathy;

public class ScoreUpdater : NetworkBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Ball;
    private MonoBehaviour P1Movement;
    private MonoBehaviour P2Movement;
    private Animator P1animator; 
    private Animator P2animator;
    private Animator P1CANVASanimator; 
    private Animator P2CANVASanimator;
    private string ballMode;
    
    private Rigidbody ballZoneRigidbody;
    private Rigidbody Player1Body;
    private Rigidbody Player2Body;
    
    public bool moveCooldown = false;
    
    Vector3 lastMousePos;
    float swipeThreshold = 150f;

    private Vector3 P2CurrPos;
    private Vector3 P1CurrPos;

    public bool cooldown;
    
    private bool PlayersAssigned = false;


    public int LastPlayerToHit;
    
    private AudioManager2 audioManager;
    

    private void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("BallZone");
        ballZoneRigidbody = Ball.GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager2>();
    }

    private void Update()
    {
        
        if (PlayersAssigned == false)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length > 1)
            {
                Player1 = players[0];
                Player2 = players[1];
                P1Movement = Player1.GetComponent<PlayerMove>();
                P2Movement = Player2.GetComponent<PlayerMove>();
                P1animator = Player1.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
                P2animator = Player2.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
                P1CANVASanimator = Player2.transform.GetChild(2).GetComponent<Animator>();
                P2CANVASanimator = Player2.transform.GetChild(2).GetComponent<Animator>();
                Player1Body = Player1.GetComponent<Rigidbody>();
                Player2Body = Player2.GetComponent<Rigidbody>();
                PlayersAssigned = true;
                
                ballZoneRigidbody.velocity = Vector3.zero;
                ballZoneRigidbody.angularVelocity = Vector3.zero;
                Ball.transform.position = new Vector3(0, 1, 10);

            }
        }

        if (cooldown)
        {
            Player1.transform.position = P1CurrPos;
            Player2.transform.position = P2CurrPos;
        }


        if (Ball.transform.position.z > 8)
        {
            LastPlayerToHit = 1;
        }else if (Ball.transform.position.z < -8)
        {
            LastPlayerToHit = 2;
        }
        
}

 
    
    



    private IEnumerator FreezePlayers(float duration, int scorer)
    {
        Player1Body.velocity = Vector3.zero;
        Player2Body.velocity = Vector3.zero;

        P1CurrPos = Player1.transform.position;
        P2CurrPos = Player2.transform.position;

        if(scorer == 3 && LastPlayerToHit == 1)
        {
            scorer = 2;
        }else if (scorer == 3 && LastPlayerToHit == 2)
        {
            scorer = 1;
        }
        
        
        if (scorer == 1)
        {
            P1animator.Play("Victroy");
            P2animator.Play("Loss");
            P1CANVASanimator.Play("score");
            P2CANVASanimator.Play("canvasidle");
        }
        else if(scorer == 2)
        {
            P2animator.Play("Victroy");
            P1animator.Play("Loss");
            P2CANVASanimator.Play("score");
            P1CANVASanimator.Play("canvasidle");
        }

        cooldown = true;
        yield return new WaitForSeconds(duration);
        cooldown = false;
        
        if (scorer == 1)
        {
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.angularVelocity = Vector3.zero;
            Ball.transform.position = new Vector3(0, 1, -10);
            audioManager.PlaySound("respawn", .5f);
        }
        else if (scorer == 2)
        {
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.angularVelocity = Vector3.zero;
            Ball.transform.position = new Vector3(0, 1, 10);
            audioManager.PlaySound("respawn", .5f);
        }
  
    }
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallZone") && gameObject.CompareTag("ScoreArea1"))
        {
            //print("P1 SCORED");
            StartCoroutine(FreezePlayers(3f, 1));
        }else if (other.CompareTag("BallZone") && gameObject.CompareTag("ScoreArea2"))
        {
            //print("P2 SCORED");
            StartCoroutine(FreezePlayers(3f, 2));
        }else if (other.CompareTag("BallZone") && gameObject.CompareTag("out"))
        {
            //print("OUT");
            StartCoroutine(FreezePlayers(3f, 3));
        }
        
    }
    
    
    
}