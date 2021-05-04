using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Notes : MonoBehaviour
{
    public int maxMessages = 30; //Showing only 30 last messages

    public GameObject chatPanel, textObject;
    public TMP_InputField chatBox;
    public Button commentBtn;

    // Set Note feature active by double tapping
    public GameObject Note;
    public GameObject NoteItself;

    private string username;

    private float tapCount;
         

    [SerializeField]
    List<Message> messageList = new List<Message>();

    private void Start()
    {
        username = NetworkManager.localPlayer.nickname;
        commentBtn.onClick.AddListener(Comment);
    }

    // Set Note feature active while double clicking the 2D collider
    void Update()
    {

        DoubleTap();


    }

    //Create and add written note to messagelist
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
            SendMessageToChat(username + ": " + chatBox.text);  //add mark before a note
            chatBox.text = "";
            
        }
    }

    // Set a note active while double tapping
    void DoubleTap()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            tapCount += 1;
            StartCoroutine(Countdown()); // Start calculating is the tapping fast enpugh
        }

        if (tapCount == 2) //Double tapping
        {
            tapCount = 0;
            StopCoroutine(Countdown());

            Note.SetActive(true);   // Set Note to be seen
            GameObject note = Instantiate(NoteItself, transform.position, transform.rotation);
            note.transform.parent = Note.transform;
        }

    }

    //For checking if tapping is fast enough
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.3f);
        tapCount = 0;
    }




    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;


    }

}


