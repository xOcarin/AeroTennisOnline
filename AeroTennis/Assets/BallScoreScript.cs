using UnityEngine;

public class BallScoreScript : MonoBehaviour
{
    private managmentScrip gameManager;
    private AudioManager2 audioManager;
    private void Start()
    {
        // Get a reference to the TennisGameManager component
        gameManager = FindObjectOfType<managmentScrip>();
        audioManager = FindObjectOfType<AudioManager2>();
    }

    // This method is called when the ball collides with a goal collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScoreArea2"))
        {
            // Score a point for player 2
            gameManager.ScorePoint(PlayerID.Player2);
            transform.position = new Vector3(0, -10, 0);
        }
        else if (other.CompareTag("ScoreArea1"))
        {
            // Score a point for player 1
            gameManager.ScorePoint(PlayerID.Player1);
            transform.position = new Vector3(0, -10, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Vector3 location = GetLoc();
            if (location.z > 14 || location.z < -14)
            {
                audioManager.PlaySound("bumb");
            }
            else
            {
                audioManager.PlaySound("bounce");
            }
            
        }
    }


    public Vector3 GetLoc()
    {
        return this.transform.position;
    }
 
}