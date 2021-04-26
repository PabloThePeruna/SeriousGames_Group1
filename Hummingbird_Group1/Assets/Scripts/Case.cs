using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class Case
{
    public string patientName;
    public string patientRR;
    public string patientBP;
    public string patientO2Sat;
    public string patientHR;
    public string patientSympDesc;
    public string patientPatDesc;
    public string lab;
    public string medsTaken;
    public int patNum;
    public string gender = "";
    public string age = "";
    public string height = "";
    public string weight = "";
    public string temperature = "";
    public int organNum; // to correspond with the organ select hummingBirdOrganNumber

    public Case(string patientName, string patientRR, string patientBP, string patientO2Sat,
        string patientHR, string patientSympDesc, string patientPatDesc, string lab, string medsTaken,
        int patNum, int organNum = 0)
    {
        this.patientName = patientName;
        this.patientRR = patientRR;
        this.patientBP = patientBP;
        this.patientO2Sat = patientO2Sat;
        this.patientHR = patientHR;
        this.patientSympDesc = patientSympDesc;
        this.patientPatDesc = patientPatDesc;
        this.lab = lab;
        this.medsTaken = medsTaken;
        this.patNum = patNum;
        this.organNum = organNum;
    }


}
