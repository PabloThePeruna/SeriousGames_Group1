using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPun
{
    public BarGraph barGraph;
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
    public Button easyButton; // button to set difficulty to easy
    public TextMeshProUGUI easyButtonText;
    public Button medButton; // button to set difficulty to medium
    public TextMeshProUGUI medButtonText; 
    public Button hardButton; // button to set difficulty to hard
    public TextMeshProUGUI hardButtonText; 
    public TextMeshProUGUI diffculityLabel; // show non-master client the chosen difficulty
    [SerializeField] private Sprite selectedButtonSprite;
    [SerializeField] private Sprite unselectedButtonSprite;
    private Color32 difficultyDark = new Color32(0x15, 0x40, 0x4D, 0xFF);
    private Color32 difficultyLight = new Color32(0xFF, 0xFF, 0xFF, 0xFF);

    private string userID;

    private void Start()
    {
        //GenerateHummingbirdTimeline();
        // Database.PostPlayerToDatabase(new Player("testlogin", "test@login.com", "test", 0, 0, new List<bool>(), 0, new List<int>()), (bool tmp) => { });
        if (NetworkManager.isLoggedIn)
        {
            Database.RetrievePlayerFromDatabase(NetworkManager.localPlayer.userID, LoginCallback);
            SetScreen(createOrJoinSection);
        }
        else
        {
            SetScreen(loginScreen);
        }
    }

    // temp function for rewriting cases in database
    public void GenerateHummingbirdTimeline()
    {
        // Create case
        //Case patient1 = new Case("Larry", 1, "Male", "60 years", "N/A", "N/A",
        //    new string[] { "110", "106", "90", "85" }, new string[] { "25", "27", "20", "16" },
        //    new string[] { "N/A", "N/A", "N/A", "N/A" }, new string[] { "90", "91", "95", "95" },
        //    new string[] { "N/A", "N/A", "N/A", "N/A" },
        //    "Abdominal pain, swollen abdomen; wet cough; swollen ankles",
        //    "Alcohol abuse for 20 years led to liver failure; liver transplant 6 months ago",
        //    new string[] { "Bilirubine 10 000;  Albimune 10", "Bilirubine 10 000;  Albimune 10", "Bilirubine 10 000;  Albimune 10", "Bilirubine 11 000;  Albimune 12" },
        //    "Prednisolon (immunosuppressive)\nAntibiotic prophylaxis", 3);
        //Case patient2 = new Case("Jeanette", 2, "Female", "77 years", "165 cm", "100 kg",
        //    new string[] { "100", "100", "95", "75" }, new string[] { "25", "25", "25", "12" },
        //    new string[] { "75/35", "82/37", "85/35", "120/70" }, new string[] { "96", "96", "96", "N/A" },
        //    new string[] { "N/A", "N/A", "N/A", "N/A" },
        //    "Pain in the chest and left arm;  anxious and silent",
        //    "Diabetics Mellitus, type II; obese",
        //    new string[] { "N/A", "N/A", "N/A", "N/A" },
        //    "Metformine (for diabetes)", 2);
        //Case patient2 = new Case("James", 3, "Male", "45 years", "N/A", "N/A",
        //    new string[] { "60", "60", "60", "N/A" }, new string[] { "14", "14", "14", "N/A" },
        //    new string[] { "140/85", "140/85", "140/85", "N/A" }, new string[] { "98", "98", "98", "N/A" },
        //    new string[] { "N/A", "N/A", "N/A", "N/A" },
        //    "Found unconscious on the street (patient was biking); uncounscious for few minutes and bit tongue; disoriented in place and time",
        //    "History of epilepsy",
        //    new string[] { "Neurological E3M5V3 Glasgow coma scale; EEG post ictal no epileptic activity", "Neurological E3M5V4 Glasgow coma scale", "Neurological E4M6V5 Glasgow coma scale", "N/A" },
        //    "Kepra (anti-epileptica)", 1);
        //Case patient2 = new Case("Creed", 4, "Male", "60 years", "175 cm", "110 kg",
        //    new string[] { "110", "120", "0", "N/A" }, new string[] { "32", "40", "20", "N/A" },
        //    new string[] { "130/90", "130/90", "0", "N/A" }, new string[] { "90", "80", "0", "N/A" },
        //    new string[] { "40", "40", "40", "N/A" },
        //    "Cough, shortness of breath; fever; feeling unwell 5 days ago, started to feel better but the last few days has had respiratory distress",
        //    "Obese; hypertension and Diabetic Mellitus type II",
        //    new string[] { "White cell count: 20;  CRP: 50;  X-ray of the chest shows bilateral signs of viral infection", "White cell count: 20;  CRP: 50;  X-ray of the chest shows bilateral signs of viral infection", "Cardiac arrest after intubation", "N/A" },
        //    "Metformine (for diabetes)\nDiuretics/betablockers (anti/hypertension treatment", 4);
        //Case patient2 = new Case("Carla", 5, "Male", "60 years", "175 cm", "110 kg",
        //    new string[] { "110", "120", "0", "N/A" }, new string[] { "32", "40", "20", "N/A" },
        //    new string[] { "130/90", "130/90", "0", "N/A" }, new string[] { "90", "80", "0", "N/A" },
        //    new string[] { "40", "40", "40", "N/A" },
        //    "Cough, shortness of breath; fever; feeling unwell 5 days ago, started to feel better but the last few days has had respiratory distress",
        //    "Obese; hypertension and Diabetic Mellitus type II",
        //    new string[] { "White cell count: 20;  CRP: 50;  X-ray of the chest shows bilateral signs of viral infection", "White cell count: 20;  CRP: 50;  X-ray of the chest shows bilateral signs of viral infection", "Cardiac arrest after intubation", "N/A" },
        //    "Metformine (for diabetes)\nDiuretics/betablockers (anti/hypertension treatment", 4);
        // Post to database
        //Database.PostCaseToDatabase(patient2, (bool succeeded) =>
        //{
        //    if (succeeded)
        //    {
        //        Debug.Log(patient2.patientName + " post success!");
        //    }
        //    else
        //    {

        //        Debug.Log(patient2.patientName + " post fail!");
        //    }
        //});
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

    /*
     * Tell the network manager to update the difficulty for everyone
     */
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
        StartCoroutine(LoadNewGame());
    }

    /*
     * Need to wait for the cases to be returned from the database before continuing to the game
     */
    IEnumerator LoadNewGame()
    {
        NetworkManager.instance.ChooseCases();

        while (!NetworkManager.instance.case1Set || !NetworkManager.instance.case2Set)
        {
            yield return null;
        }
        NetworkManager.instance.LoadLevel(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    /*
     * Called when the player clicks the leave room button.
     * Let the network manager know we're leaving and then update 
     * the UI to go back to the main screen
     */
    public void LeaveRoom()
    {
        NetworkManager.instance.LeaveRoom();
        SetScreen(createOrJoinSection);
    }

    /*
     * Called when the player clicks the create room button
     * Tell the network manager to create a room and update
     * the UI to show the room
     */
    public void CreateRoom()
    {
        NetworkManager.instance.CreateRoom();
        SetScreen(waitingRoomSection, true);
    }

    /*
     * Called when the player clicks the join room button on the main screen
     * Update the UI to the find room screen
     */
    public void FindRoom()
    {
        SetScreen(findRoomSection);
    }

    /*
     * Called when the player clicks the join room button. Ask the
     * network manager to join the room based on the input text
     * from roomCodeInput
     */
    public void JoinRoom(TMP_InputField roomCodeInput)
    {
        NetworkManager.instance.JoinRoom(roomCodeInput.text);
    }

    /*
     * Called when the player clicks the log out button. Tell the 
     * network manager before reloading the scene.
     */
    public void LogOut()
    {
        NetworkManager.instance.LogOut();
        SceneManager.LoadScene(0);
    }

    /*
     * Called when the player clicks the log in button. Remove
     * special characters to get the userID and attempt to
     * retrieve the player from the database.
     */
    public void Login()
    {
        NetworkManager.instance.ConnectToMaster();
        userID = emailInput.text;
        userID = userID.ToLower();
        for (int i = 0; i < userID.Length; i++)
        {
            if ((userID[i] < 'a' || userID[i] > 'z') && (userID[i] < '0' || userID[i] > '9'))
            {
                userID = userID.Remove(i, 1);
                i--;
            }
        }
        if (userID != "")
        {
            Database.RetrievePlayerFromDatabase(userID, LoginCallback);
        }
        else
        {
            Debug.LogWarning("Empty email");
        }
    }

    /*
     * Called when the database has finished retrieving the player. If
     * player == null then the player didn't exist and we will create
     * a new player. Update to the home screen
     */
    public void LoginCallback(Player player)
    {
        if (player == null)
        {
            player = CreateNewPlayer();
        }
        NetworkManager.instance.SetPlayer(player);
        nicknameText.text = player.nickname;
        barGraph.CreateGraph();
        SetScreen(homeScreen);
    }

    /*
     * Called if the player didn't exist in the databse. Create a default
     * player and post them
     */
    public Player CreateNewPlayer()
    {
        Player p = new Player(userID, emailInput.text, emailInput.text, 0, 0, new List<bool>(), new List<int>());
        if (userID != "")
        {
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
        } else
        {
            Debug.LogWarning("Do not post empty userID");
        }
        return p;
    }

    /*
     * Update the room name, difficulty, and player list.
     */
    [PunRPC]
    public void UpdateRoomUI()
    {
        SetScreen(waitingRoomSection, PhotonNetwork.IsMasterClient);

        easyButton.image.sprite = unselectedButtonSprite;
        easyButtonText.color = difficultyLight;
        medButton.image.sprite = unselectedButtonSprite;
        medButtonText.color = difficultyLight;
        hardButton.image.sprite = unselectedButtonSprite;
        hardButtonText.color = difficultyLight;
        if (NetworkManager.instance.difficulty == 0)
        {
            easyButton.image.sprite = selectedButtonSprite;
            easyButtonText.color = difficultyDark;
            diffculityLabel.text = "Easy";
        }
        else if (NetworkManager.instance.difficulty == 1)
        {
            medButton.image.sprite = selectedButtonSprite;
            medButtonText.color = difficultyDark; 
            diffculityLabel.text = "Normal";
        }
        else if (NetworkManager.instance.difficulty == 2)
        {
            hardButton.image.sprite = selectedButtonSprite;
            hardButtonText.color = difficultyDark; 
            diffculityLabel.text = "Hard";
        }
        else
        {
            Debug.LogWarning("Not a valid difficulty level.");
        }

        roomCodeText.text = NetworkManager.instance.roomCode;
        playerListText.text = "";

        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }
    }
}
