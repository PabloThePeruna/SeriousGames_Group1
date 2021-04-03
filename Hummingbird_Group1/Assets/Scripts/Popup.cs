using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



public class Popup : MonoBehaviour
{
    public Button _button1;


    public GameObject popup;


    void Update()
    {
      _button1.onClick.AddListener(() =>
        {
            GameObject.Destroy(this.gameObject);
        });
            
    }

    
}

