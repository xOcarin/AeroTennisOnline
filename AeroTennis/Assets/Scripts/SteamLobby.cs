using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;

public class SteamLobby : NetworkBehaviour
{
    //Callbacks
    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEntered;
    
    //vars
    public ulong CurrentLobbyID;
    private const string HostAddressKey = "HostAddress";
    private networkManager manager;
    
    //game objs
    public GameObject HostButton;
    public Text LobbyNameText;
    
    public GameObject StartButton;
    
    //ball stuff
    public GameObject ballP; 
    public Vector3 spawnPosition = new Vector3(0f, .25f, 0f); 

    private void Start()
    {
        if(!SteamManager.Initialized){return;}
        
        manager = GetComponent<networkManager>();

        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(onJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, manager.maxConnections);
        HostButton.SetActive(false);
    }

    [Command]
    public void SpawnBall()
    {
        GameObject ball = Instantiate(ballP);
        NetworkServer.Spawn(ball);
        StartButton.SetActive(false);
    }


    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK) { return; }
        
        Debug.Log("lobby created succesfully");
        
        manager.StartHost();

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());

        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name",
            SteamFriends.GetPersonaName().ToString() + "'s Lobby");
    }


    private void onJoinRequest(GameLobbyJoinRequested_t callback)
    {
        Debug.Log("Request to join lobby");
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        //everyone
        HostButton.SetActive((false));
        CurrentLobbyID = callback.m_ulSteamIDLobby;
        LobbyNameText.gameObject.SetActive(true);
        LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name");
        
        //clients
        if(NetworkServer.active) { return;}
        
        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        
        manager.StartClient();
    }
    
    
    
    

}
