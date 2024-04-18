using System;
using UnityEngine;
using TMPro;

public class PlayerPrefabInitializer : MonoBehaviour
{
    public managmentScrip gameManager;

    // Reference to the Text Mesh Pro Text elements for both player scores
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI dashtext;

    private void Start()
    {
        gameManager = FindObjectOfType<managmentScrip>();
        // Ensure the game manager is available
        if (gameManager == null)
        {
            Debug.LogError("TennisGameManager reference is missing!");
            return;
        }
        
        gameManager.player1ScoreText = player1ScoreText;
        gameManager.player2ScoreText = player2ScoreText;
        gameManager.dashtext = dashtext;


        
    }

    private void Update()
    {
        if (gameManager.player1Score == 40 && gameManager.player2Score == 40)
        {
            dashtext.text  = "Duece!";
            player1ScoreText.text = "";
            player2ScoreText.text = "";
        } else if (gameManager.player1Score == 50 && gameManager.player2Score == 40)
        {
            player1ScoreText.text = "A";
            dashtext.text  = "-";
            player2ScoreText.text = gameManager.player2Score.ToString();
        } else if (gameManager.player2Score == 50 && gameManager.player1Score == 40)
        {
            player2ScoreText.text = "A";
            dashtext.text  = "-";
            player1ScoreText.text = gameManager.player1Score.ToString();
        } else
        {
            player1ScoreText.text = gameManager.player1Score.ToString();
            player2ScoreText.text = gameManager.player2Score.ToString();
            dashtext.text  = "-";
        }

        if (gameManager.player1Score == 60)
        {
            dashtext.text  = "Player 1 wins!!!!!";
            player1ScoreText.text = "";
            player2ScoreText.text = "";
        }
        
        if (gameManager.player2Score == 60)
        {
            dashtext.text  = "Player 2 wins!!!!!";
            player1ScoreText.text = "";
            player2ScoreText.text = "";
        }
        
        
        
        
    }
}