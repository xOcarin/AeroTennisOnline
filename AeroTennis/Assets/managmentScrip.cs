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

    // Called when a player scores a point
    public void ScorePoint(PlayerID playerID)
    {
        if (!isServer)
            return;

        if (playerID == PlayerID.Player1)
        {
            player1Score++;
        }
        else if (playerID == PlayerID.Player2)
        {
            player2Score++;
        }
    }

    // Update player 1 score UI
    private void OnPlayer1ScoreChanged(int oldValue, int newValue)
    {
        print(player1ScoreText);
        print(player2ScoreText);
        player1ScoreText.text = newValue.ToString();
    }

    // Update player 2 score UI
    private void OnPlayer2ScoreChanged(int oldValue, int newValue)
    {
        print(player1ScoreText);
        print(player2ScoreText);
        player2ScoreText.text = newValue.ToString();
    }
}

public enum PlayerID
{
    Player1,
    Player2
}