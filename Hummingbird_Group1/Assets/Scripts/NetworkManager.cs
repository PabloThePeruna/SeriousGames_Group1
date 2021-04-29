using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

/*
 * This class should handle all multiplayer calls
 * and holds local player and case data
 */
public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    private bool isInitialized = false;
    private int emptyRoomTtl = 60000; // in milliseconds, 1 min
    private int maxPlayersPerRoom = 2;
    public string roomCode;
    public Case patientCase1;
    public Case patientCase2;
    public bool case1Set = false; // have we received case 1 from the database
    public bool case2Set = false; // have we received case 2 from the database

    public int difficulty = 0; // 0 = easy, 1 = normal, 2 = hard

    public static Player localPlayer; // player from the database

    public static bool isLoggedIn = false;

    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private CasePicker casePicker;
    [SerializeField] private UIManager caseUIManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if (!PhotonNetwork.IsConnected && !PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.ConnectUsingSettings(); // connect to master server
            PhotonNetwork.AutomaticallySyncScene = false;
        }
    }

    /*
     * Called when the player is logging out.
     * Disconnects from PUN2 server and destroys this
     * (so it can be reinitialized).
     */
    public void LogOut()
    {
        isLoggedIn = false;
        PhotonNetwork.Disconnect();
        Destroy(gameObject);
    }

    /*
     * Set the difficulty for every player in the room.
     * Update the room UI to show this new difficulty level.
     */
    [PunRPC]
    public void SetDifficulty(int difficulty)
    {
        this.difficulty = difficulty;
        mainMenu.UpdateRoomUI();
    }

    /*
     * Set the localPlayer field and update the player's PUN nickname
     */
    public void SetPlayer(Player player)
    {
        isLoggedIn = true;
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

    /*
     * Set the player's PUN nickname (which is 
     * displayed in the room's player list to everyone)
     */
    public void SetNickName(string nickname = "")
    {
        if (nickname == "")
        {
            nickname = "Player " + Random.Range(1000, 10000);
        }

        PhotonNetwork.NickName = nickname;
    }

    /*
     * Based on the difficulty level, decide on the patient 
     * cases that will be given to the players.
     * Then tell everyone to retrieve these cases by giving
     * the patient names
     */
    public void ChooseCases()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            string patientName1 = "";
            string patientName2 = "";
            patientName1 = "Larry";
            patientName2 = "Larry";
            //if (difficulty == 0)
            //{
            //    patientName1 = "James";
            //    patientName2 = "Creed";
            //}
            //else if (difficulty == 1)
            //{
            //    int choice = Random.Range(0, 100) % 2;
            //    patientName1 = "Jeanette";
            //    if (choice == 0)
            //    {
            //        patientName2 = "Creed";
            //    }
            //    else //if (choice == 1)
            //    {
            //        patientName2 = "Carla";
            //    }
            //}
            //else if (difficulty == 2)
            //{
            //    patientName1 = "Creed";
            //    patientName2 = "Carla";
            //}
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                photonView.RPC("UpdateCases", RpcTarget.All, patientName1, patientName2);
            }
            else if (random == 1)
            {
                photonView.RPC("UpdateCases", RpcTarget.All, patientName2, patientName1);
            }
        }
    }

    /*
     * Retrieve the cases for this game from the database
     */
    [PunRPC]
    public void UpdateCases(string name1, string name2)
    {
        Database.RetrieveCaseFromDatabase(name1, SetCase1);
        Database.RetrieveCaseFromDatabase(name2, SetCase2);
    }

    /*
     * Callback for retrieving case 1
     */
    public void SetCase1(Case response)
    {

        patientCase1 = response;
        case1Set = true;
    }

    /*
     * Callback for retrieving case 2
     */
    public void SetCase2(Case response)
    {
        patientCase2 = response;
        case2Set = true;
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

    /*
     * Attempt to join the given room. Success will call
     * OnJoinedRoom and failure will call OnJoinRoomFailed
     */
    public void JoinRoom(string roomCode)
    {
        this.roomCode = roomCode;
        PhotonNetwork.JoinRoom(roomCode);
    }

    /*
     * Called when the player wants to leave the room they are in
     */
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /*
     * When a player has left the room, update the player list
     */
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        mainMenu.photonView.RPC("UpdateRoomUI", RpcTarget.All);
    }

    /*
     * When you join a room, tell everyone to update their player list
     */
    public override void OnJoinedRoom()
    {
        mainMenu.photonView.RPC("UpdateRoomUI", RpcTarget.All);
    }

    /*
     * If joining the room fails, try to give a helpful console warning message.
     */
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

    /*
     * If you disconnect from the PUN2 servers try to 
     * reconnect if it was not on purpose.
     */
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("Disconnected from PUN2 servers.");
        if (isLoggedIn)
        {
            PhotonNetwork.Reconnect();
        }
    }

    /*
     * Load into the next scene.
     */
    [PunRPC]
    public void LoadLevel(int scene)
    {
        PhotonNetwork.LoadLevel(scene);
    }

    /*
     * When the scene changes, try to find the 
     * component controlling the UI.
     */
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
