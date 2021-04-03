using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public static PopupController Instance;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; } // stops dups running
        DontDestroyOnLoad(gameObject); // keep me forever
        Instance = this; // set the reference to it

    }

    public Popup CreatePopup()
    {
        GameObject popUpGo = Instantiate(Resources.Load("UI/Popup") as GameObject);
        return popUpGo.GetComponent<Popup>();
    }
}
