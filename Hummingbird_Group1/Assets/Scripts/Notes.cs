using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Photon.Pun;

public class Notes : MonoBehaviourPun
{
    public int maxMessages = 50; //Showing only 30 last messages

    public GameObject chatPanel, textObject;
    public TMP_InputField chatBox;

    public Button commentBtn;
    public Button notesBtn;
    public bool IsClicked = false;
    
    public GameObject Note;

    public GameObject MoreDetails;
    public GameObject LessDetails;

    public Vector3 scaleChange, positionChange;

    private string username;

   
         

    [SerializeField]
    List<Message> messageList = new List<Message>();

    private void Start()
    {
        username = NetworkManager.localPlayer.nickname;
        commentBtn.onClick.AddListener(Comment);
        notesBtn.onClick.AddListener(StartRoutine);

        Note.transform.localScale = new Vector3(0f, 0f, 0f);
        Note.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    // Set Note feature active while double clicking the 2D collider
    void Update()
    {

        ChanegSizeSmaller();
        ChanegSizeBigger();

    }

    //Create and add written note to messagelist
    [PunRPC]
    public void SendMessageToChat(string text)
    {
        // Remove and Delete old messages
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<Text>();

        newMessage.textObject.text = newMessage.text;


        messageList.Add(newMessage);

    }



    // Clicking comment button send note to the chat
    public void Comment()
    {
        if(chatBox.text != "")
        {
            
            chatBox.ActivateInputField();
            photonView.RPC("SendMessageToChat", RpcTarget.All, username + ": " + chatBox.text);  //add mark before a note
            chatBox.text = "";
            
        }
    }

    // Set a note active while double tapping
    IEnumerator ShowNotes()
    {
        if(IsClicked == false)
        {
            Debug.Log("toimii");

            Note.gameObject.SetActive(true);// Set Notes to be seen
            yield return new WaitForSeconds(0.5f);
            IsClicked = true;
        }
        else
        {
            Debug.Log("toimii2");
        
            Note.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            IsClicked = false;
        }
        
    }

    public void ChanegSizeSmaller()
    {
        if (LessDetails.activeSelf)
        {
            Note.transform.localScale = new Vector3(0.4972f, 0.4972f, 0.4972f);
            Note.transform.localPosition = new Vector3(-357.09f, -123.95f, 0f);
        }
    }

    public void ChanegSizeBigger()
    {
        if (MoreDetails.activeSelf)
        {
            Note.transform.localScale = new Vector3(0.7818f, 0.7818f, 0.7818f);
            Note.transform.localPosition = new Vector3(-288f, -70.6f, 0f);
        }
    }




    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;


    }

    public void StartRoutine()
    {
        StartCoroutine(ShowNotes());
    }

}


