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
    private string ballMode;
    
    private Rigidbody ballZoneRigidbody;
    private Rigidbody Player1Body;
    private Rigidbody Player2Body;

    public bool moveCooldown = false;
    
    Vector3 lastMousePos;
    float swipeThreshold = 150f;

    private Vector3 P2CurrPos;
    private Vector3 P1CurrPos;


    private bool PlayersAssigned = false;

    private void Start()
    {
        Ball = GameObject.FindGameObjectWithTag("BallZone");
        ballZoneRigidbody = Ball.GetComponent<Rigidbody>();
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
                Player1Body = Player1.GetComponent<Rigidbody>();
                Player2Body = Player2.GetComponent<Rigidbody>();
                PlayersAssigned = true;
            }
        }
        
    }

 
    
    



    private IEnumerator FreezePlayers(float duration, int scorer)
    {
        Player1Body.velocity = Vector3.zero;
        Player2Body.velocity = Vector3.zero;

        P1CurrPos = Player1.transform.position;
        P2CurrPos = Player2.transform.position;
        if (scorer == 1)
        {
            P1animator.Play("playerMoveLeft");
            P2animator.Play("playerMoveLeft");
          
        }
        else
        {
            P1animator.Play("SwingMovingleft");
            P2animator.Play("SwingMovingleft");
        
          
        }
        
        yield return new WaitForSeconds(duration);

        if (scorer == 1)
        {
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.angularVelocity = Vector3.zero;
            Ball.transform.position = new Vector3(0, 1, -10);
            
        }
        else
        {
            ballZoneRigidbody.velocity = Vector3.zero;
            ballZoneRigidbody.angularVelocity = Vector3.zero;
            Ball.transform.position = new Vector3(0, 1, 10);
        
          
        }
  
    }
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BallZone") && gameObject.CompareTag("ScoreArea1"))
        {
            //print("P1 SCORED");
            StartCoroutine(FreezePlayers(2f, 1));
        }else if (other.CompareTag("BallZone") && gameObject.CompareTag("ScoreArea2"))
        {
            //print("P2 SCORED");
            StartCoroutine(FreezePlayers(2f, 2));
        }
    }
    
    
    
}