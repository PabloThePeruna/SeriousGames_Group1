using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class MainMenu : MonoBehaviourPun
{
    public GameObject loginScreen; // screen where user enters login information
    public GameObject homeScreen; // screen containing general and multiplayer sections
    public GameObject generalSection; // static sections of the home screen
    public GameObject createOrJoinSection; // area with buttons to create or join a room
    public GameObject findRoomSection; // area where player enters room code to find room
    public GameObject waitingRoomSection; // area showing room information

    [Header("Login Screen")]
    public TMP_InputField emailInput; // email input field
    public TMP_InputField passwordInput; // password input field

    [Header("Home Screen")]
    public TextMeshProUGUI nicknameText; // display player nickname
    public TextMeshProUGUI roomCodeText; // display room code
    public TextMeshProUGUI playerListText; // display list of players
    public GameObject ownRoom; // button groups for room if this is the master client
    public GameObject foundRoom; // buttons groups for room if this is not the master client
    public GameObject easyButton; // button to set difficulty to easy
    public GameObject medButton; // button to set difficulty to medium
    public GameObject hardButton; // button to set difficulty to hard
    public TextMeshProUGUI diffculityLabel; // show non-master client the chosen difficulty

    private string userID;

    private void Start()
    {
        // Database.PostPlayerToDatabase(new Player("testlogin", "test@login.com", "test", 0, 0, new List<bool>(), 0, new List<int>()), (bool tmp) => { });
        SetScreen(loginScreen);
    }

    /*
     * SetActive the correct parts of the canvas
     * This will cause the 'screen' to be active in hierarchy
     * as well as any other necessary sections 
     */
    public void SetScreen(GameObject screen, bool isRoomOwner = false)
    {
        loginScreen.SetActive(false);

        if (screen == createOrJoinSection || screen == findRoomSection || screen == waitingRoomSection)
        {
            homeScreen.SetActive(true);
            generalSection.SetActive(true);
        }
        else
        {
            homeScreen.SetActive(false);
            generalSection.SetActive(false);
        }

        createOrJoinSection.SetActive(false);
        findRoomSection.SetActive(false);
        waitingRoomSection.SetActive(false);

        // Set the screen active, and make sure it is active in the hierarchy by setting all parents active
        screen.SetActive(true);
        GameObject parent = screen;
        while (!screen.activeInHierarchy)
        {
            parent = parent.transform.parent.gameObject;
            parent.SetActive(true);
        }

        if (screen == homeScreen || screen == generalSection)
        {
            generalSection.SetActive(true);
            createOrJoinSection.SetActive(true);
        }

        if (screen == waitingRoomSection)
        {
            ownRoom.SetActive(isRoomOwner);
            foundRoom.SetActive(!isRoomOwner);
        }
    }

    public void SetDifficulty(int difficulty)
    {
        NetworkManager.instance.photonView.RPC("SetDifficulty", RpcTarget.All, difficulty);
    }

    /*
     * Start the game
     */
    public void PlayGame()
    {
        Debug.Log("Start Game!");
        NetworkManager.instance.ChooseCases();
        NetworkManager.instance.LoadLevel(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LeaveRoom()
    {
        NetworkManager.instance.LeaveRoom();
        SetScreen(createOrJoinSection);
    }

    public void CreateRoom()
    {
        NetworkManager.instance.CreateRoom();
        SetScreen(waitingRoomSection, true);
    }

    public void FindRoom()
    {
        SetScreen(findRoomSection);
    }

    public void JoinRoom(TMP_InputField roomCodeInput)
    {
        NetworkManager.instance.JoinRoom(roomCodeInput.text);
    }

    public void LogOut()
    {
        NetworkManager.instance.LogOut();
        SceneManager.LoadScene(0);
    }

    public void Login()
    {
        userID = emailInput.text;
        userID = userID.ToLower();
        for (int i = 0; i < userID.Length; i++)
        {
            if ((userID[i] < 'a' || userID[i] > 'z' ) && (userID[i] < '0' || userID[i] > '9'))
            {
                userID = userID.Remove(i,1);
                i--;
            }
        }
        Database.RetrievePlayerFromDatabase(userID, LoginCallback);
    }

    public void LoginCallback(Player player)
    {
        if (player == null)
        {
            CreateNewPlayer();
        }
        else
        {
            NetworkManager.instance.SetPlayer(player);
        }
        SetScreen(homeScreen);
        nicknameText.text = PhotonNetwork.NickName;
    }

    public void CreateNewPlayer()
    {
        Player p = new Player(userID, emailInput.text, emailInput.text, 0, 0, new List<bool>(), new List<int>());
        NetworkManager.instance.SetPlayer(p);
        Database.PostPlayerToDatabase(p, (bool succeeded) =>
        {
            if (succeeded)
            {
                Debug.Log("Successfully posted new player.");
            }
            else
            {
                Debug.LogWarning("Player could not be posted to database.");
            }
        });
    }

    [PunRPC]
    public void UpdateRoomUI()
    {
        SetScreen(waitingRoomSection, PhotonNetwork.IsMasterClient);

        //if (NetworkManager.instance.difficulty == 0)
        //{
        //    easyButton.SetActive(true);
        //    medButton.SetActive(false);
        //    hardButton.SetActive(false);
        //    //diffculityLabel.text = "Easy";
        //} else if (NetworkManager.instance.difficulty == 1)
        //{
        //    easyButton.SetActive(false);
        //    medButton.SetActive(true);
        //    hardButton.SetActive(false);
        //    //diffculityLabel.text = "Medium";
        //} else if (NetworkManager.instance.difficulty == 2)
        //{
        //    easyButton.SetActive(false);
        //    medButton.SetActive(false);
        //    hardButton.SetActive(true);
        //    //diffculityLabel.text = "Hard";
        //}
        //else
        //{
        //    Debug.LogWarning("Not a valid difficulty level.");
        //}

        roomCodeText.text = NetworkManager.instance.roomCode;
        playerListText.text = "";

        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }
    }
}
