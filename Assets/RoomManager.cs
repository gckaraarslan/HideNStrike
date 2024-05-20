using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Photon.Pun;
using UnityEngine;
using Hashtable= ExitGames.Client.Photon.Hashtable;

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

    [HideInInspector]
    public int kills;
    [HideInInspector]
    public int deaths;

    public string roomNameToJoin = "test";

    
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
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
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

    public void SetHashes()
    {
        try
        {
            Hashtable hash = PhotonNetwork.LocalPlayer.CustomProperties;

            hash["kills"] = kills;
            hash["deaths"] = deaths;
        }
        catch (Exception e)
        {
        Debug.Log(e);
        }
    }
}
