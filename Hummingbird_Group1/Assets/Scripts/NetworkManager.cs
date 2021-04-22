using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private bool isInitialized = false;
    private int emptyRoomTtl = 60000; // in milliseconds, 1 min
    private int maxPlayersPerRoom = 2;
    public string roomCode;
    public Case patientCase1;
    public Case patientCase2;

    public int difficulty = 0;

    public Player localPlayer;

    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private CasePicker casePicker;
    [SerializeField] private UIManager caseUIManager;

    private void Awake()
    {
        if (!isInitialized)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // connect to master server
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void LogOut()
    {
        PhotonNetwork.Disconnect();
        Destroy(gameObject);
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

    public void ChooseCases()
    {
        if (PhotonNetwork.IsMasterClient && casePicker != null)
        {
            string patientName1 = "";
            string patientName2 = "";
            if (difficulty == 0)
            {
                patientName1 = "James";
                patientName2 = "Creed";
            }
            else if (difficulty == 1)
            {
                int choice = Random.Range(0, 100) % 2;
                patientName1 = "Jeanette";
                if (choice == 0)
                {
                    patientName2 = "Creed";
                }
                else //if (choice == 1)
                {
                    patientName2 = "Carla";
                }
            }
            else if (difficulty == 2)
            {
                patientName1 = "Jeanette";
                patientName2 = "Carla";
            }

            photonView.RPC("UpdateCases", RpcTarget.All, patientName1, patientName2);
        }
    }

    [PunRPC]
    public void UpdateCases(string name1, string name2)
    {
        Database.RetrieveCaseFromDatabase(name1, SetCase1);
        Database.RetrieveCaseFromDatabase(name2, SetCase2);
    }

    public void SetCase1(Case response)
    {
        patientCase1 = response;
    }

    public void SetCase2(Case response)
    {
        patientCase2 = response;
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
        if (cause != DisconnectCause.DisconnectByClientLogic)
        {
            PhotonNetwork.Reconnect();
        }
    }

    public void LoadLevel(int scene)
    {
        PhotonNetwork.LoadLevel(scene);
    }

    private void OnLevelWasLoaded(int level)
    {
        GameObject go = GameObject.Find("SceneManager");
        if (go != null)
        {
            go.TryGetComponent<UIManager>(out caseUIManager);
        }

        go = GameObject.Find("Canvas");
        if (go != null)
        {
            go.TryGetComponent<MainMenu>(out mainMenu);
        }
    }
}
