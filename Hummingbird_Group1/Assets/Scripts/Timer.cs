using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //Example static float variable, that can be reached from everywhere
    //Strore chooseTime to this after clicking "Choose this patient" -button
    public static float chooseTime;

    //Timer
    public float timer;

    //Patient choosing buttons
    public Button choosePatientOneButton;
    public Button choosePatientTwoButton;

    public Button choosePatientOneMoreDetailsButton;
    public Button choosePatientTwoMoreDetailsButton;



    // Start is called before the first frame update
    void Start()
    {
        //Set timer to 0 in start
        timer = 0;

        //Listens when buttons are pressed
        choosePatientOneButton.onClick.AddListener(ChoosePatientOneClick);
        choosePatientTwoButton.onClick.AddListener(ChoosePatientTwoClick);
        choosePatientOneMoreDetailsButton.onClick.AddListener(ChoosePatientOneClick);
        choosePatientTwoMoreDetailsButton.onClick.AddListener(ChoosePatientTwoClick);
    }

    // Update is called once per frame
    void Update()
    {

         //Set timer and round timer float to two decimals
         timer = Mathf.Round(Time.timeSinceLevelLoad * 100f) / 100f;

         //Prints timer just in test purposes
         //Debug.Log("Time " + timer);
        

    }

    void ChoosePatientOneClick()
    {
        //Saves timer time to globally accessible variable chooseTime
        chooseTime = timer;
        Debug.Log("Patient one chosen at time: " + chooseTime + " seconds.");

    }

    void ChoosePatientTwoClick()
    {
        //Saves timer time to globally accessible variable chooseTime
        chooseTime = timer;
        Debug.Log("Patient two chosen at time: " + chooseTime + " seconds.");
    }

    public List<int> GetMinuteSecondList()
    {
        var list = new List<int>() { (int)(chooseTime / 60f), (int)(chooseTime % 60f) };
        return list;
    }
}
