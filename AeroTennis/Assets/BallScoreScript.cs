using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallScoreScript : MonoBehaviour
{
    private managmentScrip gameManager;
    private AudioManager2 audioManager;


    private bool cooldown = false;
    private void Start()
    {
        // Get a reference to the TennisGameManager component
        gameManager = FindObjectOfType<managmentScrip>();
        audioManager = FindObjectOfType<AudioManager2>();
    }

    // This method is called when the ball collides with a goal collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreArea2") && !cooldown)
        {
            StartCoroutine(setcooldown());
            gameManager.ScorePoint(PlayerID.Player2);
            audioManager.PlaySound("splash", .5f);
        }
        else if (other.CompareTag("ScoreArea1") && !cooldown)
        {
            StartCoroutine(setcooldown());
            gameManager.ScorePoint(PlayerID.Player1);
            audioManager.PlaySound("splash", .5f);
        }else if (other.CompareTag("OUT") && !cooldown)
        {
            if (transform.position.z > 0)
            {
                StartCoroutine(setcooldown());
                gameManager.ScorePoint(PlayerID.Player1);
                audioManager.PlaySound("splash", .5f);
            }
            else
            {
                StartCoroutine(setcooldown());
                gameManager.ScorePoint(PlayerID.Player2);
                audioManager.PlaySound("splash", .5f);
            }
        }
    }



    IEnumerator setcooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(2f);
        cooldown = false;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
                audioManager.PlaySound("hitsand", .5f);
        }
    }
    
 
}