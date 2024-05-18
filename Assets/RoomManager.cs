using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;
    public GameObject player;
    [Space] 
    public Transform[] spawnPoints;

    [Space] 
    public GameObject roomCam;

    public GameObject nameUI;
    public GameObject connectingUI;
    private string nickName = "Unnamed";

    
    private void Awake()
    {
        instance = this;
    }

    public void ChangeNickName(string _name)
    {
        nickName = _name;
    }
    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }
    void Start()
    {
        // Debug.Log("Connecting...");
        // PhotonNetwork.ConnectUsingSettings();    // join room button eventine taşındı...
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
        Debug.Log("we are in the lobby");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("we were connected to a room");
        roomCam.SetActive(false);
       
        SpawnPlayer();
    } 

    public void SpawnPlayer()
    {
        Transform spawnPoint = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
        
        _player.GetComponent<PhotonView>().RPC("SetNickname",RpcTarget.AllBuffered,nickName);

        PhotonNetwork.LocalPlayer.NickName = nickName;

    }
}
