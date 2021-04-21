using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class MainMenu : MonoBehaviourPun
{
    public GameObject loginScreen;
    public GameObject homeScreen;
    public GameObject generalSection;
    public GameObject createOrJoinSection;
    public GameObject findRoomSection;
    public GameObject waitingRoomSection;

    [Header("Login Screen")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    [Header("Home Screen")]
    public TextMeshProUGUI nicknameText;
    public TextMeshProUGUI roomCodeText;
    public TextMeshProUGUI playerListText;
    public GameObject ownRoom;
    public GameObject foundRoom;
    public GameObject easyButton;
    public GameObject medButton;
    public GameObject hardButton;
    public TextMeshProUGUI diffculityLabel;


    private void Start()
    {
        // Database.PostPlayerToDatabase(new Player("testlogin", "test", 0, 0, new List<bool>(), 0, new List<int>()), (bool tmp) => { });
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
        homeScreen.SetActive(false);
        generalSection.SetActive(true);

        createOrJoinSection.SetActive(false);
        findRoomSection.SetActive(false);
        waitingRoomSection.SetActive(false);

        if (screen == createOrJoinSection || screen == findRoomSection || screen == waitingRoomSection)
        {
            homeScreen.SetActive(true);
            generalSection.SetActive(true);

            createOrJoinSection.SetActive(false);
            findRoomSection.SetActive(false);
            waitingRoomSection.SetActive(false);
        }

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

    public void Login()
    {
        Database.RetrievePlayerFromDatabase(emailInput.text, LoginCallback);
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
        Player p = new Player(emailInput.text, emailInput.text, 0, 0, new List<bool>(), -1, new List<int>());
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
