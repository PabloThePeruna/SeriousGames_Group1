using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScores : MonoBehaviour
{

    public InputField nameText;
    
    public InputField respirationRate;
    public InputField bloodPressure;
    public InputField o2Saturation;
    public InputField heartRate;
    public InputField symptomDesc;
    public InputField patientDesc;
    public InputField labResults;
    public InputField medication;

    User user = new User();

    public static string playerName;
    public static string rR;
    public static string bP;
    public static string o2Sat;
    public static string hR;
    public static string sympDesc;
    public static string patDesc;
    public static int patientNumber;
    public static string labRes;
    public static string meds;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnSubmit()
    {
        playerName = nameText.text;
        rR = respirationRate.text;
        bP = bloodPressure.text;
        o2Sat = o2Saturation.text;
        hR = heartRate.text;
        sympDesc = symptomDesc.text;
        patDesc = patientDesc.text;
        labRes = labResults.text;
        meds = medication.text;
        patientNumber += 1;
        PostToDatabase();
    }

    public void OnGetScore()
    {
        RetrieveFromDatabase();
    }


    private void PostToDatabase()
    {
        User user = new User();
        RestClient.Put("https://icarus-firebase-rdb-default-rtdb.firebaseio.com/" + playerName + ".json", user);
    }

    private void RetrieveFromDatabase()
    {
        RestClient.Get<User>("https://icarus-firebase-rdb-default-rtdb.firebaseio.com/" + nameText.text + ".json").Then ( response =>
        {
            user = response;
        });
    }
}
