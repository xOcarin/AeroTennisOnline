using UnityEngine;

public class BallScoreScript : MonoBehaviour
{
    private managmentScrip gameManager;

    private void Start()
    {
        // Get a reference to the TennisGameManager component
        gameManager = FindObjectOfType<managmentScrip>();
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
}