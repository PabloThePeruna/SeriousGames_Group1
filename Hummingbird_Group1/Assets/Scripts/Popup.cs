using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



public class Popup : MonoBehaviour
{
    public Button button1;
    public GameObject popup;

    void Start()
    {
        PlayerPrefs.SetInt("NoteHasBeenShowed", 0);
        button1.onClick.AddListener(DontShownAgain);
    }

    void Update()
    {
        //if(PlayerPrefs.SetInt("NoteHasBeenShowed", 0))
       
            
    }

    public void DontShownAgain()
    {
       popup.SetActive(false);
       PlayerPrefs.SetInt("NoteHasBeenShowed", 1);
       PlayerPrefs.Save();
       Debug.Log("Note feature has been shown");
    }
}


