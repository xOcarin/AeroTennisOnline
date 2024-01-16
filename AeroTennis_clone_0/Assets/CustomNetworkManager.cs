using UnityEngine;
using Mirror;

public class CustomNetworkManager : NetworkManager
{
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    public void OnServerAddPlayer(NetworkConnectionToClient conn, AddPlayerMessage extraMessage)
    {
        Transform spawnPoint = conn.connectionId == 0 ? player1SpawnPoint : player2SpawnPoint;

        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}