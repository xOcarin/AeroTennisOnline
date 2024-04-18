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
    public TextMeshProUGUI P1Score; 
    public TextMeshProUGUI P2Score; 
    public TextMeshProUGUI DASHTEXT; 
    public Animator P1CANVASanimator; 
    public Animator P2CANVASanimator;
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

    public GameObject player1spawn;
    

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
                P1CANVASanimator = Player1.transform.GetChild(2).GetComponent<Animator>();
                P2CANVASanimator = Player2.transform.GetChild(2).GetComponent<Animator>();
                Player1Body = Player1.GetComponent<Rigidbody>();
                Player2Body = Player2.GetComponent<Rigidbody>();
                PlayersAssigned = true;
                
                ballZoneRigidbody.velocity = Vector3.zero;
                ballZoneRigidbody.angularVelocity = Vector3.zero;
                Ball.transform.position = new Vector3(0, 1, 10);
                Destroy(player1spawn);

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
        //Ball.transform.position = new Vector3(0, -10, -10);
        
        
        
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

            if (P1animator.gameObject.activeSelf)
            {
                P1animator.Play("Victroy");
            }

            if (P2animator.gameObject.activeSelf)
            {
                P2animator.Play("Loss");
            }
            
            if (P1CANVASanimator.gameObject.activeSelf)
            {
                P1CANVASanimator.Play("score");
            }
            if (P2CANVASanimator.gameObject.activeSelf)
            {
                P2CANVASanimator.Play("oscore");
            }
           
        }
        else if(scorer == 2)
        {


            if (P1animator.gameObject.activeSelf)
            {
                P1animator.Play("Loss");
            }


            if (P2animator.gameObject.activeSelf)
            {
                P2animator.Play("Victroy");
            }

            
            if (P1CANVASanimator.gameObject.activeSelf)
            {
                P1CANVASanimator.Play("oscore");
            }

            if (P2CANVASanimator.gameObject.activeSelf)
            {
                P2CANVASanimator.Play("score");
            }
        }
        
        
        
        cooldown = true;
        yield return new WaitForSeconds(duration);
        cooldown = false;
        
        P1Score = Player1.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        P2Score = Player1.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        DASHTEXT = Player1.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>();
        
        print(P1Score.text);
        print(P2Score.text);


        if (DASHTEXT.text == "Player 1 wins!!!!!")
        {

            if (P1CANVASanimator.gameObject.activeSelf)
            {
                P1CANVASanimator.Play("WIN");
            }

            if (P2CANVASanimator.gameObject.activeSelf)
            {
                P2CANVASanimator.Play("LOSE");
            }
        }else if (DASHTEXT.text == "Player 2 wins!!!!!")
        {
            if (P1CANVASanimator.gameObject.activeSelf)
            {
                P2CANVASanimator.Play("WIN");
            }
            if (P2CANVASanimator.gameObject.activeSelf)
            {
                P1CANVASanimator.Play("LOSE");
            }
        }else
        {
            if (P1CANVASanimator.gameObject.activeSelf)
            {
                P1CANVASanimator.Play("canvasidle");
            }

            if (P2CANVASanimator.gameObject.activeSelf)
            {
                P2CANVASanimator.Play("canvasidle");
            }
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
        }else if (other.CompareTag("BallZone") && gameObject.CompareTag("OUT"))
        {
            //print("OUT");
            StartCoroutine(FreezePlayers(3f, 3));
        }
        
    }
    
    
    
}