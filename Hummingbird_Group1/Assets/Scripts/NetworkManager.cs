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
    public Case patientCase1;
    public Case patientCase2;

    public int difficulty;

    public Player localPlayer;

    [SerializeField] private MainMenu mainMenu;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // connect to master server
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    [PunRPC]
    public void SetDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
        mainMenu.UpdateRoomUI();
    }

    public void SetPlayer(Player player)
    {
        localPlayer = player;
        if (player == null)
        {
            SetNickName();
        }
        else
        {
            SetNickName(player.nickname);
        }
    }

    public void SetNickName(string nickname = "")
    {
        if (nickname == "")
        {
            nickname = "Player " + Random.Range(1000, 10000);
        }

        PhotonNetwork.NickName = nickname;
    }

    [PunRPC]
    public void UpdateCases(Case case1, Case case2)
    {
        patientCase1 = case1;
        patientCase2 = case2;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server.");
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

    public void JoinRoom(string roomCode)
    {
        this.roomCode = roomCode;
        PhotonNetwork.JoinRoom(roomCode);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        mainMenu.photonView.RPC("UpdateRoomUI", RpcTarget.All);
    }

    public override void OnJoinedRoom()
    {
        mainMenu.photonView.RPC("UpdateRoomUI", RpcTarget.All);
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
        PhotonNetwork.Reconnect();
    }

    public void LoadLevel(int scene)
    {
        PhotonNetwork.LoadLevel(scene);
    }
}
