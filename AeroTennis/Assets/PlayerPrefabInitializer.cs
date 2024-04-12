using System;
using UnityEngine;
using TMPro;

public class PlayerPrefabInitializer : MonoBehaviour
{
    public managmentScrip gameManager;

    // Reference to the Text Mesh Pro Text elements for both player scores
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;

    private void Start()
    {
        gameManager = FindObjectOfType<managmentScrip>();
        // Ensure the game manager is available
        if (gameManager == null)
        {
            Debug.LogError("TennisGameManager reference is missing!");
            return;
        }

        // Assign the Text Mesh Pro Text references
        gameManager.player1ScoreText = player1ScoreText;
        gameManager.player2ScoreText = player2ScoreText;
    }

    private void Update()
    {
        player1ScoreText.text = gameManager.player1Score.ToString();
        player2ScoreText.text = gameManager.player2Score.ToString();
    }
}