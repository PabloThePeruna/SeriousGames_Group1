using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

public class CasePicker : MonoBehaviour
{

    public Text patientnumber1;
    public Text patientName1;
    public Text respirationRate1;
    public Text heartRate1;
    public Text bloodPressure1;
    public Text o2Saturation1;
    public Text description1;
    public Text labResults1;
    public Text medication1;
    public Text patientSympDesc1;

    public Text patientnumber2;
    public Text patientName2;
    public Text respirationRate2;
    public Text heartRate2;
    public Text bloodPressure2;
    public Text o2Saturation2;
    public Text description2;
    public Text labResults2;
    public Text medication2;
    public Text patientSympDesc2;

    Case patientCase;

    public void OnSelectEasy()  //Called when "Easy" Button is pressed.
    {
        EasyMode();
    }

    public void OnSelectHard()  //Called when "Hard" Button is pressed.
    {
        HardMode();
    }

    public void EasyMode()
    {
        PatientOne("James");
        PatientTwo("Creed");
    }

    public void HardMode()
    {
        PatientOne("Jeanette");
        PatientTwo("Carla");
    }

    public void PatientOne(string patName)  //Retrieves info from the Database
    {
        Database.RetrieveCaseFromDatabase(patName, RetrieveCase1Succeeded);
    }

    public void PatientTwo(string patName)
    {
        Database.RetrieveCaseFromDatabase(patName, RetrieveCase2Succeeded);
    }

    void RetrieveCase1Succeeded(Case response)  
    {
        patientCase = response;
        patientnumber1.text = patientCase.patNum.ToString();  //This line displays the patient number when the Database has finished giving us the patient's information.
        patientName1.text = patientCase.patientName;
        respirationRate1.text = patientCase.patientRR;
        heartRate1.text = patientCase.patientHR;
        bloodPressure1.text = patientCase.patientBP;
        o2Saturation1.text = patientCase.patientO2Sat;
        description1.text = patientCase.patientPatDesc;
        labResults1.text = patientCase.lab;
        medication1.text = patientCase.medsTaken;
        patientSympDesc1.text = patientCase.patientSympDesc;
    }

    void RetrieveCase2Succeeded(Case response)
    {
        patientCase = response;
        patientnumber2.text = patientCase.patNum.ToString();  //This line displays the patient number when the Database has finished giving us the patient's information.
        patientName2.text = patientCase.patientName;
        respirationRate2.text = patientCase.patientRR;
        heartRate2.text = patientCase.patientHR;
        bloodPressure2.text = patientCase.patientBP;
        o2Saturation2.text = patientCase.patientO2Sat;
        description2.text = patientCase.patientPatDesc;
        labResults2.text = patientCase.lab;
        medication2.text = patientCase.medsTaken;
        patientSympDesc2.text = patientCase.patientSympDesc;
    }
}


