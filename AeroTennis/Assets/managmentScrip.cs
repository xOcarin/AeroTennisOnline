using Mirror;
using UnityEngine;
using TMPro;

public class managmentScrip : NetworkBehaviour
{
    
    [SyncVar(hook = nameof(OnPlayer1ScoreChanged))]
    public int player1Score = 0;

    [SyncVar(hook = nameof(OnPlayer2ScoreChanged))]
    public int player2Score = 0;

    
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
   // public TextMeshProUGUI dashtext;
    

    // Called when a player scores a point
    public void ScorePoint(PlayerID playerID)
    {
        if (!isServer)
            return;

        if (playerID == PlayerID.Player1)
        {
            if (player2Score < 30)
            {
                player1Score += 15;
            }
            else
            {
                player1Score += 10;
            }
        }
        else if (playerID == PlayerID.Player2)
        {
            if (player2Score < 30)
            {
                player2Score += 15; 
            }
            else
            {
                player2Score += 10;
            }
        }
    }

    // Update player 1 score UI
    private void OnPlayer1ScoreChanged(int oldValue, int newValue)
    {
        player1ScoreText.text = newValue.ToString();
        /*
        if (player1Score == 40 && player2Score == 40)
        {
            dashtext.text = "Duece!";
            player1ScoreText.text = "";
            player2ScoreText.text = "";
        }
        */
    }

    // Update player 2 score UI
    private void OnPlayer2ScoreChanged(int oldValue, int newValue)
    {
        player2ScoreText.text = newValue.ToString();
        /*
        if (player1Score == 40 && player2Score == 40)
        {
            dashtext.text = "Duece!";
            player1ScoreText.text = "";
            player2ScoreText.text = "";
        } */
    }
}

public enum PlayerID
{
    Player1,
    Player2
}