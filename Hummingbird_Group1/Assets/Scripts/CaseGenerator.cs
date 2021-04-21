using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseGenerator : MonoBehaviour
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

    public static int patientNumber;

    Case patientCase;

    public void OnSubmit()
    {
        patientNumber++;
        patientCase = new Case(nameText.text, respirationRate.text, bloodPressure.text, o2Saturation.text,
            heartRate.text, symptomDesc.text, patientDesc.text, labResults.text, medication.text, patientNumber);
        Database.PostCaseToDatabase(patientCase, PostResult);
    }

    void PostResult(bool succeeded)
    {
        if (succeeded)
        {
            Debug.Log("Case posted successfully.");
        } else
        {
            Debug.LogWarning("Case could not be posted.");
        }
    }

    public void OnGetScore()
    {
        Database.RetrieveCaseFromDatabase(nameText.text, RetrieveCaseSucceeded);
    }

    void RetrieveCaseSucceeded(Case response)
    {
        patientCase = response;
    }
}
