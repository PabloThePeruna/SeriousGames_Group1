using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private int emptyRoomTtl = 60000; // in milliseconds, 1 min
    private int maxPlayersPerRoom = 2;
    public string roomCode;

    private string nicknameKey = "Nickname";

    [SerializeField] private PhotonView canvasPhotonView;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // connect to master server
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void SetNickName(string nickname)
    {
        if (nickname == "")
        {
            if (PlayerPrefs.HasKey(nicknameKey))
            {
                nickname = PlayerPrefs.GetString(nicknameKey);
            }
            else
            {
                nickname = "Player " + Random.Range(1000, 10000);
            }
        }

        PlayerPrefs.SetString(nicknameKey, nickname);
        PhotonNetwork.NickName = nickname;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server.");
    }

    public void SetRoomCode(string roomCode)
    {
        this.roomCode = roomCode;
    }

    // creates a new room and this player automatically joins the created room
    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.EmptyRoomTtl = emptyRoomTtl;
        options.MaxPlayers = (byte)maxPlayersPerRoom;

        roomCode = Random.Range(1000, 10000).ToString();

        PhotonNetwork.CreateRoom(roomCode, options);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomCode);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    public override void OnLeftRoom()
    {
        canvasPhotonView.RPC("UpdateRoomUI", RpcTarget.All);
    }

    [PunRPC]
    public override void OnJoinedRoom()
    {
        canvasPhotonView.RPC("UpdateRoomUI", RpcTarget.All);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        if (returnCode == 32758 || returnCode == 32764)
        {
            Debug.LogWarning("This room does not exist.");
        }
        else if (returnCode == 32765)
        {
            Debug.LogWarning("The room is full.");
        }
        else if (returnCode > 32745 && returnCode < 32751)
        {
            Debug.LogWarning("User is already in the room.");
        }
        else
        {
            Debug.LogWarning("Unknown error. Code " + returnCode);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("Disconnected from PUN2 servers.");
        PhotonNetwork.LoadLevel(0);
    }

    public void LoadLevel(int scene)
    {
        PhotonNetwork.LoadLevel(scene);
    }
}
