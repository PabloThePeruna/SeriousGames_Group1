using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;
using Photon.Pun;

public class CasePicker : MonoBehaviourPunCallbacks
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

    Case patientCase1;
    Case patientCase2;

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
        patientCase1 = response;
        patientnumber1.text = patientCase1.patNum.ToString();  //This line displays the patient number when the Database has finished giving us the patient's information.
        patientName1.text = patientCase1.patientName;
        respirationRate1.text = patientCase1.patientRR;
        heartRate1.text = patientCase1.patientHR;
        bloodPressure1.text = patientCase1.patientBP;
        o2Saturation1.text = patientCase1.patientO2Sat;
        description1.text = patientCase1.patientPatDesc;
        labResults1.text = patientCase1.lab;
        medication1.text = patientCase1.medsTaken;
        patientSympDesc1.text = patientCase1.patientSympDesc;
        if (patientCase2 != null)
        {
            NetworkManager.instance.photonView.RPC("UpdateCases", RpcTarget.All, patientCase1, patientCase2);
        }
    }

    void RetrieveCase2Succeeded(Case response)
    {
        patientCase2 = response;
        patientnumber2.text = patientCase2.patNum.ToString();  //This line displays the patient number when the Database has finished giving us the patient's information.
        patientName2.text = patientCase2.patientName;
        respirationRate2.text = patientCase2.patientRR;
        heartRate2.text = patientCase2.patientHR;
        bloodPressure2.text = patientCase2.patientBP;
        o2Saturation2.text = patientCase2.patientO2Sat;
        description2.text = patientCase2.patientPatDesc;
        labResults2.text = patientCase2.lab;
        medication2.text = patientCase2.medsTaken;
        patientSympDesc2.text = patientCase2.patientSympDesc;
        if (patientCase1 != null)
        {
            NetworkManager.instance.photonView.RPC("UpdateCases", RpcTarget.All, patientCase1, patientCase2);
        }
    }
}


