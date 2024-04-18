using System;
using Mirror;
using UnityEngine;
using TMPro;

public class managmentScrip : NetworkBehaviour
{
    
    [SyncVar(hook = nameof(OnPlayer1ScoreChanged))]
    public int player1Score = 0;

    [SyncVar(hook = nameof(OnPlayer2ScoreChanged))]
    public int player2Score = 0;

    [SyncVar(hook = nameof(OnDueceStateChanged))]
    public bool DueceState = false;

    
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI dashtext;
    

    // Called when a player scores a point
    public void ScorePoint(PlayerID playerID)
    {
        if (!isServer)
            return;

        if (playerID == PlayerID.Player1)
        {
            if (player1Score < 30)
            {
                player1Score += 15;
            }
            else
            {
                if (player1Score == 50 && player2Score == 50)
                {
                    player1Score = 40;
                    player2Score = 40;
                }
                else
                {
                    player1Score += 10;
                }
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

        if (player1Score == 50 && player2Score == 50)
        {
            player1Score = 40;
            player2Score = 40;
        }
        if (player1Score == 40 && player2Score == 40)
        {
            DueceState = true;
        }
        else
        {
            DueceState = false;
        }
        
    }

    // Update player 1 score UI
    private void OnPlayer1ScoreChanged(int oldValue, int newValue)
    {
        player1ScoreText.text = newValue.ToString();
        
    }

    // Update player 2 score UI
    private void OnPlayer2ScoreChanged(int oldValue, int newValue)
    {
        player2ScoreText.text = newValue.ToString();
        
       
    }
    
    
    private void OnDueceStateChanged(bool oldValue, bool newValue)
    {
       
        
    }
  
    private void Update()
    {
        
    }
}

public enum PlayerID
{
    Player1,
    Player2
}






