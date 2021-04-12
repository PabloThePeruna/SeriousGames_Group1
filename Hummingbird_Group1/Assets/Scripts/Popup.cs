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

    const string Note = "NoteHasbeenShowed";
    public static bool isShowed;

    void Start()
    {
        isShowed = PlayerPrefs.GetInt(Note, 0) != 0;
        button1.onClick.AddListener(DontShownAgain);
    }

    void Update()
    {
        if (isShowed)
        {
            popup.SetActive(false);
        }
   

            
    }

    public void DontShownAgain()
    {
       popup.SetActive(false);
       isShowed = true;
       PlayerPrefs.SetInt(Note, 1);
       PlayerPrefs.Save();
       Debug.Log("Note feature has been shown");
    }
}


