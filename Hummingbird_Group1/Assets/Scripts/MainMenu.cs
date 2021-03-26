using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject multiplayerStartScreen;
    public GameObject waitingScreen;
    public GameObject joinRoomScreen;

    public TextMeshProUGUI roomCodeText;
    public TextMeshProUGUI playerListText;
    public GameObject startButton;

    private void Start()
    {
        homeScreen.SetActive(true);
        multiplayerStartScreen.SetActive(false);
        waitingScreen.SetActive(false);
        joinRoomScreen.SetActive(false);
    }

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

    [PunRPC]
    public void UpdateRoomUI()
    {
        roomCodeText.text = NetworkManager.instance.roomCode;
        playerListText.text = "";

        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }
    }
}
